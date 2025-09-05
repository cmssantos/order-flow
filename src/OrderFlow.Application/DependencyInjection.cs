using Microsoft.Extensions.DependencyInjection;

namespace OrderFlow.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Adiciona o MediatR e o configura para escanear o assembly atual
        // em busca de Commands, Queries e seus Handlers.
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}
