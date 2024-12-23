using System;
using FinalSay.Contracts;
using FinalSay.Repository;
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
        Request(() => ProcessDecision, proposal => proposal.CorrelationId, config => config.Timeout = TimeSpan.Zero);

        Initially(
            When(SubmitProposal)
                .InitializeProposal()
                .Request(ProcessProposal, context => new ProcessProposal(context.Message))
                .TransitionTo(ProcessProposal!.Pending));

        During(ProcessProposal.Pending,
            When(ProcessProposal.Completed)
                .TransitionTo(Created)
                .SendResponseAsync(x => new ProposalAccepted(x.Message.ProposalId)),
            When(ProcessProposal.Faulted)
                .TransitionTo(Cancelled)
                .SendResponseAsync(x => new ProposalRejected(x.Message.Message.ProposalId)),
            When(ProcessProposal.TimeoutExpired)
                .TransitionTo(Cancelled)
                .SendResponseAsync(x => new ProposalCancelled(x.Message.Message!.ProposalId)));

        During(Created,
            When(SubmitDecision)
                .InitializeDecision()
                .Request(ProcessDecision, context => new ProcessDecision(context.Message))
                .TransitionTo(ProcessDecision!.Pending));

        During(ProcessDecision.Pending,
            When(ProcessDecision.Completed)
                .TransitionTo(Approved)
                .SendResponseAsync(x => new DecisionAccepted(x.Message.ProposalId)),
            When(ProcessDecision.Completed2)
                .TransitionTo(Rejected)
                .SendResponseAsync(x => new DecisionRejected(x.Message.ProposalId)),
            When(ProcessDecision.Faulted)
                .TransitionTo(Rejected)
                .SendResponseAsync(x => new DecisionRejected(x.Message.Message.ProposalId)),
            When(ProcessDecision.TimeoutExpired)
                .TransitionTo(Rejected)
                .SendResponseAsync(x => new DecisionRejected(x.Message.Message!.ProposalId)));

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

    public Request<FinalSayState, ProcessDecision, DecisionApproved, DecisionRejected> ProcessDecision { get; set; }

    public Event<FinalSayStateRequested>? FinalSayStateRequested { get; set; }

    public Event<SubmitProposal>? SubmitProposal { get; set; }
    
    public Event<ProposalSubmitted>? ProposalSubmitted { get; set; }

    public Event<SubmitDecision>? SubmitDecision { get; set; }
}