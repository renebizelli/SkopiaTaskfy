namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.AddProject;


public static class Injections
{
    public static void AddProjectFeature(this IServiceCollection services)
    {
        services.AddTransient<Repository>();
    }
}
