using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using P0_RepositoryLayer.Models;
using System.Linq;
using P0_CLibrary.Models;

namespace P0_RepositoryLayer.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("Gabriel", "Schroeder")]
        [InlineData("None", "_")]
        [InlineData("Lorem", "Ipsum")]
        public void AddCustomerToDatabase(string strFirstName, string strLastName)
        {
            //Arrange
            DbContextOptions<StoreDbContext> options =  new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            Customer myGeneratedCustomer = new Customer();

            //Act
            using (StoreDbContext dbContext = new StoreDbContext( options ))
            {
                P0_RLayer myRepoLayer = new P0_RLayer( dbContext );

                myRepoLayer.CreateCustomer();
                myGeneratedCustomer = myRepoLayer.Customers.FirstOrDefault( x => x.ToString() == $"{strFirstName} {strLastName}" );
            }

            //Assert
            using (StoreDbContext dbContext = new StoreDbContext( options ))
            {
                P0_RLayer myRepoLayer = new P0_RLayer( dbContext );

                Customer DbCustomer = dbContext.Customers.FirstOrDefault(  x => x.ToString() == $"{strFirstName} {strLastName}");

                Assert.Equal(myGeneratedCustomer.CustomerID, DbCustomer.CustomerID);
            }

        }

        [Theory]
        [InlineData(3, 15)]
        public void UpdateProductStockToInventory(int ProductID, int Quantity)
        {
            //Arrange
            DbContextOptions<StoreDbContext> options =  new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
                

            
            //Act
            using (StoreDbContext dbContext = new StoreDbContext( options ))
            {
                P0_RLayer myRepoLayer = new P0_RLayer( dbContext );

                CustomerLogInRequest = myRepoLayer.CreateCustomer();

                //SetOrderForCustomer
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
