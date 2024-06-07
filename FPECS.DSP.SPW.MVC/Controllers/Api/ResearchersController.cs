using FPECS.DSP.SPW.Business.Models.Researcher;
using FPECS.DSP.SPW.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;

namespace FPECS.DSP.SPW.MVC.Controllers.Api;
[ApiController]
[Route("api/[controller]")]
public class ResearchersController(IResearcherService researcherService) : ControllerBase
{
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "api.scope")]
    [HttpPost("create")]
    public async Task<IActionResult> GetOrCreate(PublisherProfileIdentityModel model, CancellationToken cancellationToken = default) //new PublisherProfileIdentityModel(user.Surname, user.GivenName, user.Mail)
    {
        var mappedUser = await researcherService.GetInformationOnLoginAsync(model, cancellationToken);
        return Ok(mappedUser);
    }

    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "api.scope")]
    [HttpGet("get")]
    public async Task<IActionResult> GetInformation(CancellationToken cancellationToken = default)
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;

        if (string.IsNullOrEmpty(email))
        {
            return Unauthorized("Preferred username claim not found.");
        }

        var researcher = await researcherService.GetInformationByEmailAsync(email, cancellationToken);

        if (researcher is null)
        {
            return NotFound();
        }

        return Ok(researcher);
    }

    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "api.scope")]
    [HttpGet("search")]
    public async Task<IActionResult> SearchResearchers(string query, CancellationToken cancellationToken = default)
    {
        var researchers = await researcherService.SearchResearchersAsync(query, cancellationToken);

        return Ok(researchers);
    }

    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "api.scope")]
    [HttpGet("pseudonyms/{researcherId}")]
    public async Task<IActionResult> GetPseudonyms(long researcherId, CancellationToken cancellationToken = default)
    {
        var pseudonyms = await researcherService.GetResearcherPseudonymsAsync(researcherId, cancellationToken);

        return Ok(pseudonyms);
    }
}
