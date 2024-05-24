using Microsoft.EntityFrameworkCore;
using System;
namespace FPECS.DSP.SPW.DataAccess;
internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    #region Database sets

    //public required DbSet<Category> Categories { get; set; }

    #endregion
}