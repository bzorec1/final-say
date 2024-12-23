namespace FinalSay.WebApi.Infrastructure;

public interface IApiEndpointDefinition
{
    public void DefineEndpoints(WebApplication app);

    public void DefineServices(IServiceCollection services);
}