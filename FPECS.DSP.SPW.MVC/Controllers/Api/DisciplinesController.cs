using FPECS.DSP.SPW.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace FPECS.DSP.SPW.MVC.Controllers.Api;
[ApiController]
[Route("[controller]/[action]")]
[RequiredScope(RequiredScopesConfigurationKey = "api.scope")]
public class DisciplinesController(IDisciplineService disciplineService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}
