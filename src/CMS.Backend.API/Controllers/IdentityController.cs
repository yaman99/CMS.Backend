using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using BusinessFile.Backend.Application.Common.Models;
using Newtonsoft.Json;
using System.Text;


namespace BusinessFile.Backend.API.Controllers
{
    [ApiController]
    public class IdentityController : BaseController
    {
        
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityController( IHttpContextAccessor httpContextAccessor)
        {
           
            _httpContextAccessor = httpContextAccessor;
        }

    }
}
