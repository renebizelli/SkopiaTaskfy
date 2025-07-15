using MediatR;
using Microsoft.EntityFrameworkCore;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.QueryExtensions;
using Skopia.ReneBizelli.Taskfy.Api.Utils;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.RemoveTaskItem;

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

        taskItem.Active = false;

        await _context.SaveChangesAsync(cancellationToken);

        return new AcceptType();
    }

    private async Task<TaskItem?> GetTaskItemAsync(Request request, CancellationToken cancellationToken)
        => await _context.TaskItems.UserScope(request.UserId).FirstOrDefaultAsync(f => f.ExternalId.Equals(request.ExternalId), cancellationToken);
 
}
