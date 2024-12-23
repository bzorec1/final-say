namespace FinalSay.WebApi.Infrastructure;

public static class ApiEndpointDefinitionExtensions
{
    public static void AddApiEndpointDefinitions(this IServiceCollection services, params Type[] scanMarkers)
    {
        var endpointDefinitions = new List<IApiEndpointDefinition>();

        foreach (var marker in scanMarkers)
        {
            endpointDefinitions.AddRange(marker.Assembly.ExportedTypes
                .Where(type => typeof(IApiEndpointDefinition).IsAssignableFrom(type) && type is
                {
                    IsClass: true,
                    IsAbstract: false
                })
                .Select(Activator.CreateInstance)
                .Cast<IApiEndpointDefinition>());
        }

        foreach (var definition in endpointDefinitions)
        {
            definition.DefineServices(services);
        }

        services.AddSingleton<IReadOnlyCollection<IApiEndpointDefinition>>(endpointDefinitions);
    }


    public static void UseApiEndpointDefinitions(this WebApplication app)
    {
        var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IApiEndpointDefinition>>();

        foreach (var definition in definitions)
        {
            definition.DefineEndpoints(app);
        }
    }
}