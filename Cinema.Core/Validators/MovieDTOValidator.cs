using Cinema.Core.DTOs;
using FluentValidation;
using System;

namespace Cinema.Core.Validators
{
    public class MovieDTOValidator : AbstractValidator<MovieDTO>
    {
        public MovieDTOValidator()
        {
            RuleFor(movie => movie.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

            RuleFor(movie => movie.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(movie => movie.ReleaseDate)
                .NotEmpty().WithMessage("Release date is required")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Release date cannot be in the future");

            RuleFor(movie => movie.Rating)
                .InclusiveBetween(0, 10).WithMessage("Rating must be between 0 and 10");

            RuleFor(movie => movie.Duration)
                .GreaterThan(TimeSpan.Zero).WithMessage("Duration must be greater than zero");

            RuleFor(movie => movie.Genres)
                .NotEmpty().WithMessage("At least one genre is required");

            RuleFor(movie => movie.Actors)
                .NotEmpty().WithMessage("At least one actor is required");
        }
    }
}
