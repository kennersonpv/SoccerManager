using Microsoft.EntityFrameworkCore;
using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.DbContexts;
using SoccerManager.Api.Shared.Entities;

namespace SoccerManager.Api.Shared.Repositories.PlayerValues
{
    public class PlayerValueRepository : IPlayerValueRepository
    {
        private readonly DbContextOptions<SoccerManagerDbContext> _dbContext;

        public PlayerValueRepository(DbContextOptions<SoccerManagerDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<int>> AddPlayerValueAsync(int id, long value, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var player = await context.Players.FindAsync(id);

                    var playerValue = new PlayerValue
                    {
                        Player = player,
                        Value = value,
                        DateTime = DateTime.Now
                    };

                    context.PlayerValues.Add(playerValue);
                    var response = await context.SaveChangesAsync();
                    return Result<int>.Success(playerValue.Id);
                }
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(new Error("Insert", "Error to insert PlayerValue"));
            }
        }
    }
}
