using MediatR;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.Manager;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Reports.Performance;

public record Request : IRequest<Response>, IManagerPermission
{
    public int Days { get; set; }
    public int UserId { get ; set; }
    public UserRoles UserRole { get; set; } = UserRoles.None;

    public Request(int days)
    {
        Days = days;
    }
}
