using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy.Api.Structure;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddTaskItems;

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

        await AttachUserResponsibleAsync(taskItem, request.UserResponsibleExternalId, cancellationToken);

        await _context.TaskItems.AddAsync(taskItem, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task AttachProjectAsync(TaskItem taskItem, string projectExternalId, CancellationToken cancellationToken)
    {
        var externalId = Guid.Parse(projectExternalId);
        var project = await _context.Projects.FirstOrDefaultAsync(f => f.ExternalId.Equals(externalId), cancellationToken);
        taskItem.Project = project;
    }

    private async Task AttachUserResponsibleAsync(TaskItem taskItem, string userResponsibleExternalId, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(userResponsibleExternalId))
        {
            var externalId = Guid.Parse(userResponsibleExternalId);
            var user = await _context.Users.FirstOrDefaultAsync(f => f.ExternalId.Equals(externalId), cancellationToken);
            taskItem.User = user;
        }
    }
}
