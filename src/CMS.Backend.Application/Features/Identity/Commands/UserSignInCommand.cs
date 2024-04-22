using CMS.Backend.Shared.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMS.Backend.Appilication.Features.Identity.Commands
{
    public class UserSignInCommand : IRequest<JsonWebToken>
    {
        public string Email { set; get; }
        public string Password { set; get; }

        [JsonConstructor]
        public UserSignInCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
