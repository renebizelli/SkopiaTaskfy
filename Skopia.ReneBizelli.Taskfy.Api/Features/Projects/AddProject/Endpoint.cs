using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skopia.ReneBizelli.Taskfy.Api.Structure;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.AddProject;

internal class Endpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapPost("/projects", Handler);
    }

    public async Task<IResult> Handler(ISender sender, [FromBody] Request request, CancellationToken cancellationToken)
    {
        var response = await sender.Send(request, cancellationToken);

        return response.Match(
            ok => Results.Ok(),
            error => Results.StatusCode((int)error.httpStatusCode)
        );
    }
}
