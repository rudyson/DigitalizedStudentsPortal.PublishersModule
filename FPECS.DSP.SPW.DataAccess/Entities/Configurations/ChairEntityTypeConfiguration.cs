using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPECS.DSP.SPW.DataAccess.Entities.Configurations;

public class ChairEntityTypeConfiguration : IEntityTypeConfiguration<Chair>
{
    public void Configure(EntityTypeBuilder<Chair> builder)
    {
        builder.ToTable("chairs");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired();

        builder.HasOne(e => e.Faculty)
            .WithMany(e => e.Chairs)
            .HasForeignKey(e => e.FacultyId);
    }
}