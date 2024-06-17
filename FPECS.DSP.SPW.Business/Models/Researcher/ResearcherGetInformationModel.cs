using FPECS.DSP.SPW.Business.Models.Researcher.Pseudonym;
using FPECS.DSP.SPW.DataAccess.Entities.Enums;

namespace FPECS.DSP.SPW.Business.Models.Researcher;

public class ResearcherGetInformationModel
{
    public required long Id { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public required string FirstName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? OrcidUrl { get; set; }

    public string? Posada { get; set; }
    public string? Zvannya { get; set; }
    public required AcademicDegrees AcademicDegree { get; set; }
    public string? Stepin { get; set; }

    public List<ResearcherPseudonymModel> Pseudonyms { get; set; } = [];
    public List<ResearcherProfileModel> Profiles { get; set; } = [];
    public ChairGetInformationModel? Chair { get; set; }
}