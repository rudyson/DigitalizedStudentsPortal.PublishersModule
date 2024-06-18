using FPECS.DSP.SPW.Business.Models;
using FPECS.DSP.SPW.Business.Models.Publication;
using FPECS.DSP.SPW.DataAccess;
using FPECS.DSP.SPW.DataAccess.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FPECS.DSP.SPW.Business.Services;

public interface IPublicationService
{
    Task<PublicationCreateRequest> CreateAsync(PublicationCreateRequest request, CancellationToken cancellationToken = default);
    Task<PaginationWrapper<List<PublicationGetInformationModel>>> GetAllAsync(int skip = 0, int take = 10, CancellationToken cancellationToken = default);
    Task<PaginationWrapper<List<PublicationGetInformationModel>>> SearchAsync(string query, CancellationToken cancellationToken = default);
}

public class PublicationService(ApplicationDbContext context) : IPublicationService
{
    public async Task<PublicationCreateRequest> CreateAsync(PublicationCreateRequest request, CancellationToken cancellationToken = default)
    {
        var newPublicationModel = request.Adapt<Publication>();
        var newEntity = await context.Publications.AddAsync(newPublicationModel, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return newEntity.Adapt<PublicationCreateRequest>();
    }

    public async Task<PaginationWrapper<List<PublicationGetInformationModel>>> GetAllAsync(int skip = 0, int take = 10, CancellationToken cancellationToken = default)
    {
        var queryable = context.Publications
            .Include(x => x.PublicationPublishers)!
                .ThenInclude(x => x.Pseudonym)
            .Include(x => x.PublicationExternalPublishers)
            .OrderByDescending(x => x.Year);

        var count = await queryable.CountAsync(cancellationToken);

        var publications = await queryable.Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

        return new PaginationWrapper<List<PublicationGetInformationModel>>
        {
            Data = publications.Adapt<List<PublicationGetInformationModel>>(),
            Skip = skip,
            Take = take,
            Total = count
        };
    }

    public async Task<PaginationWrapper<List<PublicationGetInformationModel>>> SearchAsync(string query, CancellationToken cancellationToken = default)
    {
        var queryable = context.Publications
            .Include(x => x.PublicationPublishers)!
            .ThenInclude(x => x.Pseudonym)
            .Include(x => x.PublicationExternalPublishers);

        var filtered = queryable
            .Where(x => EF.Functions
                .ToTsVector(x.Title + ' ' + x.Reference)
                .Matches(query))
            .OrderByDescending(x => 
                EF.Functions.ToTsVector(x.Title + ' ' + x.Reference)
                .Rank(EF.Functions.PlainToTsQuery(query)));

        var publications = await filtered
            .Take(5)
            .ToListAsync(cancellationToken);

        return new PaginationWrapper<List<PublicationGetInformationModel>>
        {
            Data = publications.Adapt<List<PublicationGetInformationModel>>(),
            Skip = 0,
            Take = 5,
            Total = publications.Count
        };
    }
}
