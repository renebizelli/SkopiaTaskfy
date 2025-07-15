using MediatR;
using Microsoft.EntityFrameworkCore;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy.Api.Utils;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.QueryExtensions;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.ListProjects;

internal class Handler : IRequestHandler<Request, ResultMany<Response>>
{
    private readonly TaskfyDBContext _context;
    public Handler(TaskfyDBContext context)
    {
        _context = context;
    }
    public async Task<ResultMany<Response>> Handle(Request request, CancellationToken cancellationToken)
    {
        var projects = await _context.Projects
            .Include(i => i.Author)
            .UserScope(request.UserId)
            .Where(s => s.Active)
            .IsActive()
            .Select(s => s.Map(request.UserId))
            .ToListAsync(cancellationToken);

        var results = new ResultMany<Response>(projects);

        return results;
    }

}
