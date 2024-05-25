using FPECS.DSP.SPW.DataAccess.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPECS.DSP.SPW.DataAccess.Entities;
public class ResearcherProfile
{
    public required long Id { get; set; }
    public required long ResearcherId { get; set; }
    public required ScienceDatabaseTypes Type { get; set; }
    public string? InternalId { get; set; }
    public string? Url { get; set; }

    public virtual Researcher? Researcher { get; set; }
}