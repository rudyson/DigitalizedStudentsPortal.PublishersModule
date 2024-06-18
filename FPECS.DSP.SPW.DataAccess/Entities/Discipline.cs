using FPECS.DSP.SPW.DataAccess.Entities.Enums;

namespace FPECS.DSP.SPW.DataAccess.Entities;
public class Discipline
{
    public required long Id { get; set; }
    public required string Name { get; set; }
    public required HighEducationLevels Level { get; set; }

    public virtual List<Publication>? Publications { get; set; }
}
