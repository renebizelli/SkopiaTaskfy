using MediatR;
using Microsoft.EntityFrameworkCore;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.QueryExtensions;
using Skopia.ReneBizelli.Taskfy.Api.Utils;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Reports.Performance;


internal class Handler : IRequestHandler<Request, Response>
{
    private readonly TaskfyDBContext _context;

    public Handler(TaskfyDBContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var dateFilter = DateTime.Today.AddDays(request.Days * -1);

        var items = await _context.TaskItems
                            .AsNoTracking()
                            .Where(w => w.UserId.HasValue)
                            .Where(w => w.Status.Equals(StatusTaskItem.Done))
                            .Where(w => w.DoneAt >= dateFilter)
                            .Select(s => new { s.User!.Name, s.User.ExternalId })
                            .GroupBy(g => new { g.Name, g.ExternalId })
                            .Select(s => new PerformaceReportItem
                            {
                                Count = s.Count(),
                                User = new User
                                {
                                    UserExportId = s.Key.ExternalId,
                                    Name = s.Key.Name
                                }
                            })
                            .ToListAsync(cancellationToken);

        var result = new ResultMany<PerformaceReportItem>(items);

        return result;
    }
}
