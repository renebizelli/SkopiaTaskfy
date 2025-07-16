using MediatR;
using Microsoft.Extensions.Options;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy.Api.Structure;
using Skopia.ReneBizelli.Taskfy.Api.Utils;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddTaskItem;

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
        var project = await _repository.GetProjectAsync(request, cancellationToken);

        if (project == null) return new NotFoundErrorType();

        var canAddTaskItem = CanAddTaskItemAsync(project, cancellationToken);

        if (canAddTaskItem == null) return new ConflictErrorType();

        var taskItem = request.Map(project.Id);

        await _repository.SaveAsync(taskItem, request.UserExternalId, cancellationToken); 

        return new AcceptType();
    }

    private async Task<bool> CanAddTaskItemAsync(Project project,  CancellationToken cancellationToken)
    { 
        var count = await _repository.TaskItemCountAsync(project, cancellationToken);

        return count < project!.TaskItemsLimit;
    }
}
