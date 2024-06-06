using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPECS.DSP.SPW.DataAccess.Entities.Enums;
public enum PublicationTypes
{
    None = 0,
    Article = 1, // стаття
    Theses = 2, // тези
    MethodicalManual = 3, // Методичний посібник
    StudyMethodicalManual = 4, // Навчально-методичний посібник
    Patent = 5, // Патенти
    Notes = 6 // Замітки
}