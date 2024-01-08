using SoccerManager.Api.Shared.Abstractions;

namespace SoccerManager.Api.Shared.Repositories.Users
{
    public interface IUsersRepository
    {
        Task<Result<int>> InsertUserAsync(Entities.User user, CancellationToken cancellationToken);
        Task<Result<int>> UserExistsByEmailAsync(string email, CancellationToken cancellationToken);
        Task<Result<bool>> UserExistsByEmailPasswordAsync(string email, string password, CancellationToken cancellationToken);
    }
}
