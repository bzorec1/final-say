using System;
using FinalSay.Contracts;
using MassTransit;

namespace FinalSay.Worker.StateMachines;

public class FinalSayStateMachine : MassTransitStateMachine<FinalSayState>
{
    public FinalSayStateMachine()
    {
        InstanceState(state => state.CurrentState, Created, Cancelled, Approved, Rejected);

        Event(() => SubmitProposal, x => x.CorrelateById(context => context.Message.ProposalId));
        Event(() => ProposalSubmitted, x =>
        {
            x.CorrelateById(context => context.Message.ProposalId);
            x.OnMissingInstance(configurator => configurator.Discard());
        });
        Event(() => SubmitDecision, x =>
        {
            x.CorrelateById(context => context.Message.ProposalId);
            x.OnMissingInstance(configurator => configurator.Discard());
        });
        Event(() => FinalSayStateRequested, x =>
        {
            x.CorrelateById(context => context.Message.ProposalId);
            x.OnMissingInstance(configurator => configurator.Fault());
        });
        Request(() => ProcessProposal, proposal => proposal.CorrelationId, config => config.Timeout = TimeSpan.Zero);

        Initially(
            When(SubmitProposal)
                .Then(context =>
                {
                    context.Saga.CorrelationId = context.Message.ProposalId;
                    context.Saga.SubmittedAt = context.Message.SubmittedAt;
                    context.Saga.DecidedAt = null;
                    context.Saga.Decisions = [];

                    context.Saga.RequestId = context.RequestId;
                    context.Saga.ResponseAddress = context.ResponseAddress;
                })
                .Request(ProcessProposal,
                    context => new ProcessProposal(context.Message.ProposalId, context.Message.Content,
                        context.Message.Author, context.Message.SubmittedAt, context.Message.Members))
                .TransitionTo(ProcessProposal!.Pending));

        During(ProcessProposal.Pending,
            When(ProcessProposal.Completed)
                .TransitionTo(Created)
                .ThenAsync(async context =>
                {
                    if (context.Saga.ResponseAddress != null)
                    {
                        var endpoint = await context.GetSendEndpoint(context.Saga.ResponseAddress);
                        await endpoint.Send(context.Saga, r => r.RequestId = context.Saga.RequestId);
                    }
                }),
            When(ProcessProposal.Faulted)
                .TransitionTo(Cancelled)
                .ThenAsync(async context =>
                {
                    if (context.Saga.ResponseAddress != null)
                    {
                        var endpoint = await context.GetSendEndpoint(context.Saga.ResponseAddress);
                        await endpoint.Send(new ProposalRejected(context.Saga.CorrelationId),
                            r => r.RequestId = context.Saga.RequestId);
                    }
                }),
            When(ProcessProposal.TimeoutExpired)
                .TransitionTo(Cancelled)
                .ThenAsync(async context =>
                {
                    if (context.Saga.ResponseAddress != null)
                    {
                        var endpoint = await context.GetSendEndpoint(context.Saga.ResponseAddress);
                        await endpoint.Send(new ProposalCancelled(context.Saga.CorrelationId),
                            r => r.RequestId = context.Saga.RequestId);
                    }
                }));

        DuringAny(
            When(FinalSayStateRequested)
                .RespondAsync(x => x.Init<FinalSayStateRequested>(new
                {
                    x.Saga.CorrelationId,
                    x.Saga.CurrentState,
                    x.Saga.Decisions
                })));

        SetCompletedWhenFinalized();
    }

    public State? Created { get; set; }

    public State? Cancelled { get; set; }

    public State? Rejected { get; set; }

    public State? Approved { get; set; }

    public Request<FinalSayState, ProcessProposal, ProposalAccepted> ProcessProposal { get; set; }

    public Event<FinalSayStateRequested>? FinalSayStateRequested { get; set; }

    public Event<SubmitProposal>? SubmitProposal { get; set; }

    public Event<ProposalSubmitted>? ProposalSubmitted { get; set; }

    public Event<SubmitDecision>? SubmitDecision { get; set; }
}