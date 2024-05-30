using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace FPECS.DSP.SPW.MVC.Controllers.Api;
[ApiController]
[Route("[controller]/[action]")]
public class TestController : ControllerBase
{
    [Authorize(Roles = "Manager")]
    [RequiredScope(RequiredScopesConfigurationKey = "api.scope")]
    [HttpGet]
    public async Task<IActionResult> GetTestWithRole()
    {
        return Ok("GetTestWithRole");
    }
}
