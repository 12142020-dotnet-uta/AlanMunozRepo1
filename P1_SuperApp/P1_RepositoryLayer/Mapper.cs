using Microsoft.Extensions.Logging;
using P1_ModelLib.Models;
using P1_ModelLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_RepositoryLayer
{
    public class Mapper
    {
        private readonly ILogger<Mapper> _logger;


        public Mapper(ILogger<Mapper> logger)
        {
            _logger = logger;
        }

        public ProductViewModel ConvertProductIntoProductVM(Product product)
        {
            ProductViewModel myViewModel = new ProductViewModel()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Department = product.Department.Name,
                ProductID = product.ProductID
            };

            return myViewModel;

        }
        public LocationViewModel ConvertLocationIntoLocationVM(Location location)
        {
            LocationViewModel locationViewModel = new LocationViewModel()
            {
                Name = location.Name,
                LocationID = location.LocationID,
                Address = location.Address
            };

            return locationViewModel;
        }

        public InventoryViewModel ConvertInventoryIntoInventoryVM(Inventory inventory)
        {
            InventoryViewModel inventoryViewModel = new InventoryViewModel()
            {
                InventoryID = inventory.InventoryID,
                ProductName = inventory.Product.Name,
                ProductPrice = inventory.Product.Price,
                Quantity = inventory.Quantity,
            };

            return inventoryViewModel;
        }

    }
}
