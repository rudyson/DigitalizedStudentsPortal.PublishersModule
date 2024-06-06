using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPECS.DSP.SPW.Business.Services;
using FPECS.DSP.SPW.DataAccess.Entities;
using Mapster;

namespace FPECS.DSP.SPW.Business;

public static class MapsterConfigurationExtension
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<Researcher, ResearcherGetInformationModel>.NewConfig()
            .Map(destination => destination.Pseudonyms, source => source.ResearcherPseudonyms)
            .Map(destination => destination.Profiles, s => s.ResearcherProfiles);

        TypeAdapterConfig<Researcher, ResearcherSearchModel>.NewConfig()
            .Map(destination => destination.ShortName, source => $"{source.LastName} {source.FirstName} {source.MiddleName}");
    }
}