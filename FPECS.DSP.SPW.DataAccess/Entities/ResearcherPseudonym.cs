namespace FPECS.DSP.SPW.DataAccess.Entities;
public class ResearcherPseudonym
{
    public required long Id { get; set; }
    public required long ResearcherId { get; set; }
    public required string ShortName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? FirstName { get; set; }

    public virtual Researcher? Researcher { get; set; }
}