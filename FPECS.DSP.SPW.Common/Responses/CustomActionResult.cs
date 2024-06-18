using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FPECS.DSP.SPW.Common.Responses;

public class CustomActionResult : IActionResult
{
    public object? Data { get; set; }
    public int StatusCode { get; set; } = (int)HttpStatusCode.OK;

    public Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new CustomObjectResult<object>(Data, StatusCode);

        if (Data is not ResponseModel<object>)
        {
            objectResult.StatusCode = (int)HttpStatusCode.BadRequest;
        }

        return objectResult.ExecuteResultAsync(context);
    }
}
