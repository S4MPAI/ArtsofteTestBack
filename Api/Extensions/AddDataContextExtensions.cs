using DAL.Contexts;
using DAL.Interfaces;
using DAL.Repositories;

namespace Api.Extensions;

public static class AddDataContextExtensions
{
    public static IServiceCollection AddInMemoryContext(this IServiceCollection services)
    {
        services
            .AddSingleton<InApplicationMemoryContext>()
            .AddScoped<IUserRepository, InMemoryUserRepository>();

        return services;
    }

    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        //здесь добавляются DbContexts и классы репозиториев
        return services;
    }
}
