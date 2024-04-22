using CMS.Backend.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Appilication.Features.UsersManagement.Commands
{
    public class DeleteUserCommand : IRequest<Result>
    {
        public Guid  UserId { get; set; }
    }
}
