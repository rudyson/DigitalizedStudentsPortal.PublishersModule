using FPECS.DSP.SPW.DataAccess.Entities.Enums;

namespace FPECS.DSP.SPW.Business.Models.Publication;
public record PublicationGetInformationModel(
    long Id,
    string Title,
    string Reference,
    PublicationTypes Type,
    PublicationCategory Category,
    DateOnly Year,
    string? Url,
    List<PublicationContributorModel> Contributors,
    List<PublicationContributorModel> ExternalContributors);