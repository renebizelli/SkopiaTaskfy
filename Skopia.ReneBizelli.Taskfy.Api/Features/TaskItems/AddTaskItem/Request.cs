using MediatR;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;
using System.Text.Json.Serialization;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddTaskItem;

public record Request : IRequest, IUserRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueAt { get; set; } = DateTime.Now;
    public PriorityTaskItem Priority { get; set; }
    public StatusTaskItem Status { get; set; }
    public string UserResponsibleExternalId { get; set; } = string.Empty;
    public int UserId { get; set; }

    [JsonIgnore]
    public string ProjectExternalId { get; set; } = string.Empty;
}


