using FPECS.DSP.SPW.Business.Helpers;
using FPECS.DSP.SPW.Business.Models;
using FPECS.DSP.SPW.Business.Models.Researcher;
using FPECS.DSP.SPW.Business.Models.Researcher.Pseudonym;
using FPECS.DSP.SPW.DataAccess;
using FPECS.DSP.SPW.DataAccess.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FPECS.DSP.SPW.Business.Services;

public interface IResearcherService
{
    Task<ResearcherGetInformationModel> GetInformationOnLoginAsync(PublisherProfileIdentityModel model, CancellationToken cancellationToken = default);
    Task<ResearcherGetInformationModel?> GetInformationByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<ResearcherGetInformationModel?> GetInformationByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<PaginationWrapper<List<ResearcherGetInformationModel>>> GetAllAsync(int skip = 0, int take = 10, CancellationToken cancellationToken = default);
    Task<List<ResearcherSearchModel>> SearchResearchersAsync(string query, CancellationToken cancellationToken = default);
    Task<List<ResearcherPseudonymSearchModel>> GetResearcherPseudonymsAsync(long researcherId, CancellationToken cancellationToken = default);
    Task<ResearcherPseudonymModel> CreateResearcherPseudonymAsync(ResearcherCreatePseudonymModel model, CancellationToken cancellationToken = default);
    Task<ResearcherPseudonymModel?> DeleteResearcherPseudonymAsync(long pseudonymId, CancellationToken cancellationToken = default);
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
        var newPseudonymModel = new ResearcherPseudonym
        {
            Id = 0,
            ResearcherId = 0,
            ShortName = ResearcherHelper.GetResearcherShortName(adaptedModel),
            FirstName = adaptedModel.FirstName,
            MiddleName = adaptedModel.MiddleName,
            LastName = adaptedModel.LastName,
        };
        adaptedModel.ResearcherPseudonyms?.Add(newPseudonymModel);

        var createdProfile = await context.Researchers.AddAsync(adaptedModel, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return createdProfile.Entity.Adapt<ResearcherGetInformationModel>();
    }

    public async Task<ResearcherGetInformationModel?> GetInformationByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var researcher = await context.Researchers
            .Include(x => x.ResearcherProfiles)
            .Include(x => x.ResearcherPseudonyms)
            .Include(x => x.Chair)
            .Include(x => x.Chair!.Faculty)

            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        return researcher?.Adapt<ResearcherGetInformationModel>();
    }

    public async Task<ResearcherGetInformationModel?> GetInformationByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var researcher = await context.Researchers
            .Include(x => x.ResearcherProfiles)
            .Include(x => x.ResearcherPseudonyms)
            .Include(x => x.Chair)
            .Include(x => x.Chair!.Faculty)

            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return researcher?.Adapt<ResearcherGetInformationModel>();
    }

    public async Task<PaginationWrapper<List<ResearcherGetInformationModel>>> GetAllAsync(int skip = 0, int take = 10, CancellationToken cancellationToken = default)
    {
        var queryable = context.Researchers
            .Include(x => x.ResearcherPseudonyms)
            .Include(x => x.Chair)
            .Include(x => x.Chair!.Faculty)
            .OrderBy(x => x.Id);

        var count = await queryable.CountAsync(cancellationToken);

        var researchers = await queryable.Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

        return new PaginationWrapper<List<ResearcherGetInformationModel>>
        {
            Data = researchers.Adapt<List<ResearcherGetInformationModel>>(),
            Skip = skip,
            Take = take,
            Total = count
        };
    }

    public async Task<List<ResearcherSearchModel>> SearchResearchersAsync(string query, CancellationToken cancellationToken = default)
    {
        var clearedQuery = query.Trim().ToLower();
        var researchers = await context.Researchers
            .Where(r => r.FirstName.ToLower().Contains(clearedQuery) || r.LastName.ToLower().Contains(clearedQuery) || r.Email.ToLower().Contains(clearedQuery))
            .ToListAsync(cancellationToken);

        return researchers.Adapt<List<ResearcherSearchModel>>();
    }

    public async Task<List<ResearcherPseudonymSearchModel>> GetResearcherPseudonymsAsync(long researcherId, CancellationToken cancellationToken = default)
    {
        var pseudonyms = await context.ResearcherPseudonyms
            .Where(p => p.ResearcherId == researcherId)
            .ToListAsync(cancellationToken);

        return pseudonyms.Adapt<List<ResearcherPseudonymSearchModel>>();
    }

    public async Task<ResearcherPseudonymModel> CreateResearcherPseudonymAsync(ResearcherCreatePseudonymModel model,
        CancellationToken cancellationToken = default)
    {
        var newPseudonym = model.Adapt<ResearcherPseudonym>();
        var createdPseudonym = await context.ResearcherPseudonyms.AddAsync(newPseudonym, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return createdPseudonym.Entity.Adapt<ResearcherPseudonymModel>();
    }

    public async Task<ResearcherPseudonymModel?> DeleteResearcherPseudonymAsync(long pseudonymId, CancellationToken cancellationToken = default)
    {
        var pseudonymToDelete = await context.ResearcherPseudonyms.FirstOrDefaultAsync(x => x.Id == pseudonymId, cancellationToken);
        if (pseudonymToDelete is null)
        {
            return null;
        }

        context.ResearcherPseudonyms.Remove(pseudonymToDelete);
        await context.SaveChangesAsync(cancellationToken);
        return pseudonymToDelete.Adapt<ResearcherPseudonymModel>();
    }
}
