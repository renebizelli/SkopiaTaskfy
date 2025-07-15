using MediatR;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.AddProject;

internal record Request : IRequest, IUserRequest
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
}
