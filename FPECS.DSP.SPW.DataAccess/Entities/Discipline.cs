using FPECS.DSP.SPW.DataAccess.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPECS.DSP.SPW.DataAccess.Entities;
public class Discipline
{
    public required long Id { get; set; }
    public required string Name { get; set; }
    public required HighEducationLevels Level { get; set; }

    public virtual List<Publication>? Publications { get; set; }
}
