using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Api.Shared.Abstractions;
using SoccerManager.Api.Shared.DbContexts;
using SoccerManager.Api.Shared.Entities;
using SoccerManager.Api.Shared.Model;
using System.Collections.Generic;

namespace SoccerManager.Api.Shared.Repositories.Transfers
{
    public class TransferRepository : ITransferRepository
    {
        private readonly DbContextOptions<SoccerManagerDbContext> _dbContext;

        public TransferRepository(DbContextOptions<SoccerManagerDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<int>> AddTransferAsync(int playerId, long value, int teamId, CancellationToken cancellation)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var player = await context.Players.FindAsync(playerId);
                    var team = await context.Teams.FindAsync(teamId);

                    var transfer = new Transfer()
                    {
                        IsSold = false,
                        Player = player,
                        TeamFrom = team,
                        Value = value
                    };

                    context.Transfers.Add(transfer);
                    var response = await context.SaveChangesAsync();
                    return Result<int>.Success(transfer.Id);
                }
            }
            catch (Exception)
            {
                return Result<int>.Failure(new Error("Insert", "Error to insert Transfer"));
            }
        }

        public async Task<Result<long>> GetPlayerValueAsync(int playerId, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {

                    var transferValue = await context.Transfers.Where(t => t.Id == playerId && t.IsSold == false)
                                                                .Select(t => t.Value)
                                                                .FirstOrDefaultAsync();

                    if (transferValue > 0)
                    {

                        return Result<long>.Success(transferValue);
                    }

                    return Result<long>.Failure(new Error("Get", "Player is not on sale"));
                }
            }
            catch (Exception)
            {
                return Result<long>.Failure(new Error("Get", "Error to get Player value"));
            }
        }

        public async Task<Result<IEnumerable<PlayerModel>>> GetTransferListAsync()
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {
                    var query = from transfer in context.Transfers
                                join playerValue in context.PlayerValues
                                    on transfer.Player.Id equals playerValue.Player.Id
                                where !transfer.IsSold
                                group playerValue by new { transfer.Player, transfer.TeamFrom, transfer.TeamTo } into playerGroup
                                select new PlayerModel
                                {
                                    Id = playerGroup.Key.Player.Id,
                                    FirstName = playerGroup.Key.Player.FirstName,
                                    LastName = playerGroup.Key.Player.LastName,
                                    Country = playerGroup.Key.Player.Country,
                                    Position = playerGroup.Key.Player.Position,
                                    Age = playerGroup.Key.Player.Age,
                                    Value = playerGroup.OrderByDescending(pv => pv.DateTime).FirstOrDefault().Value
                                };

                    return Result<IEnumerable<PlayerModel>>.Success(query.ToList());
                }
            }
            catch (Exception)
            {
                return Result<IEnumerable<PlayerModel>>.Failure(new Error("Insert", "Error to insert Transfer"));
            }
        }

        public async Task<Result<bool>> UpdateTransferAsync(int playerId, int teamId, CancellationToken cancellation)
        {
            try
            {
                using (var context = new SoccerManagerDbContext(_dbContext))
                {

                    var transfer = await context.Transfers.SingleOrDefaultAsync(t => t.Id == playerId && t.IsSold == false);

                    if (transfer != null)
                    {
                        var team = await context.Teams.FindAsync(teamId);

                        transfer.TeamTo = team;
                        transfer.IsSold = true;

                        context.Transfers.Add(transfer);
                        var response = await context.SaveChangesAsync();

                        return Result<bool>.Success(true);
                    }

                    return Result<bool>.Success(false);
                }
            }
            catch (Exception)
            {
                return Result<bool>.Failure(new Error("Insert", "Error to insert Transfer"));
            }
        }
    }
}
