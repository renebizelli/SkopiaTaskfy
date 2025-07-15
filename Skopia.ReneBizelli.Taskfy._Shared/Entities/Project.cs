namespace Skopia.ReneBizelli.Taskfy._Shared.Entities;

public record Project
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TaskItemsLimit { get; set; } = 0;
    public bool Active { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    public ICollection<User> Users { get; set; } = new List<User>();
}

