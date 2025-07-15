namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.ListTaskItems;

internal static class Mapper
{
    public static Response Map(this _Shared.Entities.TaskItem taskItem, int userId)
     => new Response
     {
         ExternalId = taskItem.ExternalId,
         Title = taskItem.Title,
         Description = taskItem.Description,
         DueAt = taskItem.DueAt,
         Status = taskItem.Status,
         Priority = taskItem.Priority,
         CreatedAt = taskItem.CreatedAt,
         Responsible = taskItem.User.Map(userId)
     };

    public static Responsible? Map(this _Shared.Entities.User? user, int userId)
    {
        if (user == null) return null;

        return new Responsible
        {
            UserExternalId = user.ExternalId,
            Name = user.Name,
            Me = user.Id == userId
        };
    }
}
