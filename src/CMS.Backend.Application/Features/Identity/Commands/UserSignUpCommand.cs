using CMS.Backend.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Application.Features.Identity.Commands
{
    public class UserSignUpCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfiramtion{ get; set; }
        public string Name { get; set; }
        public string UserType { get; set; }
    }
}
