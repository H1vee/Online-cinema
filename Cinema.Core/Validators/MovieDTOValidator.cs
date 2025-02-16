using Cinema.Core.DTOs;
using FluentValidation;
using System;

namespace Cinema.Core.Validators
{
    public class MovieDTOValidator : AbstractValidator<MovieDTO>
    {
        public MovieDTOValidator()
        {
            RuleFor(m => m.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must be at most 200 characters long.");

            RuleFor(m => m.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(m => m.ReleaseDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Release date cannot be in the future.");

            RuleFor(m => m.Rating)
                .InclusiveBetween(0, 10).WithMessage("Rating must be between 0 and 10.")
                .When(m => m.Rating.HasValue);

            RuleFor(m => m.Duration)
                .GreaterThan(TimeSpan.Zero).WithMessage("Duration must be greater than zero.");
        }
    }
}