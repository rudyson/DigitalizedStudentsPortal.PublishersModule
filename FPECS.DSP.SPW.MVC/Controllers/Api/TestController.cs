using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FPECS.DSP.SPW.MVC.Controllers.Api;
[ApiController]
[Route("[controller]/[action]")]
[Authorize]
public class TestController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetNiggers()
    {
        return Ok("Nigger");
    }
}
