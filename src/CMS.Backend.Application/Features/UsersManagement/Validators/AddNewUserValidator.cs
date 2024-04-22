using CMS.Backend.Appilication.Features.UsersManagement.Commands;
using CMS.Backend.Appilication.Repository;
using FluentValidation;
using System.Threading.Tasks;

namespace CMS.Backend.Application.Features.Authentication.Validators
{
    //public class AddNewUserValidator: AbstractValidator<AddNewUserCommand>
    //{
    //    readonly IUserRepository userRepository;
    //    public AddNewUserValidator(IUserRepository repo)
    //    {
    //        userRepository = repo;
    //        RuleFor(cmd => cmd.Email)
    //            .EmailAddress().WithMessage("Please enter a valid email")
    //            .MustAsync( async(email, ct) => await(NotExistEmail(email)))
    //            .WithMessage("This email already registered");
    //        RuleFor(cmd => cmd.FirstName)
    //         .NotEmpty()
    //         .WithMessage("{PropertyName} should be not empty.");
    //        RuleFor(cmd => cmd.LastName)
    //         .NotEmpty()
    //         .WithMessage("{PropertyName} should be not empty.");


    //    }

    //    public async Task<bool> NotExistEmail(string email)
    //    {
    //       return !(await userRepository.CheckEmailExist(email));
    //    }
    //}
}
