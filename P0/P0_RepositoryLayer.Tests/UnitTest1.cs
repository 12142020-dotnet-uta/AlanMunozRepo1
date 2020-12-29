using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using P0_RepositoryLayer.Models;
using System.Linq;

namespace P0_RepositoryLayer.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("Gabriel", "Schroeder")]// Add new user
        [InlineData("Alan", "Munoz")] // Already one created
        [InlineData("Gabriel1", "")] // something to get a error....
        public void AddCustomerToDatabase(string strFirstName, string strLastName)
        {
            //Arrange
            DbContextOptions<StoreDbContext> options =  new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            string CustomerLogInRequest = "";

            //Act
            using (StoreDbContext dbContext = new StoreDbContext( options ))
            {
                P0_RLayer myRepoLayer = new P0_RLayer( dbContext );

                CustomerLogInRequest = myRepoLayer.CreateCustomer();
            }

            //Assert
            using (StoreDbContext dbContext = new StoreDbContext( options ))
            {
                P0_RLayer myRepoLayer = new P0_RLayer( dbContext );

                bool customerIsInDB = dbContext.Customers.ToList().Exists( x => x.FirstName == strFirstName && x.LastName == strLastName );

                Assert.Equal(true, customerIsInDB);
            }

        }

    }
}
