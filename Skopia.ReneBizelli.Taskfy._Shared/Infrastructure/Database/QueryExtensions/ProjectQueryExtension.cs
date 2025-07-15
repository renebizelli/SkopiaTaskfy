namespace Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.QueryExtensions;

public static class ProjectQueryExtension
{
    public static IQueryable<Entities.Project> UserScope(this IQueryable<Entities.Project> query, int userId)
        => query.Where(p => p.Users.Any(a => a.Id == userId));

    public static IQueryable<Entities.Project> IsActive(this IQueryable<Entities.Project> query)
     => query.Where(p => p.Active);
}

