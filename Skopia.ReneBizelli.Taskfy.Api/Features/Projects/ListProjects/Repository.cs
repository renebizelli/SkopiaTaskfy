using Microsoft.EntityFrameworkCore;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.QueryExtensions;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.ListProjects;

internal class Repository
{
    private readonly TaskfyDBContext _context;

    public Repository(TaskfyDBContext context)
    {
        _context = context;
    }

    public async Task<IList<Project>> ListAsync(int userId, CancellationToken cancellationToken)
       => await _context.Projects
            .Include(i => i.Author)
            .UserScope(userId)
            .IsActive()
            .OrderBy(o => o.Name)
            .ToListAsync(cancellationToken);
}
