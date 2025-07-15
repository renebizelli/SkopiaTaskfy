using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skopia.ReneBizelli.Taskfy.Api.Structure;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddCommentary;

internal class Endpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapPost("/taskitems/commentary/{taskItemExternalId}", Handler);
    }

    public async Task<IResult> Handler(ISender sender, [FromRoute] Guid taskItemExternalId, [FromBody] Request request, CancellationToken cancellationToken)
    {
        request.TaskItemExternalId = taskItemExternalId;

        await sender.Send(request, cancellationToken);

        return Results.Ok();
    }
}
