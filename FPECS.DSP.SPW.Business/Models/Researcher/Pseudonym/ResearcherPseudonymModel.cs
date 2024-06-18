namespace FPECS.DSP.SPW.Business.Models.Researcher.Pseudonym;

public class ResearcherPseudonymModel
{
    public required long Id { get; set; }
    public required string ShortName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? FirstName { get; set; }
}