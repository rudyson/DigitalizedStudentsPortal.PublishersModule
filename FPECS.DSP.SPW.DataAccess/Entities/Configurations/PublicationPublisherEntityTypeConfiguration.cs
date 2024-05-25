using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPECS.DSP.SPW.DataAccess.Entities.Configurations;

public class PublicationPublisherEntityTypeConfiguration : IEntityTypeConfiguration<PublicationPublisher>
{
    public void Configure(EntityTypeBuilder<PublicationPublisher> builder)
    {
        builder.ToTable("publications_publishers");
        builder.HasKey(e => new { e.PublicationId, e.PublisherId, e.PseudonymId });

        builder.HasOne(e => e.Publication)
            .WithMany(e => e.PublicationPublishers)
            .HasForeignKey(e => e.PublicationId);

        builder.HasOne(e => e.Publisher)
            .WithMany()
            .HasForeignKey(e => e.PublisherId);

        builder.HasOne(e => e.Pseudonym)
            .WithMany()
            .HasForeignKey(e => e.PseudonymId);
    }
}