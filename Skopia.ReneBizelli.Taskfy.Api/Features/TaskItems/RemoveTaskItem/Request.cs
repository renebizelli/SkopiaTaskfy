using MediatR;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.RemoveTaskItem;

public class Request : IRequest<Response>, IUserRequest
{
    public Guid ExternalId { get; set; } = Guid.Empty;
    public int UserId { get; set; }
    public UserRoles UserRole { get; set; } = UserRoles.None;

    public Request(Guid externalId)
    {
        ExternalId = externalId;
    }
}
