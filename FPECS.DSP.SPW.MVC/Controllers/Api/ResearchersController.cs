using FPECS.DSP.SPW.Business.Models.Researcher;
using FPECS.DSP.SPW.Business.Services;
using FPECS.DSP.SPW.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace FPECS.DSP.SPW.MVC.Controllers.Api;
[ApiController]
[Authorize]
[RequiredScope(RequiredScopesConfigurationKey = "api.scope")]
[Route("api/[controller]")]
public class ResearchersController(IResearcherService researcherService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> GetOrCreate(PublisherProfileIdentityModel model, CancellationToken cancellationToken = default) //new PublisherProfileIdentityModel(user.Surname, user.GivenName, user.Mail)
    {
        var mappedUser = await researcherService.GetInformationOnLoginAsync(model, cancellationToken);
        return mappedUser.WrapToActionResult();
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetInformation(CancellationToken cancellationToken = default)
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;

        if (string.IsNullOrEmpty(email))
        {
            return Unauthorized("Preferred username claim not found.");
        }

        var researcher = await researcherService.GetInformationByEmailAsync(email, cancellationToken);

        return researcher.WrapToActionResult();
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetInformationById(long id, CancellationToken cancellationToken = default)
    {
        var researcher = await researcherService.GetInformationByIdAsync(id, cancellationToken);

        return researcher.WrapToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPaginatedInformation(int skip = 0, int take = 10, CancellationToken cancellationToken = default)
    {
        var researchers = await researcherService.GetAllAsync(skip, take, cancellationToken);
        return researchers.WrapToPaginatedActionResult(researchers.Data!, researchers.Total);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchResearchers(string query, CancellationToken cancellationToken = default)
    {
        var researchers = await researcherService.SearchResearchersAsync(query, cancellationToken);

        return researchers.WrapToActionResult();
    }

    [HttpGet("pseudonyms/{researcherId}")]
    public async Task<IActionResult> GetPseudonyms(long researcherId, CancellationToken cancellationToken = default)
    {
        var pseudonyms = await researcherService.GetResearcherPseudonymsAsync(researcherId, cancellationToken);

        return pseudonyms.WrapToActionResult();
    }
}
