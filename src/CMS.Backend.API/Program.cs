using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using CMS.Backend.API;
using CMS.Backend.Appilication;
using CMS.Backend.Infrastructure;
using CMS.Backend.Domain.Entities;
using Autofac.Core;
using CMS.Backend.Shared.Authentication;
using Microsoft.AspNetCore.Identity;
using CMS.Backend.Domain.Entities.CourseEntity;
using CMS.Backend.Application.Common.Interfaces;
using CMS.Backend.API.Services;
using Microsoft.OpenApi.Models;
using CMS.Backend.Domain.Entities.CommunityEntity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Inject Other Layers
builder.Services.AddAppilication();
builder.Services.AddControllers();
builder.Services.AddApi(builder.Configuration);
builder.Services.AddJwt();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test01", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
            }
        });
});

// autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(cb =>
    {
        cb.RegisterAssemblyTypes(Assembly.GetEntryAssembly()!, Assembly.Load("CMS.Backend.Application"),
                Assembly.Load("CMS.Backend.Infrastructure"))
                    .AsImplementedInterfaces();

        cb.AddMongo();
        cb.AddMongoRepository<User, Guid>("Users");
        cb.AddMongoRepository<Course, Guid>("Courses");
        cb.AddMongoRepository<Community, Guid>("Communities");

        cb.RegisterType<CurrentUserService>().As<ICurrentUserService>().InstancePerLifetimeScope();
        cb.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x.WithOrigins(app.Configuration["AppSettings:Client_Url"].ToString())
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

app.MapControllers();

app.Run();
