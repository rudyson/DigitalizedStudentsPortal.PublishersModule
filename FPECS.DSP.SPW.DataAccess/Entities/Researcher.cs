using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPECS.DSP.SPW.DataAccess.Entities;
public class Researcher
{
    public required long Id { get; set; }
    public long? NppId { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public required string FirstName { get; set; }
    public long? OrcId { get; set; }

    public virtual List<ResearcherPseudonym>? ResearcherPseudonyms { get; set; }
    public virtual List<ResearcherProfile>? ResearcherProfiles { get; set; }
}