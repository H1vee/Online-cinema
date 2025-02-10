using Cinema.Core.DTOs;
using FluentValidation;
using System;

namespace Cinema.Core.Validators
{
    public class ShowtimeValidator : AbstractValidator<ShowtimeDTO>
    {
        public ShowtimeValidator()
        {
            RuleFor(showtime => showtime.MovieTitle)
                .NotEmpty().WithMessage("Movie title is required")
                .MaximumLength(100).WithMessage("Movie title cannot exceed 100 characters");

            RuleFor(showtime => showtime.ShowDateTime)
                .GreaterThan(DateTime.UtcNow).WithMessage("Showtime must be in the future");

            RuleFor(showtime => showtime.HallName)
                .NotEmpty().WithMessage("Hall name is required")
                .MaximumLength(50).WithMessage("Hall name cannot exceed 50 characters");
        }
    }
}
