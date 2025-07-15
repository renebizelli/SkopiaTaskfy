using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Skopia.ReneBizelli.Taskfy.Api.Structure;

internal static class Endpoints
{
    public static void AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        var descriptors = assembly.DefinedTypes
                    .Where(w => w is { IsAbstract: false, IsInterface: false } &&
                            w.IsAssignableTo(typeof(IEndpoint)))
                    .Select(s => ServiceDescriptor.Transient(typeof(IEndpoint), s))
                    .ToArray();

        services.TryAddEnumerable(descriptors);
    }

    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.AddEndpoint(app);
        }
    }
}
