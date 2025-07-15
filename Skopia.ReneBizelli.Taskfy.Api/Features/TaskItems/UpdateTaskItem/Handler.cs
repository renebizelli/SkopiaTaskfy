using MediatR;
using Microsoft.EntityFrameworkCore;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy.Api.Utils;
using System.Text.Json;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.UpdateTaskItem;

internal class Handler : IRequestHandler<Request, Response>
{
    private readonly TaskfyDBContext _context;

    public Handler(TaskfyDBContext context)
    {
        _context = context;
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
            await AttachUserResponsibleAsync(taskItem, request.UserExternalId, cancellationToken);

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
        => await _context.TaskItems.FirstOrDefaultAsync(f => f.ExternalId.Equals(request.TaskExternalId), cancellationToken);

    private async Task AttachUserResponsibleAsync(TaskItem taskItem, Guid userExternalId, CancellationToken cancellationToken)
    {
        if (!userExternalId.Equals(Guid.Empty))
        {
            var user = await _context.Users.FirstOrDefaultAsync(f => f.ExternalId.Equals(userExternalId), cancellationToken);
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
