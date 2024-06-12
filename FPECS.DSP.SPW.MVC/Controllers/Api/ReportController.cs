using FPECS.DSP.SPW.Business.Services;
using FPECS.DSP.SPW.MVC.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FPECS.DSP.SPW.MVC.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class ReportController(IReportService reportService) : ControllerBase
{
    [HttpGet("simple")]
    public async Task<IActionResult> GenerateSimplePublicationsListWord()
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;

        if (string.IsNullOrEmpty(email))
        {
            return Unauthorized("Preferred username claim not found.");
        }
        var resultDocument = await reportService.GenerateSimplePublicationsList(email);
        return resultDocument.WrapToActionResult();
    }
}