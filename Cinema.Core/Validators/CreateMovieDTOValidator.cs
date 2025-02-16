using FluentValidation;
using Cinema.Core.DTOs;

public class CreateMovieDTOValidator : AbstractValidator<CreateMovieDTO>
{
    public CreateMovieDTOValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
    }
}
