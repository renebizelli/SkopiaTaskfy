using MediatR;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.AddProject;

public record Request : IRequest<Response>, IUserRequest
{
    public int UserId { get; set; }
    public UserRoles UserRole { get; set; } = UserRoles.None;
    public string Name { get; set; } = string.Empty;
}
