using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using CMS.Backend.Shared.Types;

namespace CMS.Backend.Shared.Authentication
{
    public class JwtHandler : IJwtHandler
    {
        private static readonly ISet<string> DefaultClaims = new HashSet<string>
        {
            JwtRegisteredClaimNames.Sub,
            JwtRegisteredClaimNames.UniqueName,
            JwtRegisteredClaimNames.Jti,
            JwtRegisteredClaimNames.Iat,
            CustomClaimTypes.UserType,
            CustomClaimTypes.Email,
            CustomClaimTypes.IsCompleted
        };

        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtOptions _options;
        private readonly SigningCredentials _signingCredentials;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtHandler(JwtOptions options)
        {
            _options = options;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = issuerSigningKey,
                ValidIssuer = _options.Issuer,
                ValidAudience = _options.ValidAudience,
                ValidateAudience = _options.ValidateAudience,
                ValidateLifetime = _options.ValidateLifetime,
            };
        }

        public JsonWebToken CreateToken(string userId, string email ,string userType, bool isCompleted, IDictionary<string, string> claims = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User id claim can not be empty.", nameof(userId));
            }

            var now = DateTime.UtcNow;
            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString()),
                new Claim(CustomClaimTypes.UserType, userType),
                new Claim(CustomClaimTypes.Email, email),
                new Claim(CustomClaimTypes.IsCompleted, isCompleted.ToString())
            };


            var customClaims = claims?.Select(claim => new Claim(claim.Key, claim.Value)).ToArray()
                               ?? Array.Empty<Claim>();
            jwtClaims.AddRange(customClaims);
            var expires = now.AddMinutes(_options.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                RefreshToken = string.Empty,
                Expires = expires.ToTimestamp(),
                Claims = customClaims.ToDictionary(c => c.Type, c => c.Value)
            };
        }

        public JsonWebTokenPayload GetTokenPayload(string accessToken)
        {
            try
            {
                _jwtSecurityTokenHandler.ValidateToken(accessToken, _tokenValidationParameters,
                out var validatedSecurityToken);
                if (!(validatedSecurityToken is JwtSecurityToken jwt))
                {
                    return null;
                }

                return new JsonWebTokenPayload
                {
                    Subject = jwt.Subject,
                    Expires = jwt.ValidTo.ToTimestamp(),
                    Claims = jwt.Claims.Where(x => DefaultClaims.Contains(x.Type))
                        .ToDictionary(k => k.Type, v => v.Value)
                };
            }
            catch
            {

                return null;
            }
            
        }
    }
}