using Microsoft.EntityFrameworkCore;
using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.DbContexts;
using SoccerManager.Api.Shared.Entities;

namespace SoccerManager.Api.Shared.Repositories.Teams
{
    public class TeamsRepository : ITeamsRepository
    {
        private readonly DbContextOptions<SoccerManagerDbContext> _dbContext;

        public TeamsRepository(DbContextOptions<SoccerManagerDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<int>> AddTeamAsync(Team team, int userId, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var user = await context.Users.FindAsync(userId);
                    team.User = user;

                    context.Teams.Add(team);
                    var response = await context.SaveChangesAsync();
                    return Result<int>.Success(team.Id);
                }
            }
            catch (Exception)
            {

                return Result<int>.Failure(new Error("Insert", "Error to insert Team"));
            }
        }

        public async Task<Result<Team>> GetTeamByUserEmailAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var team = await context.Teams
                                    .Where(team => team.User.Email == email)
                                    .FirstOrDefaultAsync();

                    return Result<Team>.Success(team);
                }
            }
            catch (Exception)
            {
                return Result<Team>.Failure(new Error("Insert", "Error to get Team"));
            }
        }

        public async Task<Result<Team>> UpdateTeamAsync(int id, string name, string country, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var result = await context.Teams.SingleOrDefaultAsync(t => t.Id == id);
                    if(result != null)
                    {
                        result.Name = name;
                        result.Country = country;
                        await context.SaveChangesAsync();
                    }
                    return Result<Team>.Success(result);
                }
            }
            catch (Exception)
            {
                return Result<Team>.Failure(new Error("Insert", "Error to update Team"));
            }
        }
    }
}
