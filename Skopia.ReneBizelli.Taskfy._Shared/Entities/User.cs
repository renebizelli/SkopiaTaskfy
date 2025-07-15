namespace Skopia.ReneBizelli.Taskfy._Shared.Entities;

public class User
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRoles Role { get; set; }
    public bool Active { get; set; }

    public ICollection<Project> AuthoredProjects { get; set; } = new List<Project>();

    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    public ICollection<TaskItemHistory> History { get; set; } = new List<TaskItemHistory>();
}

public enum UserRoles : byte
{
    None = 0,
    Analyst = 1,
    Manager = 2
}
