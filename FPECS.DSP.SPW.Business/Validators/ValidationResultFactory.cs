using FPECS.DSP.SPW.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using System.Net;

namespace FPECS.DSP.SPW.Business.Validators;
public class ValidationResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
    {
        return new CustomActionResult
        {
            Data = new ResponseModel<object>
            {
                Message = validationProblemDetails?.Title,
                Errors = validationProblemDetails?.Errors.ToDictionary(x => x.Key, x => string.Join("; ", x.Value))
            },
            StatusCode = (int)HttpStatusCode.UnprocessableEntity
        };
    }
}
