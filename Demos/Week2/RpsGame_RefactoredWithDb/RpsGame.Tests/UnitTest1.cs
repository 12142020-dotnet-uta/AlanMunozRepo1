using System;
using Microsoft.EntityFrameworkCore;
using RpsGame_NoDb;
using Xunit;

namespace RpsGame.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void FourPlusFiveEqualsNine()
        {
            // arrange
            //creating the in-memory Db
            var options = new DbContextOptionsBuilder<RpsDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            int z;
            // act
            // add to the In-Memory Db
            using (var context = new RpsDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RpsGameRepositoryLayer repo = new RpsGameRepositoryLayer(context);
                int x = 4;
                int y = 5;
                z = x + y;

                context.SaveChanges();
            }

            //assert
            // verify the the result was as expected
            using (var context = new RpsDbContext(options))
            {
                Assert.Equal(9, z);
            }
        }


        [Theory/*(Skip = "don't wanna test this right now")*/]
        [InlineData(6, 7)]
        [InlineData(5, 8)]
        [InlineData(4, 9)]
        public void VariousInputsEqualThirteen(int x, int y)
        {
            // act
            int z = x + y;
            // assert
            Assert.Equal(13, z);
        }

        [Theory]
        [InlineData(10, 11)]
        [InlineData(5, 9)]
        public void VariousInputsDoNotEqualThirteen(int x, int y)
        {
            // act
            int z = x + y;

            // assert
            Assert.NotEqual(13, z);
        }

        [Fact]
        public void CreatePlayerSavesANewPlayerToTheDb()
        {
            // arrange
            //creating the in-memory Db
            var options = new DbContextOptionsBuilder<RpsDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            // act
            // add to the In-Memory Db
            Player p1 = new Player();
            using (var context = new RpsDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RpsGameRepositoryLayer repo = new RpsGameRepositoryLayer(context);
                p1 = repo.CreatePlayer("Sparky", "Jones");
                //context.SaveChanges();
            }

            //assert
            // verify the the result was as expected
            using (var context1 = new RpsDbContext(options))
            {
                //context.Database.EnsureCreated();
                RpsGameRepositoryLayer repo = new RpsGameRepositoryLayer(context1);

                Player result = repo.CreatePlayer("Sparky", "Jones");

                Assert.Equal(p1.playerId, result.playerId);
                //Assert.True(p1.playerId.Equals(result.playerId));
                //Assert.True(p1.playerId.CompareTo(result.playerId) == result.playerId.CompareTo(p1.playerId));


            }
        }
    }
}
