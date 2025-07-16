using MediatR;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.UpdateTaskItem;

public record Request : IRequest<Response>, IUserRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueAt { get; set; } = DateTime.Now;
    public StatusTaskItem Status { get; set; }
    public string ResponsibleExternalId { get; set; } = string.Empty;
    public Guid TaskExternalId { get; set; } = Guid.Empty;
    public int UserId { get; set; }
    public UserRoles UserRole { get; set; } = UserRoles.None;

    public Guid UserExternalId
    {
        get
        {
            Guid.TryParse(ResponsibleExternalId, out var guid);
            return guid;
        }
    }
}


