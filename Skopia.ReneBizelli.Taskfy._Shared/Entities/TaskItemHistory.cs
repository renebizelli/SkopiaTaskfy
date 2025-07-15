namespace Skopia.ReneBizelli.Taskfy._Shared.Entities;

public class TaskItemHistory
{
    public int Id { get; set; }
    public int TaskItemId { get; set; }
    public virtual TaskItem? Task { get; set; }
    public int UserId { get; set; }
    public virtual User? User { get; set; }
    public string Data { get; set; } = string.Empty;
    public TaskItemHistoryType Type { get; set; }
    public DateTime CreatedAt { get; set; }
}

public enum TaskItemHistoryType
{
    None = 0,
    Text = 1,
    Json = 2,
}
