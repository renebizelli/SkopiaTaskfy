using MediatR;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;
using System.Text.Json.Serialization;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddCommentary;

public record Request : IRequest<Response>, IUserRequest
{
    public string Commentary { get; set; } = string.Empty;
    public int UserId { get; set; }

    [JsonIgnore]
    public Guid TaskItemExternalId { get; set; } = Guid.Empty;
}


