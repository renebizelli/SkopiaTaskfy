using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skopia.ReneBizelli.Taskfy.Api.Structure;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.RemoveProject;

internal class Endpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapDelete("/projects/{externalId}", Handler);
    }

    public async Task<IResult> Handler(ISender sender, [FromRoute] Guid externalId, CancellationToken cancellationToken)
    {
        var request = new Request(externalId);

        var response = await sender.Send(request, cancellationToken);

        return response.Match(
            ok => Results.Ok(),
              error => Results.StatusCode((int)error.httpStatusCode)
        );
    }
}
