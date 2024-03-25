using System.Reflection;
using MediatR;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using Core.Abstractions.HandlerResults;
using Core.Abstractions.ServiceBus;

namespace Core.ServiceBus;

public class ServiceBus : IServiceBus
{
    private readonly IMediator mediator;
    private readonly List<Type> registeredTypes = new();
    private readonly IFeatureManager featureManager;

    public ServiceBus(IMediator mediator, IFeatureManager featureManager)
    {
        this.mediator = mediator;
        this.featureManager = featureManager;

        RegisterTypes();
    }

    public async Task<IHandlerResult<TResponse>> SendAsync<TRequest, TResponse>(TRequest payload, CancellationToken cancellationToken = default)
        where TRequest : IServiceBusRequest<TResponse>
        where TResponse : class
    {
        if (registeredTypes.Contains(typeof(TRequest)))
        {
            var response = await mediator.Send(payload, cancellationToken);

            if (response is IHandlerResult<TResponse> handlerResult)
            {
                return handlerResult;
            }

            return new ServiceBusResponse<TResponse>(response?.ToString() ?? string.Empty);
        }

        return new ModuleUnavailableResponse<TResponse>();
    }

    public async Task<IServiceBusNotificationResponse> Publish<TNotification>(
        TNotification payload,
        CancellationToken cancellationToken = default)
        where TNotification : IServiceBusNotification
    {
        if (registeredTypes.Contains(payload.GetType()))
        {
            try
            {
                await mediator.Publish(payload, cancellationToken);
            }
            catch (Exception ex)
            {
                return new ServiceBusNotificationResponse
                {
                    Success = false,
                    Message = ex.Message,
                };
            }

            return new ServiceBusNotificationResponse
            {
                Success = true,
                Message = null,
            };
        }

        return new ListenersModulesAreUnavailableNotificationResponse();
    }

    private static IEnumerable<Type> GetTypesWithFeatureGateAttribute(Assembly assembly)
    {
        return assembly.GetTypes().Where(type => type.GetCustomAttributes(typeof(FeatureGateAttribute), true).Length > 0);
    }

    private void RegisterTypes()
    {
        var featuredTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(GetTypesWithFeatureGateAttribute);
        foreach (var type in featuredTypes)
        {
            var featureGates = (IEnumerable<FeatureGateAttribute>)type.GetCustomAttributes(typeof(FeatureGateAttribute), true);
            if (featureGates.All(featureGate =>
                    featureGate.RequirementType == RequirementType.All ?
                        featureGate.Features.All(feature =>
                            featureManager.IsEnabledAsync(feature).Result) :
                        featureGate.Features.Any(feature =>
                            featureManager.IsEnabledAsync(feature).Result)))
            {
                registeredTypes.Add(type);
            }
        }
    }
}
