using Microsoft.AspNetCore.Mvc;
using CMS.Backend.Application.EventBus.Bus;

namespace CMS.Backend.API.Controllers
{
    public class BaseController : ControllerBase
    {
        private IDomainBus _bus;
        protected IDomainBus Bus => _bus ??= HttpContext.RequestServices.GetService(typeof(IDomainBus)) as IDomainBus;
    }
}
