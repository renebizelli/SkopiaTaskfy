using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skopia.ReneBizelli.Taskfy.Api.Structure;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.UpdateTaskItem;

internal class Endpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapPut("/taskitems/{taskItemExternalId}", Handler);
    }

    public async Task<IResult> Handler(ISender sender, [FromRoute] string taskItemExternalId, [FromBody] Request request, CancellationToken cancellationToken)
    {
        request.TaskExternalId = taskItemExternalId;

        var response = await sender.Send(request, cancellationToken);

        return response.Match (
            ok => Results.Ok(),
            error => Results.StatusCode((int)error.httpStatusCode)
        );
    }
}
