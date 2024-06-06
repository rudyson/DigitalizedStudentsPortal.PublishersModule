using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPECS.DSP.SPW.DataAccess.Entities;
public class PublicationExternalPublisher
{
    public required long Id { get; set; }
    public required long PublicationId { get; set; }
    public required string Pseudonym { get; set; }

    public virtual Publication? Publication { get; set; }
}