using MediatR;
using Microsoft.Extensions.Options;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy.Api.Structure;
using System.Runtime;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.AddProject;

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
        var project = request.Map(_settings.TaskItemsLimit, request.UserId);

        AttachUser(request, project);

        await _context.Projects.AddAsync(project);

        await _context.SaveChangesAsync(cancellationToken);
    }

    private void AttachUser(Request request, Project project)
    {
        var user = new User { Id = request.UserId };
        _context.Users.Attach(user);

        project.Author = user;
        project.Users = new List<User> { user! };
    }
}
