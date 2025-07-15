using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy.Api.Structure;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddTaskItem;

internal class Handler : IRequestHandler<Request>
{
    private readonly TaskfyDBContext _context;
    private readonly ProjectSettings _settings;

    public Handler(TaskfyDBContext context, IOptions<ProjectSettings> options)
    {
        _context = context;
        _settings = options.Value;
    }

    public async Task Handle(Request request, CancellationToken cancellationToken)
    {
        var taskItem = request.Map();

        await AttachProjectAsync(taskItem, request.ProjectExternalId, cancellationToken);

        await AttachUserResponsibleAsync(taskItem, request.UserExternalId, cancellationToken);

        await _context.TaskItems.AddAsync(taskItem, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task AttachProjectAsync(TaskItem taskItem, Guid projectExternalId, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(f => f.ExternalId.Equals(projectExternalId), cancellationToken);
        taskItem.Project = project;
    }

    private async Task AttachUserResponsibleAsync(TaskItem taskItem, Guid userExternalId, CancellationToken cancellationToken)
    {
        if (!userExternalId.Equals(Guid.Empty))
        {
            var user = await _context.Users.FirstOrDefaultAsync(f => f.ExternalId.Equals(userExternalId), cancellationToken);
            taskItem.User = user;
        }
    }
}
