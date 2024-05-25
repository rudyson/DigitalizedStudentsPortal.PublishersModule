using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPECS.DSP.SPW.DataAccess.Entities.Configurations;

public class ScienceEmployeeEntityTypeConfiguration : IEntityTypeConfiguration<ScienceEmployee>
{
    public void Configure(EntityTypeBuilder<ScienceEmployee> builder)
    {
        builder.ToTable("science_employee");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Posada).IsRequired();
        builder.Property(e => e.Zvannya).IsRequired();
        builder.Property(e => e.Address).IsRequired();
        builder.Property(e => e.PhoneNumber).IsRequired();

        builder.HasOne(e => e.Chair)
            .WithMany(e => e.ScienceEmployees)
            .HasForeignKey(e => e.ChairId);
    }
}