using Microsoft.EntityFrameworkCore;
using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.DbContexts;
using SoccerManager.Api.Shared.Entities;

namespace SoccerManager.Api.Shared.Repositories.Players
{
    public class PlayersRepository : IPlayersRepository
    {
        private readonly DbContextOptions<SoccerManagerDbContext> _dbContext;

        public PlayersRepository(DbContextOptions<SoccerManagerDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<int>> AddPlayerAsync(Player player, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    context.Players.Add(player);
                    var response = await context.SaveChangesAsync();
                    return Result<int>.Success(player.Id);
                }
            }
            catch (Exception)
            {

                return Result<int>.Failure(new Error("Insert", "Error to insert Player"));
            }
        }

        public async Task<Result<Player>> UpdatePlayerAsync(int id, string firstName, string lastName, string country, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var result = await context.Players.SingleOrDefaultAsync(p => p.Id == id);
                    if (result != null)
                    {
                        result.FirstName = firstName;
                        result.LastName = lastName;
                        result.Country = country;
                        await context.SaveChangesAsync();
                    }
                    return Result<Player>.Success(result);
                }
            }
            catch (Exception)
            {
                return Result<Player>.Failure(new Error("Update", "Error to update Player"));
            }
        }
    }
}
