using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Core.Abstractions;

namespace Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        services.AddMediatR(typeof(Features).Assembly);
        services.AddValidatorsFromAssembly(typeof(Features).Assembly);
        return services;
    }

    public static IServiceCollection AddDatabase<TDb>(
        this IServiceCollection services,
        string connectionString,
        bool useInMemoryDatabase)
        where TDb : DbContext
    {
        services.AddDbContext<TDb>(ConfigureDbContext);
        return services;

        void ConfigureDbContext(IServiceProvider provider, DbContextOptionsBuilder options)
        {
            if (useInMemoryDatabase)
            {
                options.EnableSensitiveDataLogging().UseInMemoryDatabase("InMemoryDb", builder => builder.EnableNullChecks());
            }
            else
            {
                options.UseSqlServer(connectionString, o => o.MigrationsAssembly("Host.Application"));
            }
        }
    }
}