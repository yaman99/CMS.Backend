using System.Collections.Generic;

namespace CMS.Backend.Shared.Authentication
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(string userId, string email, string userType , bool isCompleted, IDictionary<string, string> claims = null);
        JsonWebTokenPayload GetTokenPayload(string accessToken);
    }
}