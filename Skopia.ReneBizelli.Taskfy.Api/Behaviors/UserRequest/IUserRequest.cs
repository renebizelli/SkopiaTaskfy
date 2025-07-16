using Skopia.ReneBizelli.Taskfy._Shared.Entities;

namespace Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;

public interface IUserRequest
{
    int UserId { get; set; }
    UserRoles UserRole { get; set; }
}   
