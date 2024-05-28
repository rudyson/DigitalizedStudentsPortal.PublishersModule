using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace FPECS.DSP.SPW.MVC.Controllers.Api;
[ApiController]
[Route("[controller]/[action]")]
[Authorize(Roles = "Manager")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class TestController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTest()
    {
        return Ok("Test");
    }
}
