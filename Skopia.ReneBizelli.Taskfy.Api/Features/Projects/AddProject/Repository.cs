using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.AddProject;

internal class Repository
{
    private readonly TaskfyDBContext _context;
    public Repository(TaskfyDBContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(Project project, CancellationToken cancellationToken)
    {
        LinkProjectUser(project);

        await _context.Projects.AddAsync(project);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public void LinkProjectUser(Project project)
    {
        var user = new User { Id = project.AuthorId };
        _context.Users.Attach(user);

        project.Users = new List<User> { user! };
    }
}
