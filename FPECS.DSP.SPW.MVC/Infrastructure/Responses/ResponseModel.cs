namespace FPECS.DSP.SPW.MVC.Infrastructure.Responses;

public class ResponseModel<T> where T : class
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public IDictionary<string, string>? Errors { get; set; }
}