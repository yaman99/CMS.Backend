using CMS.Backend.Appilication.Features.Identity.Commands;
using CMS.Backend.Appilication.Repository;
using CMS.Backend.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Appilication.Features.Identity.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        readonly IUserRepository _userRepository;
        readonly IPasswordHasher<User> _passwordHasher;
        public ChangePasswordValidator(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;

            RuleFor(cmd => cmd.CurrentPassword)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.");
           
            RuleFor(cmd => cmd.PasswordConfirmation)
                .NotEmpty()
                .Equal(x => x.NewPassword).WithMessage("New Password is Not Equal Password Confirmation");
        }
        private async Task<bool> IsItOldPassword(Guid userId, string newPassword)
        {
            var user = await _userRepository.GetAsync(userId);
            return !user.ValidatePassword(newPassword, _passwordHasher);
        }
    }
}
