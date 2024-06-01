using AutoMapper;
using CMS.Backend.Appilication.Features.UsersManagement.Commands;
using CMS.Backend.Application.Common.Models;
using CMS.Backend.Appilication.Repository;
using CMS.Backend.Domain;
using CMS.Backend.Shared.Types;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CMS.Backend.Domain.Entities;
using CMS.Backend.Application.EventBus.Bus;
using CMS.Backend.Domain.Events;

namespace CMS.Backend.Appilication.Features.UsersManagement.Handlers
{
    internal class UserManagementCommandsHandler :
        IRequestHandler<AddNewUserCommand, Result>,
        IRequestHandler<DeleteUserCommand, Result>,
        IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IDomainBus _eventBus;
        private readonly IMapper _mapper;

        public UserManagementCommandsHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IDomainBus eventBus, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _eventBus = eventBus;
            _mapper = mapper;
        }

        public async Task<Result> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {

            var userId = Guid.NewGuid();

            await CreateUser(userId, request.Email, request.FirstName, request.LastName, request.UserType);

            return Result.Success();
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.UserId);
            if (user == null)
                return Result.Failure(new string[] { "User Invalid" });

            user.IsDeleted = true;
            user.DeActivateAccount();

            //if (user.UserType == RolesName.Customer)
            //{
            //    var userProfile = await _eventBus.ExecuteAsync<GetCustomerByIdQuery, Customer>(new GetCustomerByIdQuery { UserId = user.Id });
            //    if (userProfile != null && userProfile.HasAccount)
            //    {
            //        await _eventBus.ExecuteAsync<ModifyAccountSatusCommand, Result>(new ModifyAccountSatusCommand
            //        {
            //            AccountStatus = false,
            //            CustomerId = userProfile.Id
            //        });
            //    }

            //}
            await _userRepository.UpdateAsync(user);
            return Result.Success();
        }

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.UserId);
            if (user == null)
                return Result.Failure(new string[] { "User Invalid" });

            var newUser = _mapper.Map(request, user);
            await _userRepository.UpdateAsync(newUser);

            return Result.Success();
        }


       
        private async Task CreateUser(Guid id, string email, string firstName, string lastName, string userType)
        {
            var user = new User(id, email, userType, $"{firstName} {lastName}");
            var stampPasswrod = user.GenerateStampPassword();
            user.SetPassword(stampPasswrod, _passwordHasher);

            await _userRepository.AddAsync(user);
            await _eventBus.RaiseEvent(new NewAccountCreated( user.Email, stampPasswrod));
        }
       
        
    }
}
