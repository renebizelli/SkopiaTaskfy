using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;

namespace Skopia.ReneBizelli.Taskfy._Shared.Services.User;

internal class UserServices : IUserServices
{
    private readonly TaskfyDBContext _context;
    private readonly IMemoryCache _cache;

    public UserServices(
        TaskfyDBContext context,
        IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<Entities.User?> GetUserExternalIdAsync(Guid userExternalId, CancellationToken cancellationToken)
    {
        if (userExternalId == Guid.Empty) return null;

        return await _cache.GetOrCreateAsync($"user-external-id:{userExternalId}", async e =>
        {
            e.SlidingExpiration = TimeSpan.FromMinutes(30);
            e.SetPriority(CacheItemPriority.Low);

            var user = await GetFromDbAsync(userExternalId, cancellationToken); 

            e.Value = user;

            return user;
        });
    }

    private async Task<Entities.User?> GetFromDbAsync(Guid userExternalId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(u => u.ExternalId == userExternalId && u.Active)
            .FirstOrDefaultAsync(cancellationToken);
    }
}

public static class UserServicesConfiguration
{
    public static void AddUserService(this IServiceCollection services)
    {
        services.AddTransient<IUserServices, UserServices>();
    }
}
