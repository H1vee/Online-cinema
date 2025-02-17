using FluentValidation;
using Cinema.Core.DTOs;

public class SimpleTicketDTOValidator : AbstractValidator<SimpleTicketDTO>
{
    public SimpleTicketDTOValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.FinalPrice).GreaterThan(0);
        RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
    }
}
