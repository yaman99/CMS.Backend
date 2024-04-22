using CMS.Backend.Appilication.Features.Identity.Commands;
using FluentValidation;


namespace CMS.Backend.Application.Features.Authentication.Validators
{
    public class UserSignInValidator: AbstractValidator<UserSignInCommand>
    {
        public UserSignInValidator()
        {
            RuleFor(cmd => cmd.Email)
              .NotEmpty()
              .WithMessage("{PropertyName} should be not empty.")
              .EmailAddress()
              .WithMessage("Invalid email address");

            RuleFor(cmd => cmd.Password)
              .NotEmpty()
              .WithMessage("{PropertyName} should be not empty.");
        }
    }
}
