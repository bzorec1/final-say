using FinalSay.Contracts;
using FinalSay.WebApi.Infrastructure;
using MassTransit;

namespace FinalSay.WebApi.EndpointDefinitions;

public class ProposalApiEndpointDefinition : IApiEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/proposals/{proposalId}/decisions",
            async (IRequestClient<GetDecisions> requestClient, Guid proposalId) =>
            {
                var response = await requestClient.GetResponse<List<MemberDecision>>(new { ProposalId = proposalId });
                return Results.Ok(response.Message);
            });

        app.MapPost("/proposals", async (
            IRequestClient<SubmitProposal> requestClient,
            SubmitProposal proposal,
            CancellationToken cancellationToken = default) =>
        {
            var response =
                await requestClient.GetResponse<ProposalAccepted, ProposalRejected>(proposal, cancellationToken);
            return response.Is(out Response<ProposalAccepted>? accepted)
                ? Results.Accepted($"/proposals/{proposal.ProposalId}", response.Message)
                : Results.BadRequest(response.Message);
        });

        app.MapPost("/proposals/{proposalId}/decisions", async (
            IRequestClient<SubmitDecision> requestClient,
            Guid proposalId,
            SubmitDecision decision) =>
        {
            decision = decision with { ProposalId = proposalId };
            var response = await requestClient.GetResponse<DecisionAccepted, DecisionRejected>(decision);
            return response.Is(out Response<DecisionAccepted>? accepted) ? Results.Accepted() : Results.BadRequest(response.Message);
        });

        app.MapGet("/proposals/{proposalId}/state",
            async (IRequestClient<GetProposalState> requestClient, Guid proposalId) =>
            {
                var response = await requestClient.GetResponse<ProposalState>(new { ProposalId = proposalId });

                return response.Message != null ? Results.Ok(response.Message) : Results.NotFound();
            });
    }

    public void DefineServices(IServiceCollection services)
    {
    }
}

public class ProposalState
{
}

public class GetProposalState
{
}

public class GetDecisions
{
}