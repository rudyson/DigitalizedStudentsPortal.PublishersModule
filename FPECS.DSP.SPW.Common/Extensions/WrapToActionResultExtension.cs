using FPECS.DSP.SPW.Common.Responses;
using System.Net;
using System.Security;

namespace FPECS.DSP.SPW.Common.Extensions;

public static class WrapToCustomActionResultExtension
{
    public static CustomActionResult WrapToActionResult(this object? item)
    {
        return new CustomActionResult()
        {
            Data = new ResponseModel<object>
            {
                Data = item
            },
            StatusCode = item != null ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NoContent
        };
    }

    public static CustomActionResult WrapToPaginatedActionResult(this object? item, object data, int amount)
    {
        return new CustomActionResult()
        {
            Data = new PaginatedResponseModel<object>
            {
                Data = data,
                Count = amount
            },
            StatusCode = item != null ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NoContent
        };
    }

    public static CustomActionResult WrapToActionResult(this HttpStatusCode statusCode, string? message = null,
        string? errorMessage = null)
    {
        return new CustomActionResult()
        {
            StatusCode = (int)statusCode,
            Data = new ResponseModel<object>
            {
                Message = message ?? statusCode.ToString(),
                Errors = errorMessage is null ? null : new Dictionary<string, string>() { { "$", errorMessage } }
            }
        };
    }

    public static CustomActionResult WrapToActionResult(this Exception exception)
    {
        var statusCode = exception switch
        {
            UnauthorizedAccessException => HttpStatusCode.Unauthorized, // 401
            SecurityException => HttpStatusCode.Forbidden, // 403
            ArgumentNullException => HttpStatusCode.BadRequest, // 400
            ArgumentOutOfRangeException => HttpStatusCode.BadRequest, // 400
            ArgumentException => HttpStatusCode.BadRequest, // 400
            KeyNotFoundException => HttpStatusCode.NotFound, // 404
            NotImplementedException => HttpStatusCode.NotImplemented, // 501
            NotSupportedException => HttpStatusCode.NotImplemented, // 501
            ObjectDisposedException => HttpStatusCode.Gone, // 410
            InvalidDataException => HttpStatusCode.UnprocessableEntity, // 422
            TimeoutException => HttpStatusCode.RequestTimeout, // 408
            InvalidOperationException => HttpStatusCode.Conflict, // 409
            OperationCanceledException => HttpStatusCode.Conflict, // 409
            _ => HttpStatusCode.InternalServerError // 500 if unexpected
        };

        return new CustomActionResult()
        {
            StatusCode = (int)statusCode,
            Data = new ResponseModel<object>
            {
                Message = nameof(exception),
                Errors = new Dictionary<string, string>()
                {
                    { nameof(exception.Message), exception.Message },
                    { nameof(exception.StackTrace), exception.StackTrace ?? "" },
                    { nameof(exception.Data), exception.Data.ToString() ?? "" }
                }
            }
        };
    }
}