using FPECS.DSP.SPW.DataAccess.Entities;
using FPECS.DSP.SPW.DataAccess.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPECS.DSP.SPW.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FPECS.DSP.SPW.Business.Services;
public record PublisherProfileIdentityModel(string LastName, string FirstName, string Email);

public class ResearcherGetInformationModel
{
    public required long Id { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }
    public required string FirstName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? OrcidUrl { get; set; }

    public string? Posada { get; set; }
    public string? Zvannya { get; set; }
    public required AcademicDegrees AcademicDegree { get; set; }
    public string? Stepin { get; set; }

    public List<ResearcherPseudonymModel> Pseudonyms { get; set; } = [];
    public List<ResearcherProfileModel> Profiles { get; set; } = [];
}

public class ResearcherPseudonymModel
{
    public required long Id { get; set; }
    public required string ShortName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? FirstName { get; set; }
}

public class ResearcherProfileModel
{
    public required long Id { get; set; }

    public required ScienceDatabaseTypes Type { get; set; }

    // Identifier in science database
    public string? InternalId { get; set; }
    public string? Url { get; set; }
}

public interface IResearcherService
{
    Task<ResearcherGetInformationModel> GetInformationOnLoginAsync(PublisherProfileIdentityModel model, CancellationToken cancellationToken = default);
    Task<ResearcherGetInformationModel> GetInformationAsync(string email, CancellationToken cancellationToken = default);
}
public class ResearcherService(ApplicationDbContext context) : IResearcherService
{
    public async Task<ResearcherGetInformationModel> GetInformationOnLoginAsync(PublisherProfileIdentityModel model, CancellationToken cancellationToken = default)
    {
        var researcherFromDatabase = await context.Researchers
            .Include(x => x.ResearcherProfiles)
            .Include(x => x.ResearcherPseudonyms)
            .FirstOrDefaultAsync(x => x.Email == model.Email, cancellationToken);

        if (researcherFromDatabase is not null)
        {
            return researcherFromDatabase.Adapt<ResearcherGetInformationModel>();
        }

        var adaptedModel = model.Adapt<Researcher>();

        var createdProfile = await context.Researchers.AddAsync(adaptedModel, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return createdProfile.Entity.Adapt<ResearcherGetInformationModel>();
    }

    public async Task<ResearcherGetInformationModel> GetInformationAsync(string email, CancellationToken cancellationToken = default)
    {
        var researcher = await context.Researchers
            .Include(x => x.ResearcherProfiles)
            .Include(x => x.ResearcherPseudonyms)
            .FirstAsync(x => x.Email == email, cancellationToken);

        return researcher.Adapt<ResearcherGetInformationModel>();
    }
}
