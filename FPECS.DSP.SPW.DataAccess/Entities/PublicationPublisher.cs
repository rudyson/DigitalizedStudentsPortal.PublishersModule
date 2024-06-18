namespace FPECS.DSP.SPW.DataAccess.Entities;
public class PublicationPublisher
{
    public required long PublicationId { get; set; }
    public required long PublisherId { get; set; }
    public long? PseudonymId { get; set; }

    public virtual Publication? Publication { get; set; }
    public virtual Researcher? Publisher { get; set; }
    public virtual ResearcherPseudonym? Pseudonym { get; set; }
}