using MediatR;

namespace Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;

public class UserRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IUserRequest
    where TResponse : notnull
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserRequestBehavior(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userIdHeader = _httpContextAccessor.HttpContext?.Request.Headers["X-User-External-Id"] ?? string.Empty;

        request.UserId = 1;

        return await next();
    }
}
