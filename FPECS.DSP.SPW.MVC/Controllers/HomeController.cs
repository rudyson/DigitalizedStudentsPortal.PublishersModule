using FPECS.DSP.SPW.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Graph;
using Microsoft.Identity.Web;

namespace FPECS.DSP.SPW.MVC.Controllers;

[Authorize]
public class HomeController(GraphServiceClient graphServiceClient) : Controller
{

    [AuthorizeForScopes(ScopeKeySection = "MicrosoftGraph:Scopes")]
    public async Task<IActionResult> Index()
    {
        var user = await graphServiceClient.Me.Request().GetAsync();

        ViewData["UserDisplayName"] = user.DisplayName;
        ViewData["UserJobTitle"] = user.JobTitle;
        ViewData["UserId"] = user.Id;
        ViewData["UserMail"] = user.Mail;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
