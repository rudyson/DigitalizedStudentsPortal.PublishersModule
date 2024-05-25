using FPECS.DSP.SPW.DataAccess.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPECS.DSP.SPW.DataAccess.Entities;
public class Researcher
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

    public virtual List<ResearcherPseudonym>? ResearcherPseudonyms { get; set; }
    public virtual List<ResearcherProfile>? ResearcherProfiles { get; set; }

    public long? ChairId { get; set; }
    public virtual Chair? Chair { get; set; }
}