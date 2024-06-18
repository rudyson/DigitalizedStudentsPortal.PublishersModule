namespace FPECS.DSP.SPW.DataAccess.Entities;
public class Chair
{
    public required long Id { get; set; }
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }

    public required long FacultyId { get; set; }
    public virtual Faculty? Faculty { get; set; }

    public virtual List<Researcher>? Researchers { get; set; }
}