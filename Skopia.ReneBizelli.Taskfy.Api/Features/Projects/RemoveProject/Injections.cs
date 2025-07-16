namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.RemoveProject;

public static class Injections
{
    public static void AddRemoveproject(this IServiceCollection services)
    {
        services.AddTransient<Repository>();
    }
}
