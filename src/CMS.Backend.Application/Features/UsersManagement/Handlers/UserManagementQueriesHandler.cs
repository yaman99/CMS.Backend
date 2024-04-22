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

namespace CMS.Backend.Appilication.Features.UsersManagement.Handlers
{
    public class UserManagementQueriesHandler :
        IRequestHandler<GetAllUsersQuery, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserManagementQueriesHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            var usersDto = _mapper.Map<IEnumerable<GetAllUsersDto>>(users);
            return Result.Success(usersDto);
        }
    }
}
