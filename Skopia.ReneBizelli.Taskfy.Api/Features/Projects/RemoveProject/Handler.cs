using MediatR;
using Skopia.ReneBizelli.Taskfy.Api.Utils;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.RemoveProject;

internal class Handler : IRequestHandler<Request, Response>
{
    private readonly Repository _repository;

    public Handler(Repository repository)
    {
        _repository = repository;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetProjectAsync(request, cancellationToken);

        if (project == null) return new NotFoundErrorType();

        var removable = await _repository.CanRemoveAsync(project.Id, request.UserId, cancellationToken);

        if(!removable) return new ConflictErrorType();

        await _repository.RemoveAsync(project, cancellationToken);

        return new AcceptType();
    }




}
