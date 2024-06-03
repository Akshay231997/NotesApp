using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Application.Features.Identity.Commands.RegisterUser;
using NotesApp.Common.Audits;
using System.Reflection;

namespace NotesApp.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserValidator>();
        services.AddScoped<IAudit, Audit>();

        return services;
    }
}
