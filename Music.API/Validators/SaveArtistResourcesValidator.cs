using FluentValidation;
using Music.API.DTOs;

namespace Music.API.Validators
{
    public class SaveArtistResourcesValidator : AbstractValidator<SaveArtistDTO>
    {
        public SaveArtistResourcesValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
