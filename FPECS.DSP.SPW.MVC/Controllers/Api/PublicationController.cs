using FPECS.DSP.SPW.Business.Models.Publication;
using FPECS.DSP.SPW.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace FPECS.DSP.SPW.MVC.Controllers.Api;

[ApiController]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "api.scope")]
[Route("api/[controller]")]
public class PublicationController(IPublicationService publicationService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(PublicationCreateRequest model, CancellationToken cancellationToken = default)
    {
        var createdPublication = await publicationService.CreateAsync(model, cancellationToken);
        return Ok(createdPublication);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPaginatedInformation(int skip = 0, int take = 10, CancellationToken cancellationToken = default)
    {
        var publications = await publicationService.GetAllAsync(skip, take, cancellationToken);
        return Ok(publications);
    }
}