namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddTaskItem;

public static class Injections
{
    public static void AddTaskItemFeature(this IServiceCollection services)
    {
        services.AddTransient<Repository>();
    }
}
