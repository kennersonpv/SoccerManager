using Microsoft.EntityFrameworkCore;
using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.DbContexts;
using SoccerManager.Api.Shared.Entities;

namespace SoccerManager.Api.Shared.Repositories.TeamPlayers
{
    public class TeamPlayersRepository : ITeamPlayersRepository
    {
        private readonly DbContextOptions<SoccerManagerDbContext> _dbContext;

        public TeamPlayersRepository(DbContextOptions<SoccerManagerDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<int>> AddTeamPlayerAsync(int playerId, int teamId, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var player = await context.Players.FindAsync(playerId);
                    var team = await context.Teams.FindAsync(teamId);
                    var teamPlayer = new TeamPlayer
                    {
                        Player = player,
                        Team = team,
                        IsPlayer = true
                    };

                    context.TeamPlayers.Add(teamPlayer);
                    var response = await context.SaveChangesAsync();
                    return Result<int>.Success(teamPlayer.Id);
                }
            }
            catch (Exception)
            {

                return Result<int>.Failure(new Error("Insert", "Error to insert TeamPlayer"));
            }
        }

        public async Task<Result<IEnumerable<Player>>> GetPlayersByTeamAsync(int teamId, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var players = await context.TeamPlayers
                                    .Where(tp => tp.Team.Id == teamId && tp.IsPlayer)
                                    .Select(tp => tp.Player)
                                    .ToListAsync();

                    return Result<IEnumerable<Player>>.Success(players);
                }
            }
            catch (Exception)
            {
                return Result<IEnumerable<Player>>.Failure(new Error("Insert", "Error to get Players"));
            }
        }

        public async Task<Result<bool>> GetPlayerByTeamAsync(int teamId, int playerId, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var players = await context.TeamPlayers
                                    .Where(tp => tp.Team.Id == teamId && tp.IsPlayer && tp.Player.Id == playerId)
                                    .Select(tp => tp.Player)
                                    .ToListAsync();
                    if (players != null)
                    {
                        return Result<bool>.Success(true);
                    }

                    return Result<bool>.Failure(new Error("Get", "Player not found"));
                }
            }
            catch (Exception)
            {
                return Result<bool>.Failure(new Error("Get", "Error to get Players"));
            }
        }

        public async Task<Result<bool>> UpdateTeamPlayerAsync(int playerId, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var result = await context.TeamPlayers.SingleOrDefaultAsync(tp => tp.Player.Id == playerId && tp.IsPlayer == true);
                    if (result != null)
                    {
                        result.IsPlayer = false;
                        await context.SaveChangesAsync();

                        return Result<bool>.Success(true);
                    }
                    return Result<bool>.Success(false);
                }
            }
            catch (Exception)
            {
                return Result<bool>.Failure(new Error("Update", "Error to update TeamPlayer"));
            }
        }
    }
}
