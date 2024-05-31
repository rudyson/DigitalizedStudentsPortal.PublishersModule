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
    [HttpPost("get")]
    public async Task<IActionResult> GetOrCreate(PublisherProfileIdentityModel model) //new PublisherProfileIdentityModel(user.Surname, user.GivenName, user.Mail)
    {
        var mappedUser = await researcherService.GetInformationOnLoginAsync(model);
        return Ok(mappedUser);
    }
}
