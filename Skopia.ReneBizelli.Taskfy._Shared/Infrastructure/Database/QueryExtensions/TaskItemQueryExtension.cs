namespace Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.QueryExtensions;

public static class TaskItemQueryExtension
{
    public static IQueryable<Entities.TaskItem> UserScope(this IQueryable<Entities.TaskItem> query, int userId)
        => query.Where(p => p.Project!.Users.Any(a => a.Id == userId));

    public static IQueryable<Entities.TaskItem> IsActive(this IQueryable<Entities.TaskItem> query)
     => query.Where(p => p.Project!.Active && p.Active);
}

