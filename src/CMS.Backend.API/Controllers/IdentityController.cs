using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using CMS.Backend.Application.Common.Models;
using Newtonsoft.Json;
using System.Text;
using CMS.Backend.Appilication.Features.Identity.Commands;
using CMS.Backend.Shared.Authentication;
using CMS.Backend.Application.Common.Exceptions;
using CMS.Backend.Application.Features.Identity.Commands;

namespace CMS.Backend.API.Controllers
{
    [ApiController]
    public class IdentityController : BaseController
    {
        
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityController( IHttpContextAccessor httpContextAccessor)
        {
           
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> UserSignIn(UserSignInCommand command)
            => Ok(await Bus.ExecuteAsync<UserSignInCommand, JsonWebToken>(command));

        [HttpPost("sign-up")]
        public async Task<IActionResult> UserSignUp(UserSignUpCommand command)
            => Ok(await Bus.ExecuteAsync<UserSignUpCommand , Result>(command));
        

    }
}
