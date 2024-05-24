using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPECS.DSP.SPW.Business;
public static class MapsterConfigurationExtension
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
/*
TypeAdapterConfig<From, To>
    .NewConfig()
    .Map(dest => dest.Name, src => src.Name);*/
}
}
