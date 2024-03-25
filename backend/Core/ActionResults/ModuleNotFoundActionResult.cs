using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Core.ActionResults;

public class ModuleNotFoundActionResult : ActionResult
{
    public ModuleNotFoundActionResult(IEnumerable<string> moduleNames)
    {
        ModuleNames = moduleNames;
    }

    public IEnumerable<string> ModuleNames { get; }

    public override async Task ExecuteResultAsync(ActionContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var exceptionMessage = $"Some of the required modules were not found. Missing modules: {string.Join(',', ModuleNames)}.";
        context.HttpContext.Response.StatusCode = 501;
        context.HttpContext.Response.ContentType = MediaTypeNames.Text.Plain;
        await context.HttpContext.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(exceptionMessage));
    }
}
