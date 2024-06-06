using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPECS.DSP.SPW.DataAccess.Entities.Configurations;

public class PublicationExternalPublisherEntityTypeConfiguration : IEntityTypeConfiguration<PublicationExternalPublisher>
{
    public void Configure(EntityTypeBuilder<PublicationExternalPublisher> builder)
    {
        builder.ToTable("publication_external_publishers");
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Publication)
            .WithMany(e => e.PublicationExternalPublishers)
            .HasForeignKey(e => e.PublicationId);
    }
}