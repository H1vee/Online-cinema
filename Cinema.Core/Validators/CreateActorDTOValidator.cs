using Cinema.Core.DTOs;
using FluentValidation;
using System;

namespace Cinema.Core.Validators
{
    public class CreateActorDTOValidator : AbstractValidator<CreateActorDTO>
    {
        public CreateActorDTOValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Actor name is required.")
                .MaximumLength(100).WithMessage("Actor name cannot exceed 100 characters.");

            RuleFor(a => a.BirthDate)
                .LessThan(DateTime.Today).WithMessage("Birth date must be in the past.")
                .When(a => a.BirthDate.HasValue);

            RuleFor(a => a.Nationality)
                .MaximumLength(100).WithMessage("Nationality cannot exceed 100 characters.");
        }
    }
}