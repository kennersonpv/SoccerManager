using Microsoft.EntityFrameworkCore;
using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.DbContexts;
using SoccerManager.Api.Shared.Repositories.Users;

namespace SoccerManager.Api.Shared.Repositories.User
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DbContextOptions<SoccerManagerDbContext> _dbContext;

        public UsersRepository(DbContextOptions<SoccerManagerDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<int>> InsertUserAsync(Entities.User user, CancellationToken cancellation)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    context.Users.Add(user);
                    var response = await context.SaveChangesAsync();
                    return Result<int>.Success(user.Id);
                }
            }
            catch (Exception)
            {

                return Result<int>.Failure(new Error("Insert", "Error to insert User"));
            }
        }

        public async Task<Result<int>> UserExistsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var user = await context.Users.SingleOrDefaultAsync(u => u.Email == email);

                    if (user != null)
                    {
                        return Result<int>.Success(user.Id);
                    }

                    return Result<int>.Failure(new Error("Get", "User not found"));
                }
            }
            catch (Exception)
            {
                return Result<int>.Failure(new Error("Get", "Error to get User"));
            }
        }

        public async Task<Result<bool>> UserExistsByEmailPasswordAsync(string email, string password, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var user = await context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);

                    if (user != null)
                    {
                        return Result<bool>.Success(true);
                    }

                    return Result<bool>.Failure(new Error("Get", "User not found"));
                }
            }
            catch (Exception)
            {
                return Result<bool>.Failure(new Error("Get", "Error to get User"));
            }
        }
    }
}
