using MediatR;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;

namespace Skopia.ReneBizelli.Taskfy.Api.Behaviors.Manager;

public class ManagerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IManagerPermission
    where TResponse : notnull
{
    private readonly TaskfyDBContext _taskfyDBContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ManagerBehavior(
        TaskfyDBContext taskfyDBContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _taskfyDBContext = taskfyDBContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.UserRole != _Shared.Entities.UserRoles.Manager)
        {
            throw new UnauthorizedAccessException();
        }

        return await next();
    }
}

