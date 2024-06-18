namespace FPECS.DSP.SPW.Business.Models.Researcher.Pseudonym;

public record ResearcherCreatePseudonymModel(long ResearcherId, string ShortName, string? LastName, string? FirstName, string? MiddleName);