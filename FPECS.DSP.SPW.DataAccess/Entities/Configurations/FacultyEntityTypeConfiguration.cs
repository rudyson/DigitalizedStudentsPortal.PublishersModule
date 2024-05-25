using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPECS.DSP.SPW.DataAccess.Entities.Configurations;

public class FacultyEntityTypeConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.ToTable("faculties");
        builder.HasKey(e => e.Id);
    }
}