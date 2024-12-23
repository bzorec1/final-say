using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinalSay.Repository;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddFinalSayRepository(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<FinalSayDbContext>(options => { options.UseSqlServer(connectionString); });

        return services;
    }
}