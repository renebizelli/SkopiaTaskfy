using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy.Api.Structure;
using Skopia.ReneBizelli.Taskfy.Api.Utils;
using System.Text.Json;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.UpdateTaskItems;

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
        var taskItem = await GetTaskItemAsync(request, cancellationToken);

        if (taskItem == null) return new NotFoundErrorType();

        request.Map(taskItem!);

        var editedFields = ExtractEditedFields(taskItem);

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await AttachUserResponsibleAsync(taskItem, request.UserResponsibleExternalId, cancellationToken);

            await AddHistoryAsync(editedFields, taskItem, request, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync();

            return new AcceptType();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task<TaskItem?> GetTaskItemAsync(Request request, CancellationToken cancellationToken)
    {
        var externalId = Guid.Parse(request.TaskExternalId);

        var taskItem = await _context.TaskItems.FirstOrDefaultAsync(f => f.ExternalId.Equals(externalId), cancellationToken);
        return taskItem;
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

    private async Task AddHistoryAsync(Dictionary<string, object?>  editedFields, TaskItem taskItem, Request request, CancellationToken cancellationToken)
    {
        if (editedFields.Any()) {

            var history = new TaskItemHistory
            {
                UserId = request.UserId,
                CreatedAt = DateTime.Now,
                Data = JsonSerializer.Serialize(editedFields),
                Type = TaskItemHistoryType.Json,
                TaskItemId = taskItem.Id,
            };

            await _context.TaskItemHistories.AddAsync(history, cancellationToken);
        }
    }

    private Dictionary<string, object?> ExtractEditedFields(TaskItem taskItem)
    {
        var editedFields = new Dictionary<string, object?>();

        var entry = _context.Entry(taskItem);

        foreach (var prop in entry.Properties)
        {
            if (prop.IsModified)
            {
                editedFields[prop.Metadata.Name] = prop.CurrentValue;
            }
        }

        return editedFields;
    }
}
