
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using BusinessFile.Backend.Api.Filters;


namespace BusinessFile.Backend.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();

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
