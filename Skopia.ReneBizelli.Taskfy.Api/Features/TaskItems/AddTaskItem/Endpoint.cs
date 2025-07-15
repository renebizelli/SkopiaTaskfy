using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skopia.ReneBizelli.Taskfy.Api.Structure;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddTaskItem;

internal class Endpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapPost("/taskitems/projects/{projectExternalId}", Handler);
    }

    public async Task<IResult> Handler(ISender sender, [FromRoute] Guid projectExternalId, [FromBody] Request request, CancellationToken cancellationToken)
    {
        request.ProjectExternalId = projectExternalId;

        await sender.Send(request, cancellationToken);

        return Results.Ok();
    }
}
