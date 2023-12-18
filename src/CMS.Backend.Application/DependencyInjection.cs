using BusinessFile.Backend.Application.Common.Behaviours;
using BusinessFile.Backend.Application.EventBus;
using BusinessFile.Backend.Application.EventBus.Bus;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BusinessFile.Backend.Appilication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppilication(this IServiceCollection services)
        {
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IDomainBus, MediatrBus>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            return services;
        }
    }
}
