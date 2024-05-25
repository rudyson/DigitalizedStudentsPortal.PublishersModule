using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPECS.DSP.SPW.DataAccess.Entities.Configurations;

public class ResearcherEntityTypeConfiguration : IEntityTypeConfiguration<Researcher>
{
    public void Configure(EntityTypeBuilder<Researcher> builder)
    {
        builder.ToTable("researchers");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.LastName).IsRequired();
        builder.Property(e => e.FirstName).IsRequired();

        builder.HasMany(e => e.ResearcherPseudonyms)
            .WithOne(e => e.Researcher)
            .HasForeignKey(e => e.ResearcherId);

        builder.HasMany(e => e.ResearcherProfiles)
            .WithOne(e => e.Researcher)
            .HasForeignKey(e => e.ResearcherId);
    }
}