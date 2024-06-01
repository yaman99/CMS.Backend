
using CMS.Backend.API.Helpers;
using CMS.Backend.Domain.Events;
using EmailSender.Interfaces;
using MediatR;

namespace CMS.Backend.API.Handlers
{
    
    public class NotificationHandler : 
        INotificationHandler<NewAccountCreated> 
    {

        private readonly IEmailSenderService _emailSenderService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationHandler(IEmailSenderService emailSenderService, IHttpContextAccessor httpContextAccessor)
        {
            _emailSenderService = emailSenderService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Handle(NewAccountCreated notification, CancellationToken cancellationToken)
        {
            string host = _httpContextAccessor.HttpContext.Request.Host.Host;
            var port = _httpContextAccessor.HttpContext.Request.Host.Port;
            var scheme = _httpContextAccessor.HttpContext.Request.Scheme;

            var signinLink = port.HasValue ? $"{scheme}://{host}:{port}/auth/login" :
               $"{scheme}://{host}/auth/login";
            var msg = EmailTemplateParser.GetTempPasswordMessage(notification.Password, signinLink);

            await _emailSenderService.SendEmailAsync("Uskudar CMS System", notification.Email, "Your Account Created", msg);
        }
    }
}
