using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.QueryExtensions;
using Skopia.ReneBizelli.Taskfy.Api.Structure;
using Skopia.ReneBizelli.Taskfy.Api.Utils;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddTaskItem;

internal class Handler : IRequestHandler<Request, Response>
{
    private readonly TaskfyDBContext _context;
    private readonly ProjectSettings _settings;

    public Handler(TaskfyDBContext context, IOptions<ProjectSettings> options)
    {
        _context = context;
        _settings = options.Value;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var taskItem = request.Map();

        var allowedInProject = _context.Projects.Any(a => a.Users.Any( a => a.Id.Equals(request.UserId)));

        if (!allowedInProject) return new NotFoundErrorType();

        var project = await GetProjectAsync(request, cancellationToken);

        if (project == null) return new NotFoundErrorType();

        var allowedAddTasks = CanAddTaskItemAsync(project, cancellationToken);

        if (allowedAddTasks == null) return new ConflictErrorType();

        taskItem.ProjectId = project.Id;

        await AttachUserResponsibleAsync(taskItem, request.UserExternalId, cancellationToken);

        await _context.TaskItems.AddAsync(taskItem, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);

        return new AcceptType();
    }

    private async Task<Project?> GetProjectAsync(Request request, CancellationToken cancellationToken)
        => await _context.Projects.UserScope(request.UserId).FirstOrDefaultAsync(f => f.ExternalId.Equals(request.ProjectExternalId), cancellationToken);

    private async Task AttachUserResponsibleAsync(TaskItem taskItem, Guid userExternalId, CancellationToken cancellationToken)
    {
        if (!userExternalId.Equals(Guid.Empty))
        {
            var user = await _context.Users.FirstOrDefaultAsync(f => f.ExternalId.Equals(userExternalId), cancellationToken);
            taskItem.User = user;
        }
    }

    private async Task<bool> CanAddTaskItemAsync(Project project,  CancellationToken cancellationToken)
    { 
        var count = await _context.TaskItems.Where(w => w.Active).CountAsync(f => f.ProjectId.Equals(project.Id), cancellationToken);

        return count < project!.TaskItemsLimit;
    }
}
