using Skopia.ReneBizelli.Taskfy._Shared.Entities;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.AddProject;

internal static class Mapper
{
    public static Project Map(this Request request, int taskLimit, int userId) =>
        new Project
        {
            ExternalId = Guid.NewGuid(),
            Name = request.Name,
            Active = true,
            CreatedAt = DateTime.Now,
            TaskItemsLimit = taskLimit,
            AuthorId = userId
        };
}
