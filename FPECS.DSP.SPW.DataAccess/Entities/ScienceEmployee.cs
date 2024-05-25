using FPECS.DSP.SPW.DataAccess.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPECS.DSP.SPW.DataAccess.Entities;
public class ScienceEmployee
{
    public long Id { get; set; }
    public required string Posada { get; set; }
    public required string Zvannya { get; set; }
    public required AcademicDegrees AcademicDegree { get; set; }
    public string Stepin { get; set; }
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public string? OrcidUrl { get; set; }

    public required long ChairId { get; set; }
    public virtual Chair? Chair { get; set; }
}