namespace FPECS.DSP.SPW.DataAccess.Entities;

public class Faculty
{
    public required long Id { get; set; }
    public required string Title { get; set; }

    public virtual List<Chair>? Chairs { get; set; }
}