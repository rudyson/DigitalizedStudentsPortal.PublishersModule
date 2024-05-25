using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPECS.DSP.SPW.DataAccess.Entities;

public class Faculty
{
    public required long Id { get; set; }
    public required string Title { get; set; }

    public virtual List<Chair>? Chairs { get; set; }
}