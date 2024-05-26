using FPECS.DSP.SPW.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FPECS.DSP.SPW.Business.Services;
using FPECS.DSP.SPW.DataAccess;
using FPECS.DSP.SPW.DataAccess.Entities.Enums;
using Microsoft.Graph;
using Microsoft.Identity.Web;

namespace FPECS.DSP.SPW.MVC.Controllers;

[Authorize]
public class HomeController(GraphServiceClient graphServiceClient, IResearcherService researcherService) : Controller
{

    [AuthorizeForScopes(ScopeKeySection = "MicrosoftGraph:Scopes")]
    public async Task<IActionResult> Index()
    {
        var user = await graphServiceClient.Me.Request().GetAsync();

        var mappedIdentity =
            new PublisherProfileIdentityModel(user.Surname, user.GivenName, user.Mail, AcademicDegrees.None);

        var mappedUser = await researcherService.GetInformationOnLoginAsync(mappedIdentity);

        // TODO: TenantIdentity verifying
        /*
    ViewData["UserDisplayName"] = user.DisplayName;
    ViewData["UserJobTitle"] = user.JobTitle;
    ViewData["UserGivenName"] = user.GivenName;
    ViewData["UserSurname"] = user.Surname;
    ViewData["UserId"] = user.Id;
    ViewData["UserMail"] = user.Mail;
        */
        return View(mappedUser);
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