using Cinema.Core.DTOs;
using FluentValidation;
using System;

namespace Cinema.Core.Validators
{
    public class TicketDTOValidator : AbstractValidator<TicketDTO>
    {
        public TicketDTOValidator()
        {
            RuleFor(ticket => ticket.MovieTitle)
                .NotEmpty().WithMessage("Movie title is required");

            RuleFor(ticket => ticket.ShowDateTime)
                .GreaterThan(DateTime.UtcNow).WithMessage("Showtime must be in the future");

            RuleFor(ticket => ticket.RowNumber)
                .GreaterThan(0).WithMessage("Row number must be greater than 0");

            RuleFor(ticket => ticket.SeatNumber)
                .GreaterThan(0).WithMessage("Seat number must be greater than 0");

            RuleFor(ticket => ticket.FinalPrice)
                .GreaterThan(0).WithMessage("Final price must be greater than 0");

            RuleFor(ticket => ticket.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(status => status == "Booked" || status == "Purchased" || status == "Cancelled")
                .WithMessage("Status must be 'Booked', 'Purchased', or 'Cancelled'");
        }
    }
}