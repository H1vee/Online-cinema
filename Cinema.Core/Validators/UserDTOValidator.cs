using Cinema.Core.DTOs;
using FluentValidation;

namespace Cinema.Core.Validators
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(user => user.FullName)
                .NotEmpty().WithMessage("Full name is required")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Full name can only contain letters and spaces");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(user => user.Roles)
                .NotNull().WithMessage("Roles cannot be null")
                .Must(roles => roles.Count > 0).WithMessage("User must have at least one role")
                .ForEach(role => role.NotEmpty().WithMessage("Each role should not be empty"));
        }
    }
}
