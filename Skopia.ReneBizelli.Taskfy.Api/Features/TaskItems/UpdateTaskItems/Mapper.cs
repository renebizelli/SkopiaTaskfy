using Skopia.ReneBizelli.Taskfy._Shared.Entities;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.UpdateTaskItems;

public static class Mapper
{
    public static void Map(this Request request, TaskItem taskItem)
     {
        taskItem.Title = request.Title;
        taskItem.Description = request.Description;
        taskItem.DueAt = request.DueAt;
        taskItem.Status = request.Status;
     }

}
