using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPECS.DSP.SPW.DataAccess.Entities.Configurations;

public class ResearcherPseudonymEntityTypeConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<ResearcherPseudonym>
{
    public void Configure(EntityTypeBuilder<ResearcherPseudonym> builder)
    {
        builder.ToTable("researcher_pseudonyms");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ShortName).IsRequired();
    }
}