using FPECS.DSP.SPW.Business.Models.Researcher;
using FPECS.DSP.SPW.Business.Models.Researcher.Pseudonym;

namespace FPECS.DSP.SPW.Business.Models.Publication;

public record InternalAuthorModel(ResearcherSearchModel Author, ResearcherPseudonymSearchModel Pseudonym);