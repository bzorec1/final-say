using FinalSay.Contracts;
using FinalSay.Contracts.Commands;
using Microsoft.EntityFrameworkCore;

namespace FinalSay.Repository;

public class FinalSayDbContext : DbContext
{
    public FinalSayDbContext(DbContextOptions options) : base(options)
    {
    }

    public FinalSayDbContext()
    {
    }

    public DbSet<Member> Members { get; set; }

    public DbSet<SubmitProposal> Proposals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity(typeof(SubmitProposal), builder =>
        {
            builder.ToTable("Proposal");

            builder.Property(nameof(SubmitProposal.ProposalId))
                .IsRequired()
                .ValueGeneratedNever();

            builder.HasKey(nameof(SubmitProposal.ProposalId));

            builder.Property(nameof(SubmitProposal.AuthorMemberId))
                .IsRequired();

            builder.Property(nameof(SubmitProposal.SubmittedAt));

            builder.OwnsOne(typeof(ProposalDetails), nameof(SubmitProposal.Details), navigationBuilder =>
            {
                navigationBuilder.Property(nameof(ProposalDetails.Title))
                    .IsRequired();

                navigationBuilder.Property(nameof(ProposalDetails.Description))
                    .IsRequired();
            });

            builder.OwnsMany(typeof(Member), nameof(SubmitProposal.Members), navigationBuilder =>
            {
                navigationBuilder.WithOwner().HasForeignKey(nameof(SubmitProposal.ProposalId));

                navigationBuilder.Property(nameof(Member.MemberId))
                    .IsRequired()
                    .ValueGeneratedNever();

                navigationBuilder.HasKey(nameof(Member.MemberId));

                navigationBuilder.Property(nameof(Member.Name))
                    .IsRequired();

                navigationBuilder.Property(nameof(Member.Email))
                    .IsRequired();
            });
        });
    }
}