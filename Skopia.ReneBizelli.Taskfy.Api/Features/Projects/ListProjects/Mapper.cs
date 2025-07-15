using Skopia.ReneBizelli.Taskfy._Shared.Entities;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.ListProjects;

internal static class Mapper
{
    public static Response Map(this Project project, int userId)
        => new Response 
        { 
            ExternalId = project.ExternalId,
            Name = project.Name,
            CreatedAt = project.CreatedAt,
            Author = project.Author.Map(userId)
        };

    public static Author Map(this User user, int userId)
        => new Author
        {
            UserExternalId = user.ExternalId,
            Name = user.Name,
            Me = user.Id == userId
        };

}

     