using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.Entities;

namespace SoccerManager.Api.Shared.Repositories.Teams
{
    public interface ITeamsRepository
    {
        Task<Result<int>> AddTeamAsync(Team team, int userId, CancellationToken cancellationToken);
        Task<Result<Team>> UpdateTeamAsync(int id, string name, string country, CancellationToken cancellationToken);
        Task<Result<Team>> GetTeamByUserEmailAsync(string email, CancellationToken cancellationToken);
    }
}
