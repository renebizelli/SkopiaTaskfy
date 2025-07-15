using MediatR;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.RemoveProject;

public class Request : IRequest<Response>, IUserRequest
{
    public Guid ExternalId { get; set; } = Guid.Empty;
    public int UserId { get; set; }

    public Request(Guid externalId)
    {
        ExternalId = externalId;
    }
}
