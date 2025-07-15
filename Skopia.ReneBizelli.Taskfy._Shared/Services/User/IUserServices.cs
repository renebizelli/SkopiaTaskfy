namespace Skopia.ReneBizelli.Taskfy._Shared.Services.User;

public interface IUserServices
{
    Task<Entities.User?> GetUserExternalIdAsync(Guid userExternalId, CancellationToken cancellationToken);
}
