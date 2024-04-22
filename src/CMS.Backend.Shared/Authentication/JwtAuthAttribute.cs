using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CMS.Backend.Shared.Authentication
{
    public class JwtAuthAttribute : AuthAttribute
    {
        public JwtAuthAttribute(string policy = "") : base(JwtBearerDefaults.AuthenticationScheme, policy)
        {
        }
    }
}