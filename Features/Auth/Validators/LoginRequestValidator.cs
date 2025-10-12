using FluentValidation;
using LMS_SoulCode.Features.Auth.Models;

namespace LMS_SoulCode.Features.Auth.Validators
{

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .Matches(@"^[\w\.\-]+@([\w\-]+\.)+[a-zA-Z]{2,7}$")
                .WithMessage("Please enter a valid email address");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
              
    }
}
