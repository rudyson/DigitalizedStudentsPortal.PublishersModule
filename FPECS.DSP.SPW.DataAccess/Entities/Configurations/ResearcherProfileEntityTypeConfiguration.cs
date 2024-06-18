using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPECS.DSP.SPW.DataAccess.Entities.Configurations;

public class ResearcherProfileEntityTypeConfiguration : IEntityTypeConfiguration<ResearcherProfile>
{
    public void Configure(EntityTypeBuilder<ResearcherProfile> builder)
    {
        builder.ToTable("researcher_profiles");
        builder.HasKey(e => e.Id);
    }
}