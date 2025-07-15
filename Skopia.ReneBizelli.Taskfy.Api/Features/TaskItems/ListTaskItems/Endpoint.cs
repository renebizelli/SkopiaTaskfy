using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skopia.ReneBizelli.Taskfy.Api.Structure;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.ListTaskItems;

internal class Endpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet("/taskitems/{projectExternalId}", Handler);
    }

    public async Task<IResult> Handler(ISender sender, [FromRoute] string projectExternalId, CancellationToken cancellationToken)
    {
        var request = new Request(projectExternalId);

        var response = await sender.Send(request, cancellationToken);

        return Results.Ok(response);
    }
}
