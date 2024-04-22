using CMS.Backend.Appilication.Features.Identity.Commands;
using CMS.Backend.Appilication.Repository;
using CMS.Backend.Application.Common.Models;
using CMS.Backend.Application.Features.Identity.Commands;
using CMS.Backend.Domain;
using CMS.Backend.Domain.Entities;
using CMS.Backend.Shared.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Backend.Appilication.Features.Identity.Handlers
{
    public class IdentityCommandsHandler :
        IRequestHandler<UserSignInCommand, JsonWebToken>,
        IRequestHandler<UserSignOutCommand, Result>,
        IRequestHandler<UserSignUpCommand, Result>,
        IRequestHandler<RefreshAccessTokenCommand, Result>,
        IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;
        public IdentityCommandsHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
        }

        public async Task<JsonWebToken> Handle(UserSignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Email);
            if (user == null || !user.ValidatePassword(request.Password, _passwordHasher)){
                throw new DomainException(Codes.InvalidCredentials);
            }
            if (!user.IsActive)
            {
                throw new DomainException(Codes.NotActivated);
            }
            if (user.IsDeleted)
            {
                throw new DomainException(Codes.AccountDeleted);
            }

            var jwt = _jwtHandler.CreateToken(user.Id.ToString() , user.Email , user.UserType , user.IsCompleted);
            user.GenerateRefreshToken(_passwordHasher);
            jwt.RefreshToken = user.RefreshToken.Token;

            await _userRepository.UpdateAsync(user);
            return jwt;
                
            
        }

        public async Task<Result> Handle(UserSignOutCommand request, CancellationToken cancellationToken)
        {
            var tokenPayload = _jwtHandler.GetTokenPayload(request.AccessToken);

            if (tokenPayload == null)
                return Result.Failure(new string[] { "INVALID_TOKEN" });

            tokenPayload.Claims.TryGetValue(JwtRegisteredClaimNames.UniqueName, out var userId);
            var user = await _userRepository.GetAsync(Guid.Parse(userId));

            user.RefreshToken.Revoke();
            await _userRepository.UpdateAsync(user);

            return Result.Success();
        }

        public async Task<Result> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenPayload = _jwtHandler.GetTokenPayload(request.AccessToken);
            if (tokenPayload == null)
            {
                return Result.Failure(new string[] { "INVALID_ACCESS_TOKEN" });
            }
            else if (tokenPayload.Expires > DateTime.UtcNow.ToTimestamp())
            {
                return Result.Failure(new string[] { "TOKEN_NOT_EXPIRED" });
            }

            tokenPayload.Claims.TryGetValue(JwtRegisteredClaimNames.UniqueName, out var userId);
            var user = await _userRepository.GetAsync(Guid.Parse(userId));

            // Check Refresh Token
            if (user == null)
            {
                return Result.Failure(new string[] { "INVALID_USER" });
            }
            else if (user.RefreshToken.Token != request.RefreshToken)
            {
                return Result.Failure(new string[] { "INVALID_REFRESH_TOKEN" });
            }
            else if (user.RefreshToken.Revoked)
            {
                return Result.Failure(new string[] { "REVOKED_REFRESH_TOKEN" });
            }
            else if (user.RefreshToken.Expired)
            {
                return Result.Failure(new string[] { "EXPIRED_REFRESH_TOKEN" });
            }

            var jwt = _jwtHandler.CreateToken(user.Id.ToString(), user.Email, user.UserType , user.IsCompleted);
            user.GenerateRefreshToken(_passwordHasher);
            jwt.RefreshToken = user.RefreshToken.Token;

            await _userRepository.UpdateAsync(user);

            return Result.Success(jwt);
        }


        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.UserId);

            if (user == null || !user.ValidatePassword(request.CurrentPassword, _passwordHasher))
            {
                throw new DomainException(Codes.InvalidCredentials);
            }
            if(!user.IsCompleted)
                user.IsCompleted = true;

            user.SetPassword(request.NewPassword, _passwordHasher);

            await _userRepository.UpdateAsync(user);

            return Result.Success();
        }

        public async Task<Result> Handle(UserSignUpCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid();

            var user = new User(userId, request.Email, request.UserType , request.Name);
            
            user.SetPassword(request.Password, _passwordHasher);

            await _userRepository.AddAsync(user);

            return Result.Success();
        }
    }
}
