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
                .NotEmpty().WithMessage("Movie title is required");

            RuleFor(showtime => showtime.ShowDateTime)
                .GreaterThan(DateTime.UtcNow).WithMessage("Showtime must be in the future");

            RuleFor(showtime => showtime.HallName)
                .NotEmpty().WithMessage("Hall name is required");
        }
    }
}
