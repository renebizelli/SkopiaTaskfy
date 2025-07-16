using MediatR;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;
using Skopia.ReneBizelli.Taskfy.Api.Utils;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.ListProjects;

internal record Request : IRequest<ResultMany<Response>>, IUserRequest
{
    public int UserId { get; set; }
    public UserRoles UserRole { get; set; } = UserRoles.None;
}
