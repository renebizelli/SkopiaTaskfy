using MediatR;
using Microsoft.EntityFrameworkCore;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.QueryExtensions;
using Skopia.ReneBizelli.Taskfy.Api.Utils;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.RemoveProject;

internal class Handler : IRequestHandler<Request, Response>
{
    private readonly TaskfyDBContext _context;

    public Handler(TaskfyDBContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var project = await GetTaskItemAsync(request, cancellationToken);

        if (project == null) return new NotFoundErrorType();

        var canDeleteAsync = await CanDeleteAsync(project.Id, request.UserId, cancellationToken);

        if(!canDeleteAsync)
        {
            return new ConflictErrorType();
        }

        project.Active = false;

        await _context.SaveChangesAsync(cancellationToken);

        return new AcceptType();
    }

    private async Task<Project?> GetTaskItemAsync(Request request, CancellationToken cancellationToken)
        => await _context.Projects.UserScope(request.UserId).FirstOrDefaultAsync(f => f.ExternalId.Equals(request.ExternalId), cancellationToken);

    private async Task<bool> CanDeleteAsync(int projectId, int userId, CancellationToken cancellationToken)
    => await _context.TaskItems.AllAsync(f => f.ProjectId.Equals(projectId) && f.Status == StatusTaskItem.Done, cancellationToken);



}
