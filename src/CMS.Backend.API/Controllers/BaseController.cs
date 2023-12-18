using Microsoft.AspNetCore.Mvc;
using BusinessFile.Backend.Application.EventBus.Bus;

namespace BusinessFile.Backend.API.Controllers
{
    public class BaseController : ControllerBase
    {
        private IDomainBus _bus;
        protected IDomainBus Bus => _bus ??= HttpContext.RequestServices.GetService(typeof(IDomainBus)) as IDomainBus;
    }
}
