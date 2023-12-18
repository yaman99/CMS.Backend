using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using CMS.Backend.API;
using CMS.Backend.Appilication;
using CMS.Backend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Inject Other Layers
builder.Services.AddAppilication();
builder.Services.AddControllers();
builder.Services.AddApi(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
});


// autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(cb =>
    {
        cb.RegisterAssemblyTypes(Assembly.GetEntryAssembly()!, Assembly.Load("CMS.Backend.Application"),
                Assembly.Load("CMS.Backend.Infrastructure"))
                    .AsImplementedInterfaces();

        cb.AddMongo();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors(x => x.WithOrigins(app.Configuration["AppSettings:Client_Url"].ToString())
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

app.MapControllers();

app.Run();
