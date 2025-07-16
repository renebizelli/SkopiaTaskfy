using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skopia.ReneBizelli.Taskfy.Api.Structure;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Reports.Performance;

internal class Endpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointBuilder)
    {
        endpointBuilder.MapGet("/reports/performance/{days}", Handler);
    }

    public async Task<IResult> Handler(ISender sender, [FromRoute] int days, CancellationToken cancellationToken)
    {
        var request = new Request(days);

        var response = await sender.Send(request, cancellationToken);

        return response.Match(
            result => Results.Ok(result),
            error => Results.StatusCode((int)error.httpStatusCode)
        );
    }
}
