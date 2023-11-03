
using System.Text;
using Application.UnitOfWork;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Extension;

public static class ApplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()    //WithOrigins("https://domain.com")
                    .AllowAnyMethod()       //WithMethods("GET","POST)
                    .AllowAnyHeader());     //WithHeaders("accept","content-type")
        });
    public static void AddAplicacionServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

    }
   
}
