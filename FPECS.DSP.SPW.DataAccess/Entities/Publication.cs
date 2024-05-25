using FPECS.DSP.SPW.DataAccess.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPECS.DSP.SPW.DataAccess.Entities;

public class Publication
{
    public required long Id { get; set; }
    public required string Title { get; set; }
    public string? PublicationOriginSource { get; set; }
    public string? PublicationOriginSourceUrl { get; set; }
    public required PublicationTypes Type { get; set; }
    public short? Pages { get; set; }
    public short? PagesAuthor { get; set; }
    public string? Doi { get; set; }
    public string? Isbn { get; set; }
    public string? Issn { get; set; }
    public required PublicationCategory Category { get; set; }

    public virtual List<PublicationPublisher>? PublicationPublishers { get; set; }
}