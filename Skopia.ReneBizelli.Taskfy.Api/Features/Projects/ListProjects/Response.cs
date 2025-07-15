namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.ListProject;

internal record Response
{
    public Guid ExternalId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
