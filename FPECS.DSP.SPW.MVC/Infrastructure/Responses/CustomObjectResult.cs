using Microsoft.AspNetCore.Mvc;

namespace FPECS.DSP.SPW.MVC.Infrastructure.Responses;

public class CustomObjectResult<TValue> : ObjectResult where TValue : class
{
    public CustomObjectResult(TValue? value, int status) : base(value)
    {
        Value = TypedValue = value;
        StatusCode = status;
        DeclaredType = typeof(TValue);
    }

    public TValue? TypedValue { get; }

    public static implicit operator CustomObjectResult<TValue>(TValue value)
    {
        return new(value, 200);
    }
}