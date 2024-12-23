using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using MassTransit.EntityFrameworkIntegration;

namespace FinalSay.Repository;

public class FinalSayStateDbContext : SagaDbContext
{
    public FinalSayStateDbContext()
    {
    }

    public FinalSayStateDbContext(DbCompiledModel options)
        : base(options)
    {
    }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get { yield return new FinalSayStateMap(); }
    }
}