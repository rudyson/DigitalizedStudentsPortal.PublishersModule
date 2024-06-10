using FPECS.DSP.SPW.Business.Models.Researcher;

namespace FPECS.DSP.SPW.Business.Models.Publication;

public record InternalAuthorModel(ResearcherSearchModel Author, ResearcherPseudonymSearchModel Pseudonym);