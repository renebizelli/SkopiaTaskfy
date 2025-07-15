using MediatR;
using Skopia.ReneBizelli.Taskfy.Api.Structure;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.ListProjects;

internal class Endpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet("/projects", Handler);
    }

    public async Task<IResult> Handler(ISender sender, CancellationToken cancellationToken)
    {
        var request = new Request();

        var response = await sender.Send(request, cancellationToken);

        return Results.Ok(response);
    }
}
