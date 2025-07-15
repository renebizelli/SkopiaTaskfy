using MediatR;
using Skopia.ReneBizelli.Taskfy._Shared.Services.User;

namespace Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;

public class UserRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IUserRequest
    where TResponse : notnull
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserServices _userService;

    public UserRequestBehavior(
        IHttpContextAccessor httpContextAccessor,
        IUserServices userService)
    {
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userIdHeader = _httpContextAccessor.HttpContext?.Request.Headers["X-User-External-Id"] ?? string.Empty;

        Guid.TryParse(userIdHeader, out var userExternalId);

        var user = await _userService.GetUserExternalIdAsync(userExternalId, cancellationToken);

        if (user == null)
        {
            throw new UnauthorizedAccessException();
        }

        request.UserId = user.Id;

        return await next();
    }
}
