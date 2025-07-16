using MediatR;
using Microsoft.EntityFrameworkCore;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.QueryExtensions;
using Skopia.ReneBizelli.Taskfy.Api.Utils;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddCommentary;

internal class Handler : IRequestHandler<Request, Response>
{
    private readonly TaskfyDBContext _context;

    public Handler(TaskfyDBContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var taskItem = await _context.TaskItems.UserScope(request.UserId).FirstOrDefaultAsync(a =>  a.ExternalId.Equals(request.TaskItemExternalId) && a.Active);

        if (taskItem == null) return new NotFoundErrorType();

        var history = new TaskItemHistory
        {
            UserId = request.UserId,
            CreatedAt = DateTime.Now,
            Data = request.Commentary,
            Type = TaskItemHistoryType.Text,
            TaskItemId = taskItem.Id,
        };

        await _context.TaskItemHistories.AddAsync(history, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new AcceptType();
    }
}
