using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPECS.DSP.SPW.Business.Models.Publication;
using FPECS.DSP.SPW.DataAccess.Entities;
using Mapster;
using FPECS.DSP.SPW.Business.Models.Researcher;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Bibliography;

namespace FPECS.DSP.SPW.Business;

public static class MapsterConfigurationExtension
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<Researcher, ResearcherGetInformationModel>.NewConfig()
            .Map(destination => destination.Pseudonyms, source => source.ResearcherPseudonyms)
            .Map(destination => destination.Profiles, s => s.ResearcherProfiles);

        TypeAdapterConfig<Researcher, ResearcherSearchModel>.NewConfig()
            .Map(destination => destination.ShortName, source => string.Join(' ', source.LastName, source.FirstName, source.MiddleName));


        TypeAdapterConfig<Chair, ChairGetInformationModel>.NewConfig()
            .Map(destination => destination.ChairName, source => source.Name)
            .Map(destination => destination.ChairAbbreviation, source => source.Abbreviation)
            .Map(destination => destination.FacultyTitle, source => source.Faculty!.Title)
            .Map(destination => destination.ChairId, source => source.Id)
            .Map(destination => destination.FacultyId, s => s.FacultyId);

        TypeAdapterConfig<PublicationCreateRequest, Publication>.NewConfig()
            .Map(destination => destination.ConferenceStartDate, source => source.ConferenceDates[0])
            .Map(destination => destination.ConferenceEndDate, source => source.ConferenceDates[1])
            .Map(destination => destination.PublicationExternalPublishers,
                source => source.ExternalAuthors!
                    .Select(x => new PublicationExternalPublisher{Id = 0, PublicationId = 0, Pseudonym = x}),
                shouldMap => shouldMap.ExternalAuthors != null)
            .Map(destination => destination.PublicationPublishers,
                source => source.InternalAuthors
                    .Select(x => new PublicationPublisher { PublicationId = 0, PublisherId = x.Author.Id,  PseudonymId = x.Pseudonym.Id }))
            .Map(destination => destination.Year, source => DateOnly.FromDateTime(source.Year));

        TypeAdapterConfig<InternalAuthorModel, PublicationPublisher>.NewConfig()
            .Map(destination => destination.PseudonymId, source => source.Pseudonym.Id)
            .Map(destination => destination.PublisherId, source => source.Author.Id);

        TypeAdapterConfig<Publication, PublicationGetInformationModel>.NewConfig()
            .Map(
                destination => destination.Contributors,
                source => source.PublicationPublishers!
                    .Select(x => new PublicationContributorModel(
                        x.PublisherId, 
                        x.PseudonymId!.Value,
                        x.Pseudonym!.ShortName))
                    .ToList()
                );
        /*
                    .AddRange(
                        source.PublicationExternalPublishers!
                            .Select(x => new PublicationContributorModel(
                                null,
                                x.Id,
                                x.Pseudonym))
                            .ToList()
                        ));*/
    }
}