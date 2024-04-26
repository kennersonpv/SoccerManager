using Microsoft.EntityFrameworkCore;
using Moq;
using SoccerManager.Api.Shared.DbContexts;
using SoccerManager.Api.Shared.Entities;
using SoccerManager.Api.Shared.Repositories.Players;

namespace SoccerManager.Api.Shared.Tests.Repositories.Players
{
    [TestFixture]
    public class PlayersRepositoryTests
    {
        [Test]
        public async Task AddPlayerAsync_ShouldAddPlayerToDatabase()
        {
            // Arrange
            var dbContextOptionsMock = new Mock<DbContextOptions<SoccerManagerDbContext>>();
            var dbContextMock = new Mock<SoccerManagerDbContext>(dbContextOptionsMock.Object);
            var repository = new PlayersRepository(dbContextOptionsMock.Object);
            var player = new Player { FirstName = "John", LastName = "Doe", Country = "USA" };

            // Act
            var result = await repository.AddPlayerAsync(player, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.Greater(result.Value, 0);

            // Verify that Add and SaveChangesAsync were called on the mocked DbContext
            dbContextMock.Verify(db => db.Players.Add(It.IsAny<Player>()), Times.Once);
            dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task UpdatePlayerAsync_ShouldUpdatePlayerInDatabase()
        {
            // Arrange
            var dbContextOptionsMock = new Mock<DbContextOptions<SoccerManagerDbContext>>();
            var dbContextMock = new Mock<SoccerManagerDbContext>(dbContextOptionsMock.Object);
            var repository = new PlayersRepository(dbContextOptionsMock.Object);
            var player = new Player { Id = 1, FirstName = "John", LastName = "Doe", Country = "USA" };

            // Mock the DbSet<Player> and the FindAsync method
            var dbSetMock = new Mock<DbSet<Player>>();
            dbContextMock.Setup(db => db.Players).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.FindAsync(It.IsAny<int>())).ReturnsAsync(player);

            // Act
            var result = await repository.UpdatePlayerAsync(player.Id, "UpdatedJohn", "UpdatedDoe", "Canada", CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);

            // Verify that FindAsync and SaveChangesAsync were called on the mocked DbContext
            dbContextMock.Verify(db => db.Players.FindAsync(It.IsAny<int>()), Times.Once);
            dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            // Verify that player properties were updated
            Assert.AreEqual("UpdatedJohn", player.FirstName);
            Assert.AreEqual("UpdatedDoe", player.LastName);
            Assert.AreEqual("Canada", player.Country);
        }
    }
}