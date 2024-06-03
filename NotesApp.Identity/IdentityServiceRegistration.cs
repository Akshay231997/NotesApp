using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Application.Contracts.Identity;
using NotesApp.Identity.Repository;

namespace NotesApp.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IIdentityRepository, IdentityRepository>();

        return services;
    }
}
