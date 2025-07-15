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
    public string UserResponsibleExternalId { get; set; } = string.Empty;
    public string TaskExternalId { get; set; } = string.Empty;
    public int UserId { get; set; }
}


