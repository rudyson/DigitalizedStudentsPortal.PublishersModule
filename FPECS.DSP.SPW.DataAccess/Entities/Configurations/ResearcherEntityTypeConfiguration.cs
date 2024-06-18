using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPECS.DSP.SPW.DataAccess.Entities.Configurations;

public class ResearcherEntityTypeConfiguration : IEntityTypeConfiguration<Researcher>
{
    public void Configure(EntityTypeBuilder<Researcher> builder)
    {
        builder.ToTable("researchers");
        builder.HasKey(e => e.Id);

        builder.HasMany(e => e.ResearcherPseudonyms)
            .WithOne(e => e.Researcher)
            .HasForeignKey(e => e.ResearcherId);

        builder.HasMany(e => e.ResearcherProfiles)
            .WithOne(e => e.Researcher)
            .HasForeignKey(e => e.ResearcherId);

        builder.HasOne(e => e.Chair)
            .WithMany(e => e.Researchers)
            .HasForeignKey(e => e.ChairId);
    }
}