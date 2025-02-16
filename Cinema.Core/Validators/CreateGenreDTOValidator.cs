using Cinema.Core.DTOs;
using FluentValidation;

namespace Cinema.Core.Validators
{
    public class CreateGenreDTOValidator : AbstractValidator<CreateGenreDTO>
    {
        public CreateGenreDTOValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("Genre name is required.")
                .MaximumLength(100).WithMessage("Genre name cannot exceed 100 characters.");
        }
    }
}