using Microsoft.Extensions.Logging;
using P1_ModelLib.Models;
using Store_RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_RepositoryLayer
{
    public class Repository
    {   
        private readonly ILogger<Repository> _logger;

        private readonly StoreDbContext _storeDbContext;

        internal List<Customer> Customers = new List<Customer>();


        internal List<Location> Locations = new List<Location>();
        internal List<Product> Products = new List<Product>();
        internal List<Inventory> Inventory = new List<Inventory>();
        internal List<Order> Orders = new List<Order>();

        public Location GetDefaultLocationForRegisterCustomer()
        {
            Location myLocation = _storeDbContext.Locations.FirstOrDefault(x => x.Name == "Central Branch");

            return myLocation;
        }

        /// <summary>
        /// Constructor for our Repository Layer Object. It initializes the Lists and gets the context from any production or in-memory DB
        /// </summary>
        public Repository(StoreDbContext dbContextClass, ILogger<Repository> logger)
        {

            _storeDbContext = dbContextClass;
            DbInitializer.Initialize(_storeDbContext);
            this.Customers = _storeDbContext.Users.ToList();
            this.Locations = _storeDbContext.Locations.ToList();
            this.Products = _storeDbContext.Products.ToList();
            this.Inventory = _storeDbContext.Inventory.ToList();
            this.Orders = _storeDbContext.Orders.ToList();
            _logger = logger;
        }


    }
}
