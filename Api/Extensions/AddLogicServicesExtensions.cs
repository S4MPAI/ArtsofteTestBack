using Logic.Interfaces;
using Logic.Services;

namespace Api.Extensions;

public static class AddLogicServicesExtensions
{
    public static IServiceCollection AddLogicServices(this IServiceCollection services)
    {
        services.AddScoped<IUserManager, UserManager>();

        return services;
    }
}
