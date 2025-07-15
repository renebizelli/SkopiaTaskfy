using MediatR;
using Microsoft.EntityFrameworkCore;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;

namespace Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;

public class UserRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IUserRequest
    where TResponse : notnull
{
    private readonly TaskfyDBContext _taskfyDBContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserRequestBehavior(
        TaskfyDBContext taskfyDBContext,
        IHttpContextAccessor httpContextAccessor )
    {
        _taskfyDBContext = taskfyDBContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userIdHeader = _httpContextAccessor.HttpContext?.Request.Headers["X-User-External-Id"] ?? string.Empty;

        Guid.TryParse(userIdHeader, out var userExternalId);

        var user = await _taskfyDBContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.ExternalId.Equals(userExternalId), cancellationToken);

        if (user == null)
        {
            throw new UnauthorizedAccessException();
        }

        request.UserId = user.Id;

        return await next();
    }
}
