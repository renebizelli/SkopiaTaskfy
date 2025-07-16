namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.ListProjects;

public static class Injections
{
    public static void AddListProjectFeature(this IServiceCollection services)
    {
        services.AddTransient<Repository>();
    }
}
