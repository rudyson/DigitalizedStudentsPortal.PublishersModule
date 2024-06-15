using FluentValidation;
using FPECS.DSP.SPW.Business.Models.Publication;
using FPECS.DSP.SPW.DataAccess.Entities.Enums;

namespace FPECS.DSP.SPW.Business.Validators.Publications;
public class PublicationCreateRequestValidator : AbstractValidator<PublicationCreateRequest>
{
    public PublicationCreateRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Reference).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Year).LessThanOrEqualTo(DateTime.Now);
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Category).IsInEnum();
        RuleFor(x => x.InternalAuthors.Count).GreaterThanOrEqualTo(1);

        When(x => x.Type is PublicationTypes.Theses, () =>
        {
            RuleFor(x => x.ConferenceDates).NotEmpty();
            RuleFor(x => x.ConferenceCity).NotEmpty();
            RuleFor(x => x.ConferenceName).NotEmpty();
            When(x => x.ConferenceDates is { Count: 2 }, () =>
            {
                RuleFor(x => x.ConferenceDates![1] > x.ConferenceDates[0]);
            });
            When(x => x.IsInternational, () =>
            {
                RuleFor(x => x.ConferenceCountry).NotEmpty();
            });
        });
    }
}