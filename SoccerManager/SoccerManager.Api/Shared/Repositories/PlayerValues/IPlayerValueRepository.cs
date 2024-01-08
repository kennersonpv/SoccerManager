using SoccerManager.Api.Shared.Abstractions;

namespace SoccerManager.Api.Shared.Repositories.PlayerValues
{
    public interface IPlayerValueRepository
    {
        Task<Result<int>> AddPlayerValueAsync(int playerId, long playerValue, CancellationToken cancellationToken);
    }
}
