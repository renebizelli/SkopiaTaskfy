using MediatR;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.Manager;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.RemoveProject;

public class Request : IRequest<Response>, IUserRequest, IManagerPermission
{
    public Guid ExternalId { get; set; } = Guid.Empty;
    public UserRoles UserRole { get; set; } = UserRoles.None;
    public int UserId { get; set; }

    public Request(Guid externalId)
    {
        ExternalId = externalId;
    }
}
