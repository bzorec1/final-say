using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using MassTransit.EntityFrameworkIntegration;

namespace FinalSay.Repository;

public class FinalSayStateMap : SagaClassMap<FinalSayState>
{
    protected override void Configure(EntityTypeConfiguration<FinalSayState> entity, DbModelBuilder model)
    {
        entity.Property(x => x.CurrentState);
        entity.Property(x => x.SubmittedAt);

        base.Configure(entity, model);
    }
}