using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Abstractions;
using Core.Extensions;
using TaxCalculator.Module.Data;
using TaxCalculator.Module.Mapping.TaxBands;
using TaxCalculator.Contract;
using TaxCalculator.Module.Mapping.TaxCalculations;

namespace TaxCalculator.Module.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTaxCalculatorModule(this IServiceCollection services, string connectionString, IConfigurationSection configurationSection, bool useInMemoryDatabase)
    {
        Features.AvailableFeatures.Add(Features.TaxCalculator);
        services.AddDatabase<TaxCalculatorDataContext>(connectionString, useInMemoryDatabase);
        services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly, typeof(Startup).Assembly);
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        services.AddAutoMapper(typeof(TaxBandsMappingProfile).GetTypeInfo().Assembly);
        services.AddAutoMapper(typeof(TaxCalculationsMappingProfile).GetTypeInfo().Assembly);
        return services;
    }
}