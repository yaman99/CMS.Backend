using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using CMS.Backend.Application.Common.Models;
using Newtonsoft.Json;
using System.Text;


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

    }
}
