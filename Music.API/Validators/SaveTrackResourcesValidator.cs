using FluentValidation;
using Music.API.DTOs;

namespace Music.API.Validators
{
    public class SaveTrackResourcesValidator : AbstractValidator<SaveTrackDTO>
    {
        public SaveTrackResourcesValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.ArtistId)
                .NotEmpty()
                .WithMessage("'Artist Id' must not be empty.");
        }
    }
}
