using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Infrastructure.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace eAppointmentServer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add your infrastructure services here

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        services.AddIdentity<AppUser, AppRole>(action =>
        {
            action.Password.RequiredLength = 8;
            action.Password.RequireDigit = true;
            action.Password.RequireLowercase = true;
            action.Password.RequireUppercase = true;
            action.Password.RequireNonAlphanumeric = true;
            action.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

        services.Scan(scan =>
            scan.FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(registrationStrategy: RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        return services;
    }
}
