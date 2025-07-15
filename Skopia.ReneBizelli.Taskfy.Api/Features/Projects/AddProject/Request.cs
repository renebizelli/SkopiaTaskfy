using MediatR;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.AddProject;

public record Request : IRequest<Response>, IUserRequest
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
}
