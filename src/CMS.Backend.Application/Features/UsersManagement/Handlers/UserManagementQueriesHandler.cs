using AutoMapper;
using CMS.Backend.Appilication.Features.UsersManagement.DTOs;
using CMS.Backend.Appilication.Features.UsersManagement.Queries;
using CMS.Backend.Application.Common.Models;
using CMS.Backend.Appilication.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CMS.Backend.Application.Features.UsersManagement.Queries;
using CMS.Backend.Application.Common.Interfaces;

namespace CMS.Backend.Appilication.Features.UsersManagement.Handlers
{
    public class UserManagementQueriesHandler :
        IRequestHandler<GetAllUsersQuery, Result>,
        IRequestHandler<GetUserQuery, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public UserManagementQueriesHandler(IUserRepository userRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            var usersDto = _mapper.Map<IEnumerable<GetAllUsersDto>>(users);
            return Result.Success(usersDto);
        }

        public async Task<Result> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_currentUserService.UserId!);
            var user = await _userRepository.GetAsync(userId);
            return Result.Success(user);
        }
    }
}
