using FPECS.DSP.SPW.DataAccess.Entities.Enums;

namespace FPECS.DSP.SPW.Business.Models.Publication;
public record PublicationCreateRequest(
    string Title,
    string Reference,
    PublicationTypes Type,
    PublicationCategory Category,
    DateTime Year,
    short? Pages,
    short? PagesAuthor,
    string? PublishingName,
    bool IsWithStudent,
    bool IsInternational,
    string? ConferenceName,
    List<DateTime> ConferenceDates,
    string? ConferenceCountry,
    string? ConferenceCity,
    string? MagazineName,
    string? MagazineIssue,
    int? MagazineNumber,
    int? PageFirst,
    int? PageLast,
    string? Doi,
    string? Isbn,
    string? Issn,
    string? Url,
    List<InternalAuthorModel> InternalAuthors,
    List<string>? ExternalAuthors
);