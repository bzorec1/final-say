using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FinalSay.Repository;

public class FinalSayDbContextFactory : IDesignTimeDbContextFactory<FinalSayDbContext>
{
    public FinalSayDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var connectionString = config.GetConnectionString("Default");

        var optionsBuilder = new DbContextOptionsBuilder<FinalSayDbContext>();

        optionsBuilder.UseSqlServer(connectionString);

        return new FinalSayDbContext(optionsBuilder.Options);
    }
}