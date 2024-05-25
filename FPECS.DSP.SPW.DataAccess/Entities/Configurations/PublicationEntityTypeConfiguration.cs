using FPECS.DSP.SPW.DataAccess.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPECS.DSP.SPW.DataAccess.Entities.Configurations;

public class PublicationEntityTypeConfiguration : IEntityTypeConfiguration<Publication>
{
    public void Configure(EntityTypeBuilder<Publication> builder)
    {
        builder.ToTable("publications");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title).IsRequired();
        builder.Property(e => e.PublicationOriginSource).IsRequired();
        builder.Property(e => e.Type).IsRequired();
        builder.Property(e => e.Pages).IsRequired();
        builder.Property(e => e.Category).HasDefaultValue(PublicationCategory.B);

        builder.HasMany(e => e.PublicationPublishers)
            .WithOne(e => e.Publication)
            .HasForeignKey(e => e.PublicationId);
    }
}