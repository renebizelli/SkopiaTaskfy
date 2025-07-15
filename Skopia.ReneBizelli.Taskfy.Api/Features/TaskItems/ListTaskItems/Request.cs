using MediatR;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;
using Skopia.ReneBizelli.Taskfy.Api.Utils;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.ListTaskItems;

internal record Request : IRequest<ResultMany<Response>>, IUserRequest
{
    public int UserId { get; set; }
    public Guid ProjecExternalId { get; init; }

    public Request(string projecExternalId)
    {
        ProjecExternalId = Guid.Parse(projecExternalId);
    }
}
