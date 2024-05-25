using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPECS.DSP.SPW.DataAccess.Entities;
public class PublicationPublisher
{
    public required long PublicationId { get; set; }
    public required long PublisherId { get; set; }
    public required long PseudonymId { get; set; }
    public short? PublicationNumber { get; set; }

    public virtual Publication? Publication { get; set; }
    public virtual Researcher? Publisher { get; set; }
    public virtual ResearcherPseudonym? Pseudonym { get; set; }
}