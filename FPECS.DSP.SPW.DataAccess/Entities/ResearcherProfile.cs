﻿using FPECS.DSP.SPW.DataAccess.Entities.Enums;

namespace FPECS.DSP.SPW.DataAccess.Entities;
public class ResearcherProfile
{
    public required long Id { get; set; }
    public required ScienceDatabaseTypes Type { get; set; }
    // Identifier in science database
    public string? InternalId { get; set; }
    public string? Url { get; set; }

    public required long ResearcherId { get; set; }
    public virtual Researcher? Researcher { get; set; }
}