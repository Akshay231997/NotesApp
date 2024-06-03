using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Application.Contracts.Persistence;
using NotesApp.Persistence.Repository;

namespace NotesApp.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ICommonRepository, CommonRepository>();

        return services;
    }
}
