namespace FPECS.DSP.SPW.Common.Responses;

public class ResponseModel<T> where T : class
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public IDictionary<string, string>? Errors { get; set; }
}