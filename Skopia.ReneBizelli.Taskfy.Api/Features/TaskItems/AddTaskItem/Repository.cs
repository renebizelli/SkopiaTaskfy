using Microsoft.EntityFrameworkCore;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.QueryExtensions;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddTaskItem;

internal class Repository
{
    private readonly TaskfyDBContext _context;
    public Repository(TaskfyDBContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(TaskItem taskItem, Guid userExternalId, CancellationToken cancellationToken)
    {
        await AttachUserResponsibleAsync(taskItem, userExternalId, cancellationToken);

        await _context.TaskItems.AddAsync(taskItem, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Project?> GetProjectAsync(Request request, CancellationToken cancellationToken)
        => await _context.Projects.UserScope(request.UserId).FirstOrDefaultAsync(f => f.ExternalId.Equals(request.ProjectExternalId), cancellationToken);

    public async Task<int> TaskItemCountAsync(Project project, CancellationToken cancellationToken)
        => await _context.TaskItems.Where(w => w.Active).CountAsync(f => f.ProjectId.Equals(project.Id), cancellationToken);


    private async Task AttachUserResponsibleAsync(TaskItem taskItem, Guid userExternalId, CancellationToken cancellationToken)
    {
        if (!userExternalId.Equals(Guid.Empty))
        {
            var user = await _context.Users.FirstOrDefaultAsync(f => f.ExternalId.Equals(userExternalId), cancellationToken);
            taskItem.User = user;
        }
    }

}
