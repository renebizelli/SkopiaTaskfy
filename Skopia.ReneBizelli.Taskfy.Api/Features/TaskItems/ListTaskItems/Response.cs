using Skopia.ReneBizelli.Taskfy._Shared.Entities;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.ListTaskItems;

internal record Response
{
    public Guid ExternalId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueAt { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public StatusTaskItem Status { get; set; }
    public PriorityTaskItem Priority { get; set; }
    public User? User { get; set; }
}

internal record User
{
    public Guid ExternalId { get; set; } 
    public string Name { get; set; } = string.Empty;
    public bool Me { get; set; }
}
