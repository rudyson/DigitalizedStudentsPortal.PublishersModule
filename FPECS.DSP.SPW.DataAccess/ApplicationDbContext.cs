using Microsoft.EntityFrameworkCore;
using FPECS.DSP.SPW.DataAccess.Entities;

namespace FPECS.DSP.SPW.DataAccess;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    #region Database sets

    public required DbSet<Researcher> Researchers { get; set; }
    public required DbSet<ResearcherPseudonym> ResearcherPseudonyms { get; set; }
    public required DbSet<ResearcherProfile> ResearcherProfiles { get; set; }
    public required DbSet<Publication> Publications { get; set; }
    public required DbSet<PublicationPublisher> PublicationPublishers { get; set; }
    public required DbSet<Chair> Chairs { get; set; }
    public required DbSet<Faculty> Faculties { get; set; }
    public required DbSet<PublicationDiscipline> PublicationsDisciplines { get; set; }
    public required DbSet<Discipline> Disciplines { get; set; }
    public required DbSet<PublicationExternalPublisher> PublicationExternalPublishers { get; set; }

    #endregion
}