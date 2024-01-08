using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.Entities;

namespace SoccerManager.Api.Shared.Repositories.Players
{
    public interface IPlayersRepository
    {
        Task<Result<int>> AddPlayerAsync (Player player, CancellationToken cancellationToken);
        Task<Result<Player>> UpdatePlayerAsync(int id, string firstName, string lastName, string country, CancellationToken cancellationToken);
    }
}
