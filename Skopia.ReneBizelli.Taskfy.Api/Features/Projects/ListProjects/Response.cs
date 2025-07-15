namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.ListProjects;

internal record Response
{
    public Guid ExternalId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
