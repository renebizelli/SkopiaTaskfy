using MediatR;
using Skopia.ReneBizelli.Taskfy.Api.Utils;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.ListProjects;

internal class Handler : IRequestHandler<Request, ResultMany<Response>>
{
    private readonly Repository _repository;
    public Handler(Repository context)
    {
        _repository = context;
    }

    public async Task<ResultMany<Response>> Handle(Request request, CancellationToken cancellationToken)
    {
        var projects = await _repository.ListAsync(request.UserId, cancellationToken);

        var results = new ResultMany<Response>(projects.Select(s => s.Map(request.UserId)));

        return results;
    }

}
