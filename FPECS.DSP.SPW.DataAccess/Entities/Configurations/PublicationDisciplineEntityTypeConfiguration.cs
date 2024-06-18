using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPECS.DSP.SPW.DataAccess.Entities.Configurations;

public class PublicationDisciplineEntityTypeConfiguration : IEntityTypeConfiguration<PublicationDiscipline>
{
    public void Configure(EntityTypeBuilder<PublicationDiscipline> builder)
    {
        builder.ToTable("publications_disciplines");
    }
}