using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement.Mvc;
using Core.ActionResults;

namespace Core.Handlers;

public class DisabledModuleFeaturesHandler : IDisabledFeaturesHandler
{
    public Task HandleDisabledFeatures(IEnumerable<string> features, ActionExecutingContext context)
    {
        context.Result = new ModuleNotFoundActionResult(features);
        return Task.CompletedTask;
    }
}