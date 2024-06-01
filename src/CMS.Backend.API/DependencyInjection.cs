
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using CMS.Backend.Api.Filters;
using EmailSender.Interfaces;
using EmailSender.Models;
using EmailSender.Service;


namespace CMS.Backend.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.Configure<SmtpConfig>(configuration.GetSection("SmtpConfig"));

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            }).AddFluentValidation();


            return services;
        }
    }
}
