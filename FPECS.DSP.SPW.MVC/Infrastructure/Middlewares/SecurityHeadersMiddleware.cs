using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace FPECS.DSP.SPW.MVC.Infrastructure.Middlewares;

public class SecurityHeadersMiddleware(RequestDelegate next)
{
    public Task Invoke(HttpContext context)
    {
        var headers = context.Request.Headers;

        headers.Append(HeaderNames.XContentTypeOptions, (StringValues)"nosniff");
        headers.Append(HeaderNames.XXSSProtection, (StringValues)"1");
        headers.Append(HeaderNames.XFrameOptions, (StringValues)"DENY");

        return next(context);
    }
}