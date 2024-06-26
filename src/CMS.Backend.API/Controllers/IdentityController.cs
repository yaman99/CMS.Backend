﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using CMS.Backend.Application.Common.Models;
using Newtonsoft.Json;
using System.Text;
using CMS.Backend.Appilication.Features.Identity.Commands;
using CMS.Backend.Shared.Authentication;
using CMS.Backend.Application.Common.Exceptions;
using CMS.Backend.Application.Features.Identity.Commands;
using Microsoft.Net.Http.Headers;

namespace CMS.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
            => Ok(await Bus.ExecuteAsync<ChangePasswordCommand, Result>(command));

        [HttpPost("sign-out")]
        public async Task<IActionResult> UserSignOut()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            var result = await Bus.ExecuteAsync<UserSignOutCommand, Result>(new UserSignOutCommand
            {
                AccessToken = accessToken,
            });

            if (result.Succeeded)
                return Ok();
            return BadRequest(result.Errors);
        }


    }
}
