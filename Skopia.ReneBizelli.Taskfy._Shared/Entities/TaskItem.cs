namespace Skopia.ReneBizelli.Taskfy._Shared.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueAt { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool Active { get; set; }
    public StatusTaskItem Status { get; set; }
    public PriorityTaskItem Priority { get; set; }
    public int ProjectId { get; set; }
    public virtual Project? Project { get; set; }

    public ICollection<TaskItemHistory> History { get; set; } = new List<TaskItemHistory>();

    public int? UserId { get; set; }
    public virtual User? User { get; set; }
}

public enum StatusTaskItem : byte
{
    None = 0,
    Pending = 1,
    Doing = 2,
    Done = 3
}

public enum PriorityTaskItem : byte
{
    None = 0,
    Low = 1,
    Medium = 2,
    High = 3
}