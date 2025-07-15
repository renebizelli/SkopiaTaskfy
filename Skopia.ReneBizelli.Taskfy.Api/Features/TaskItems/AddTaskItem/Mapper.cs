using Skopia.ReneBizelli.Taskfy._Shared.Entities;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddTaskItem;

public static class Mapper
{
    public static TaskItem Map(this Request request)
     => new TaskItem
     {
         ExternalId = Guid.NewGuid(),
         Title = request.Title,
         Description = request.Description,
         DueAt = request.DueAt,
         CreatedAt = DateTime.Now,
         Active = true,
         Priority = request.Priority,
         Status = request.Status,
     };

}
