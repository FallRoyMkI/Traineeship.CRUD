using Microsoft.EntityFrameworkCore;
using WorkTest.Bll;
using WorkTest.Contracts;
using WorkTest.Dal;

namespace WorkTest.Api;

public static class ServiceControllerExtension
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderManager, OrderManager>();
        services.AddScoped<IOrderValidator, OrderValidator>();

        return services;
    }

    public static IServiceCollection AddCustomRepositories(this IServiceCollection repositories)
    {
        repositories.AddTransient<IOrderRepository, OrderRepository>();

        return repositories;
    }

    public static void Migration(this IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        using Context? context = serviceScope.ServiceProvider.GetService<Context>();

        if (context is not null)
        {
            context.Database.Migrate();
        }
    }
}