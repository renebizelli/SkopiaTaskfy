using MediatR;
using Microsoft.EntityFrameworkCore;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.QueryExtensions;
using Skopia.ReneBizelli.Taskfy.Api.Utils;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.ListTaskItems;

internal class Handler : IRequestHandler<Request, ResultMany<Response>>
{
    private readonly TaskfyDBContext _context;

    public Handler(TaskfyDBContext context)
    {
        _context = context;
    }
    public async Task<ResultMany<Response>> Handle(Request request, CancellationToken cancellationToken)
    {
        var taskItems = await _context.TaskItems.Include(i => i.User)
            .UserScope(request.UserId)
            .Where(s => s.Active)
            .Where(w => w.Project!.ExternalId == request.ProjecExternalId)
            .IsActive()
            .Select(s =>  s.Map(request.UserId))
            .ToListAsync();

        var results = new ResultMany<Response>(taskItems);

        return results;
    }
}
