namespace FPECS.DSP.SPW.Common.Responses;

public class PaginatedResponseModel<TValue> : ResponseModel<TValue> where TValue : class
{
    public int Count { get; set; } = 0;
}