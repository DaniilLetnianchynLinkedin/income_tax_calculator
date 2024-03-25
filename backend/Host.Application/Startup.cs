using Core.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using Core.Abstractions;
using Core.Abstractions.ServiceBus;
using Core.Extensions;
using Core.Handlers;
using Core.Logging;
using Core.PipelineBehaviors;
using Core.ServiceBus;
using TaxCalculator.Module.Data;
using TaxCalculator.Module.Extensions;
using Host.Application.Extensions;
using ILogger = Core.Abstractions.Logging.ILogger;



namespace Host.Application;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        Configuration = configuration;
        WebHostEnvironment = webHostEnvironment;
    }

    public IConfiguration Configuration { get; }

    public IWebHostEnvironment WebHostEnvironment { get; }

    public bool UseInMemoryDatabase => string.Equals(WebHostEnvironment.EnvironmentName, "Testing", StringComparison.OrdinalIgnoreCase);

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        var connectionString = Configuration["LocalDbConnectionString"] ??
        $"Server=tcp:{Configuration["DbConnectionString:Server"]},{Configuration["DbConnectionString:Port"] ?? "1433"};" +
        $"Initial Catalog={Configuration["DbConnectionString:Database"]};" +
        "Persist Security Info=False;" +
        $"User ID={Configuration["DbConnectionString:Username"]};" +
        $"Password={Configuration["DbConnectionString:Password"]};" +
        "MultipleActiveResultSets=False;" +
        "Encrypt=True;" +
        "TrustServerCertificate=True;" +
        "Connection Timeout=30;";

        services.AddControllers();
        services.AddFeatureManagement().UseDisabledFeaturesHandler(new DisabledModuleFeaturesHandler());
        services.AddCore();
        services.AddScoped<IServiceBus, ServiceBus>();
        AddModuleIfEnabled(Features.TaxCalculator, services.AddTaxCalculatorModule, out _);

        var assembly = typeof(Startup).Assembly;
        services.AddMediatR(assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(assembly);
        services.AddOpenApiDocument();
        services.AddSingleton<ILogger, Logger>();

        void AddModuleIfEnabled(string moduleName, Func<string, IConfigurationSection, bool, IServiceCollection> addModule, out bool isModuleEnabled)
        {
            var moduleFeature = Configuration[ConfigurationPath.Combine("FeatureManagement", moduleName)];
            isModuleEnabled = bool.TryParse(moduleFeature, out var moduleFeatureBool) && moduleFeatureBool;
            if (isModuleEnabled)
            {
                addModule(connectionString, Configuration.GetSection(ConfigurationPath.Combine("ModuleSettings", moduleName)), UseInMemoryDatabase);
            }
        }
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseProductionExceptionHandler();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        var allowedOrigins = Configuration.GetSection("AllowedCorsOrigins")
            .GetChildren().Select(x => x.Value).ToHashSet();
        app.UseCors(pol => pol.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin());

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseOpenApi();
        app.UseSwaggerUi3();

        EnsureDatabaseCreated(app);
    }

    private void EnsureDatabaseCreated(IApplicationBuilder app)
    {
        EnsureDatabaseMigratedIfEnabled<TaxCalculatorDataContext>(Features.TaxCalculator);

        void EnsureDatabaseMigratedIfEnabled<TDataContext>(string moduleName)
            where TDataContext : DbContext
        {
            var moduleFeature = Configuration[ConfigurationPath.Combine("FeatureManagement", moduleName)];
            var isModuleEnabled = bool.TryParse(moduleFeature, out var moduleFeatureBool) && moduleFeatureBool;
            if (isModuleEnabled)
            {
                using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var dataContext = serviceScope.ServiceProvider.GetRequiredService<TDataContext>();
                if (UseInMemoryDatabase)
                {
                    dataContext.Database.EnsureCreated();
                }
                else
                {
                    dataContext.Database.Migrate();
                }
            }
        }
    }
}