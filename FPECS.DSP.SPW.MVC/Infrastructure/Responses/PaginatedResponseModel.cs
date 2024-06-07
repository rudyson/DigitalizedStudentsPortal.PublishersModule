namespace FPECS.DSP.SPW.MVC.Infrastructure.Responses;

public class PaginatedResponseModel<TValue> : ResponseModel<TValue> where TValue : class
{
    public int Count { get; set; } = 0;
}