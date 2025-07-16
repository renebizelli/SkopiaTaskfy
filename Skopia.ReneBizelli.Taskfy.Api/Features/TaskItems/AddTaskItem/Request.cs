using MediatR;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;
using System.Text.Json.Serialization;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddTaskItem;

public record Request : IRequest<Response>, IUserRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueAt { get; set; } = DateTime.Now;
    public PriorityTaskItem Priority { get; set; }
    public StatusTaskItem Status { get; set; }
    public string ResponsibleExternalId { get; set; } = string.Empty;
    public Guid UserExternalId
    {
        get
        {
            Guid.TryParse(ResponsibleExternalId, out var guid);
            return guid;
        }
    }
    public int UserId { get; set; }
    public UserRoles UserRole { get; set; } = UserRoles.None;

    [JsonIgnore]
    public Guid ProjectExternalId { get; set; } = Guid.Empty;
}


