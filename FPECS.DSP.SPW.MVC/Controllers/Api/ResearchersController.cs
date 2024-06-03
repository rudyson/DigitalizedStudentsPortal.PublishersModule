using FPECS.DSP.SPW.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        var researcher = await researcherService.GetInformationAsync(email, cancellationToken);
        return Ok(researcher);
    }
}
