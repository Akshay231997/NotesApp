using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Application.Contracts.Identity;
using NotesApp.Identity.DBContext.ApplicationDBContext;
using NotesApp.Identity.DBContext.AuthDBContext;
using NotesApp.Identity.Models;
using NotesApp.Identity.Repository;
using NotesApp.Identity.Service;
using System.Text;

namespace NotesApp.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<AuthDBConnectionDetails>(configuration.GetSection("AuthDBConnectionDetails"));
        services.Configure<ApplicationDBConnectionDetails>(configuration.GetSection("ApplicationDBConnectionDetails"));
        services.AddScoped<IIdentityRepository, IdentityRepository>();
        services.AddSingleton<IAuthDBContext, AuthDBContext>();
        services.AddSingleton<IApplicationDBContext, ApplicationDBContext>();
        services.AddScoped<IToken, Token>();

        MapCollections.MapAllCollections();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))

            };
        });

        return services;
    }
}
