using MediatR;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.ListProjects;

internal class Handler : IRequestHandler<Request, Response>
{
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    { 
        return new Response();
    }
}
