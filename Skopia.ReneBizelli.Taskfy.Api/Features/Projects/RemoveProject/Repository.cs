using Microsoft.EntityFrameworkCore;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.QueryExtensions;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.RemoveProject;

internal class Repository
{
    private readonly TaskfyDBContext _context;
    public Repository(TaskfyDBContext context)
    {
        _context = context;
    }

    public async Task RemoveAsync(Project project, CancellationToken cancellationToken)
    {
        project.Active = false;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Project?> GetProjectAsync(Request request, CancellationToken cancellationToken)
    => await _context.Projects.UserScope(request.UserId).FirstOrDefaultAsync(f => f.ExternalId.Equals(request.ExternalId), cancellationToken);

    public async Task<bool> CanRemoveAsync(int projectId, int userId, CancellationToken cancellationToken)
    => await _context.TaskItems.UserScope(userId).AllAsync(f => f.ProjectId.Equals(projectId) && f.Status == StatusTaskItem.Done, cancellationToken);


}
