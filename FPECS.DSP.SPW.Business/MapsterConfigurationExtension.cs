using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPECS.DSP.SPW.DataAccess.Entities;
using Mapster;
using FPECS.DSP.SPW.Business.Models.Researcher;

namespace FPECS.DSP.SPW.Business;

public static class MapsterConfigurationExtension
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<Researcher, ResearcherGetInformationModel>.NewConfig()
            .Map(destination => destination.Pseudonyms, source => source.ResearcherPseudonyms)
            .Map(destination => destination.Profiles, s => s.ResearcherProfiles);


        TypeAdapterConfig<Chair, ChairGetInformationModel>.NewConfig()
            .Map(destination => destination.ChairName, source => source.Name)
            .Map(destination => destination.ChairAbbreviation, source => source.Abbreviation)
            .Map(destination => destination.FacultyTitle, source => source.Faculty!.Title)
            .Map(destination => destination.ChairId, source => source.Id)
            .Map(destination => destination.FacultyId, s => s.FacultyId);
    }
}