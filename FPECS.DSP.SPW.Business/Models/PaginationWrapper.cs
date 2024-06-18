namespace FPECS.DSP.SPW.Business.Models;
public class PaginationWrapper<TModel> where TModel : class
{
    public TModel? Data { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public int Total { get; set; }
}
