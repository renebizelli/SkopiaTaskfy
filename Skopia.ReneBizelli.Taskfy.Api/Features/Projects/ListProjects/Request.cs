using MediatR;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.ListProjects;

public record Request : IRequest<Response>, IUserRequest
{
    public int UserId { get; set; }
}
