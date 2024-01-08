using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.Entities;

namespace SoccerManager.Api.Shared.Repositories.TeamPlayers
{
    public interface ITeamPlayersRepository
    {
        Task<Result<int>> AddTeamPlayerAsync(int playerId, int teamId, CancellationToken cancellationToken);
        Task<Result<bool>> UpdateTeamPlayerAsync(int playerId, int TeamId, CancellationToken cancellationToken);
        Task<Result<IEnumerable<Player>>> GetPlayersByTeamAsync(int teamId, CancellationToken cancellationToken);
        Task<Result<bool>> GetPlayerByTeamAsync(int teamId, int playerId, CancellationToken cancellationToken);
    }
}
