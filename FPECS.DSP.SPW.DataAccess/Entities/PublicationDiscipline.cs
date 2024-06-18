namespace FPECS.DSP.SPW.DataAccess.Entities;
public class PublicationDiscipline
{
    public required long PublicationId { get; set; }
    public virtual Publication? Publication { get; set; }

    public required long DisciplineId { get; set; }
    public virtual Discipline? Discipline { get; set; }
}
