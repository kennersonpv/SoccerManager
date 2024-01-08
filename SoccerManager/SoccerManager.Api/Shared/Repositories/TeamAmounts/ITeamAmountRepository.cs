using SoccerManager.Api.Shared.Abstractions;

namespace SoccerManager.Api.Shared.Repositories.TeamAmounts
{
    public interface ITeamAmountRepository
    {
        Task<Result<int>> AddTeamAmountAsync(int teamId, long amount, CancellationToken cancellationToken);
        Task<Result<long>> GetTeamAmountAsync(int teamId, CancellationToken cancellationToken);
    }
}
