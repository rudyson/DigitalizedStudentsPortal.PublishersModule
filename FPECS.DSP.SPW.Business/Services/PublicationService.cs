using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPECS.DSP.SPW.Business.Models.Publication;
using FPECS.DSP.SPW.DataAccess;
using FPECS.DSP.SPW.DataAccess.Entities;
using Mapster;

namespace FPECS.DSP.SPW.Business.Services;

public interface IPublicationService
{
    Task<PublicationCreateRequest> CreateAsync(PublicationCreateRequest request, CancellationToken cancellationToken = default);
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
}
