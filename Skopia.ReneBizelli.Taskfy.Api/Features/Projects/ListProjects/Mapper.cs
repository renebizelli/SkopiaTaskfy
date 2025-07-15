using Skopia.ReneBizelli.Taskfy._Shared.Entities;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.ListProjects;

internal static class Mapper
{
    public static Response Map(this Project project)
        => new Response 
        { 
            ExternalId = project.ExternalId,
            Name = project.Name,
            CreatedAt = project.CreatedAt
        };

}

     