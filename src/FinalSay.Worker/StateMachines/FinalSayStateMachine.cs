using System;
using System.Linq;
using FinalSay.Contracts;
using FinalSay.Repository;
using MassTransit;

namespace FinalSay.Worker.StateMachines;

public sealed class FinalSayStateMachine : MassTransitStateMachine<FinalSayState>
{
    public FinalSayStateMachine()
    {
        InstanceState(state => state.CurrentState, Created, Cancelled, Approved, Rejected);

        Event(() => SubmitProposal, x => x.CorrelateById(context => context.Message.ProposalId));
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
        Request(() => ProcessProposal, proposal => proposal.CorrelationId,
            config => config.Timeout = TimeSpan.FromMinutes(1));
        Request(() => ProcessDecision, proposal => proposal.CorrelationId,
            config => config.Timeout = TimeSpan.FromMinutes(1));

        Initially(
            When(SubmitProposal)
                .InitializeProposal()
                .Request(ProcessProposal, context => new ProcessProposal
                {
                    ProposalId = context.Message.ProposalId,
                    Details = context.Message.Details,
                    AuthorMemberId = context.Message.AuthorMemberId,
                    SubmittedAt = context.Message.SubmittedAt,
                    Members = context.Message.Members
                })
                .TransitionTo(ProcessProposal!.Pending));

        During(ProcessProposal.Pending,
            When(ProcessProposal.Completed)
                .TransitionTo(Created)
                .SendResponseAsync(x => x.Message),
            When(ProcessProposal.Completed2)
                .TransitionTo(Rejected)
                .SendResponseAsync(x => x.Message),
            When(ProcessProposal.Faulted)
                .TransitionTo(Cancelled)
                .SendResponseAsync(x => new ProposalCancelled
                {
                    CancelledAt = DateTime.Now,
                    ProposalId = x.Message.Message.ProposalId,
                    AuthorMemberId = x.Message.Message.AuthorMemberId,
                    Reason = string.Join(", ", x.Message.Exceptions.Select(e => e.Message))
                }),
            When(ProcessProposal.TimeoutExpired)
                .TransitionTo(Cancelled)
                .SendResponseAsync(x => new ProposalCancelled
                {
                    ProposalId = x.Message.Message!.ProposalId,
                    AuthorMemberId = x.Message.Message!.AuthorMemberId,
                    CancelledAt = DateTime.Now,
                    Reason = "The proposal processing timed out."
                }));

        During(Created,
            When(SubmitDecision)
                .InitializeDecision()
                .Request(ProcessDecision, context => new ProcessDecision
                {
                    ProposalId = context.Message.ProposalId,
                    AuthorMemberId = context.Message.AuthorMemberId,
                    Decision = context.Message.Outcome,
                    SubmittedAt = context.Message.SubmittedAt
                })
                .TransitionTo(ProcessDecision!.Pending));

        During(ProcessDecision.Pending,
            When(ProcessDecision.Completed)
                .SendResponseAsync(x => x.Message),
            When(ProcessDecision.Completed2)
                .TransitionTo(Approved)
                .SendResponseAsync(x => x.Message),
            When(ProcessDecision.Completed3)
                .TransitionTo(Rejected)
                .SendResponseAsync(x => x.Message),
            When(ProcessDecision.Faulted)
                .TransitionTo(Created)
                .SendResponseAsync(x => new DecisionRejected
                {
                    DecisionId = x.Message.Message.DecisionId,
                    ProposalId = x.Message.Message.ProposalId,
                    AuthorMemberId = x.Message.Message.AuthorMemberId,
                    Reason = string.Join(", ", x.Message.Exceptions.Select(e => e.Message))
                }),
            When(ProcessDecision.TimeoutExpired)
                .TransitionTo(Created)
                .SendResponseAsync(x => new DecisionRejected
                {
                    DecisionId = x.Message.Message!.DecisionId,
                    ProposalId = x.Message.Message!.ProposalId,
                    AuthorMemberId = x.Message.Message.AuthorMemberId,
                    Reason = "The decision processing timed out."
                }));

        DuringAny(
            When(FinalSayStateRequested)
                .RespondAsync(x => x.Init<FinalSayStateRequested>(new
                {
                    x.Saga.CorrelationId,
                    x.Saga.CurrentState
                })));

        SetCompletedWhenFinalized();
    }

    public State? Created { get; set; }

    public State? Cancelled { get; set; }

    public State? Rejected { get; set; }

    public State? Approved { get; set; }

    public Request<FinalSayState, ProcessProposal, ProposalAccepted, ProposalRejected> ProcessProposal { get; set; }

    public Request<FinalSayState, ProcessDecision, DecisionAccepted, ProposalApproved, ProposalRejected> ProcessDecision
    {
        get;
        set;
    }

    public Event<FinalSayStateRequested>? FinalSayStateRequested { get; set; }

    public Event<SubmitProposal>? SubmitProposal { get; set; }

    public Event<SubmitDecision>? SubmitDecision { get; set; }
}