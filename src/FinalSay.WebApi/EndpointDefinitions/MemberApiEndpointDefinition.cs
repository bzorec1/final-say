using FinalSay.Contracts;
using FinalSay.WebApi.Infrastructure;
using MassTransit;

namespace FinalSay.WebApi.EndpointDefinitions;

public class MemberApiEndpointDefinition : IApiEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/members", async (IRequestClient<CreateMember> requestClient, CreateMember member) =>
        {
            var response = await requestClient.GetResponse<MemberCreated>(member);
            return Results.Created($"/members/{response.Message.MemberId}", response.Message);
        });

        app.MapGet("/members/{memberId}", async (IRequestClient<GetMember> requestClient, Guid memberId) =>
        {
            var response = await requestClient.GetResponse<Member>(new { MemberId = memberId });
            return Results.Ok(response.Message);
        });

        app.MapGet("/members", async (IRequestClient<GetMembers> requestClient) =>
        {
            var response = await requestClient.GetResponse<List<Member>>(new { });
            return Results.Ok(response.Message);
        });
    }

    public void DefineServices(IServiceCollection services)
    {
    }
}

public class GetMembers
{
}

public class GetMember
{
}