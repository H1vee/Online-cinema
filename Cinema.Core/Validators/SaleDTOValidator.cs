using System;
using Cinema.Core.DTOs;
using FluentValidation;

namespace Cinema.Core.Validators
{
    public class SaleDTOValidator : AbstractValidator<SaleDTO>
    {
        public SaleDTOValidator()
        {
            RuleFor(sale => sale.UserFullName)
                .NotEmpty().WithMessage("User full name is required")
                .MaximumLength(30).WithMessage("User full name cannot exceed 30 characters");

            RuleFor(sale => sale.PurchaseDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Purchase date cannot be in the future");

            RuleFor(sale => sale.TotalAmount)
                .GreaterThan(0).WithMessage("Total amount must be greater than 0");

            RuleFor(sale => sale.Tickets)
                .NotEmpty().WithMessage("Sale must contain at least one ticket")
                .ForEach(ticket => ticket.SetValidator(new TicketDTOValidator()));
        }
    }
}
