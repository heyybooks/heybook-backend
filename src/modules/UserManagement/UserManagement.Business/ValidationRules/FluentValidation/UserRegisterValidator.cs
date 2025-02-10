using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Entity.DTOs;

namespace UserManagement.Business.ValidationRules.FluentValidation
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name is required.")
                                     .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name is required.")
                                    .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required.")
                                 .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(u => u.Username).NotEmpty().WithMessage("Username is required.")
                                    .MinimumLength(4).WithMessage("Username must be at least 4 characters.")
                                    .MaximumLength(20).WithMessage("Username cannot exceed 20 characters.");

            RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required.")
                                    .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                                    .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                                    .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                                    .Matches("[0-9]").WithMessage("Password must contain at least one number.");
        }
    }
}
