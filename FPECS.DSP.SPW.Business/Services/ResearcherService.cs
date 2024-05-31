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
}
public interface IResearcherService
{
    Task<ResearcherGetInformationModel> GetInformationOnLoginAsync(PublisherProfileIdentityModel model, CancellationToken cancellationToken = default);
}
public class ResearcherService(ApplicationDbContext context) : IResearcherService
{
    public async Task<ResearcherGetInformationModel> GetInformationOnLoginAsync(PublisherProfileIdentityModel model, CancellationToken cancellationToken = default)
    {
        var researcherFromDatabase = await context.Researchers
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
}
