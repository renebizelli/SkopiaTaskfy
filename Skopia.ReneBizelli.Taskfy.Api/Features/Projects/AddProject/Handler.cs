using MediatR;
using Microsoft.Extensions.Options;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy.Api.Features.Projects.ListProjects;
using Skopia.ReneBizelli.Taskfy.Api.Structure;
using Skopia.ReneBizelli.Taskfy.Api.Utils;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.AddProject;

internal class Handler : IRequestHandler<Request, Response>
{
    private readonly Repository _repository;
    private readonly ProjectSettings _settings;

    public Handler(Repository repository, IOptions<ProjectSettings> options)
    {
        _repository = repository;
        _settings = options.Value;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var project = request.Map(_settings.TaskItemsLimit, request.UserId);

        await _repository.SaveAsync(project, cancellationToken);

        return new AcceptType();
    }


}
