using Microsoft.EntityFrameworkCore;
using SoccerManager.Api.Shared.Entities;

namespace SoccerManager.Api.Shared.DbContexts
{
    public class SoccerManagerDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerValue> PlayerValues { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TeamAmount> TeamAmounts { get; set; }


        public SoccerManagerDbContext(DbContextOptions<SoccerManagerDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
    }
}
