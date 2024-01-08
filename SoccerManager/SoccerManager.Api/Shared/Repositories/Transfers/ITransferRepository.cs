using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.Entities;
using SoccerManager.Api.Shared.Model;

namespace SoccerManager.Api.Shared.Repositories.Transfers
{
    public interface ITransferRepository
    {
        Task<Result<int>> AddTransferAsync(int playerId, long Value, int teamId, CancellationToken cancellation);
        Task<Result<bool>> UpdateTransferAsync(int playerId, int teamSold, CancellationToken cancellationToken);
        Task<Result<long>> GetPlayerValueAsync(int playerId, CancellationToken cancellationToken);
        Task<Result<IEnumerable<PlayerModel>>> GetTransferListAsync();
    }
}
