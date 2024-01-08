using Microsoft.EntityFrameworkCore;
using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.DbContexts;
using SoccerManager.Api.Shared.Entities;

namespace SoccerManager.Api.Shared.Repositories.TeamAmounts
{
    public class TeamAmountRepository : ITeamAmountRepository
    {
        private readonly DbContextOptions<SoccerManagerDbContext> _dbContext;

        public TeamAmountRepository(DbContextOptions<SoccerManagerDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<int>> AddTeamAmountAsync(int teamId, long amount, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var team = await context.Teams.FindAsync(teamId);

                    var teamAmount = new TeamAmount
                    {
                        Team = team,
                        Amount = amount,
                        DateTime = DateTime.Now
                    };

                    context.TeamAmounts.Add(teamAmount);
                    var response = await context.SaveChangesAsync();
                    return Result<int>.Success(teamAmount.Id);
                }
            }
            catch (Exception)
            {

                return Result<int>.Failure(new Error("Insert", "Error to insert TeamAmount"));
            }
        }

        public async Task<Result<long>> GetTeamAmountAsync(int teamId, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var lastAmount = await context.TeamAmounts
                                    .Where(ta => ta.Team.Id == teamId)
                                    .OrderByDescending(ta => ta.DateTime)
                                    .FirstOrDefaultAsync();

                    return Result<long>.Success(lastAmount.Amount);
                }
            }
            catch (Exception)
            {

                return Result<long>.Failure(new Error("Insert", "Error to get TeamAmount"));
            }
        }
    }
}
