using FluentValidation;
using UserManagement.Entity.DTOs;

namespace UserManagement.Business.ValidationRules.FluentValidation
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required.");

            RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required.")
                                    .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }
    }
}
