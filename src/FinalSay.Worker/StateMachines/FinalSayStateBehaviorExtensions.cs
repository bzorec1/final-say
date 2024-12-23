using System;
using FinalSay.Contracts.Commands;
using FinalSay.Repository;
using MassTransit;

namespace FinalSay.Worker.StateMachines;

public static class FinalSayStateBehaviorExtensions
{
    public static EventActivityBinder<FinalSayState, SubmitProposal> InitializeProposal(
        this EventActivityBinder<FinalSayState, SubmitProposal> binder)
    {
        return binder
            .Then(context =>
            {
                context.Saga.CorrelationId = context.Message.ProposalId;
                context.Saga.SubmittedAt = context.Message.SubmittedAt;
                context.Saga.DecidedAt = null;

                context.Saga.RequestId = context.RequestId;
                context.Saga.ResponseAddress = context.ResponseAddress;
            });
    }

    public static EventActivityBinder<FinalSayState, SubmitDecision> InitializeDecision(
        this EventActivityBinder<FinalSayState, SubmitDecision> binder)
    {
        return binder
            .Then(context => { });
    }

    public static EventActivityBinder<FinalSayState, T> SendResponseAsync<T, TResponse>(
        this EventActivityBinder<FinalSayState, T> binder, Func<BehaviorContext<FinalSayState, T>, TResponse> message)
        where T : class
        where TResponse : class
    {
        return binder
            .ThenAsync(async context =>
            {
                if (context.Saga.ResponseAddress != null)
                {
                    var endpoint = await context.GetSendEndpoint(context.Saga.ResponseAddress);
                    await endpoint.Send(message, r => r.RequestId = context.Saga.RequestId);
                }
            });
    }
}