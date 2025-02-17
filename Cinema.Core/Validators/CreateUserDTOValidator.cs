using System;
using Cinema.Core.DTOs;
using FluentValidation;

namespace Cinema.Core.Validators
{
    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator()
        {
            RuleFor(user => user.FullName)
                .NotEmpty().WithMessage("Full name is required")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Full name can only contain letters and spaces");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character (e.g., @, $, %, etc.)");
        }
    }
}
