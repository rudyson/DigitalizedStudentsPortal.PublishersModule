using FPECS.DSP.SPW.DataAccess.Entities.Enums;

namespace FPECS.DSP.SPW.Business.Models.Researcher;

public class ResearcherProfileModel
{
    public required long Id { get; set; }

    public required ScienceDatabaseTypes Type { get; set; }

    // Identifier in science database
    public string? InternalId { get; set; }
    public string? Url { get; set; }
}