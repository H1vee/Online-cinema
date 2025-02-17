using Cinema.Core.DTOs;
using FluentValidation;

namespace Cinema.Core.Validators
{
    public class CreateTicketDTOValidator : AbstractValidator<CreateTicketDTO>
    {
        public CreateTicketDTOValidator()
        {
            RuleFor(ticket => ticket.ShowtimeID).GreaterThan(0).WithMessage("ShowtimeID must be greater than 0.");
            RuleFor(ticket => ticket.SeatID).GreaterThan(0).WithMessage("SeatID must be greater than 0.");
            RuleFor(ticket => ticket.UserID).GreaterThan(0).WithMessage("UserID must be greater than 0.");
            RuleFor(ticket => ticket.RuleID).GreaterThan(0).WithMessage("RuleID must be greater than 0.");
            RuleFor(ticket => ticket.FinalPrice).GreaterThan(0).WithMessage("FinalPrice must be greater than 0.");
            RuleFor(ticket => ticket.Status).NotEmpty().WithMessage("Status is required.");
        }
    }
}