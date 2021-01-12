using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using P1_ModelLib.Models;
using P1_ModelLib.ViewModels;
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
        private readonly Mapper _mapper;

        /// <summary>
        /// Constructor for our Repository Layer Object. It initializes the Lists and gets the context from any production or in-memory DB
        /// </summary>
        public Repository(StoreDbContext dbContextClass, ILogger<Repository> logger, Mapper mapper)
        {

            _storeDbContext = dbContextClass;
            DbInitializer.Initialize(_storeDbContext);
            //this.Locations = _storeDbContext.Locations.ToList();
            //this.Products = _storeDbContext.Products.ToList();
            //this.Inventory = _storeDbContext.Inventory.ToList();
            //this.Orders = _storeDbContext.Orders.ToList();
            _logger = logger;
            _mapper = mapper;
        }


        //internal List<Location> Locations = new List<Location>();
        //internal List<Product> Products = new List<Product>();
        //internal List<Inventory> Inventory = new List<Inventory>();
        //internal List<Order> Orders = new List<Order>();

        public Location GetDefaultLocationForRegisterCustomer()
        {
            Location myLocation = _storeDbContext.Locations.FirstOrDefault(x => x.Name == "Central Branch");

            return myLocation;
        }

        public List<ProductViewModel> GetAllTheProducts()
        {
            List<Product> Products = _storeDbContext.Products.Include( product => product.Department ). ToList();

            List<ProductViewModel> listProductVM = new List<ProductViewModel>();

            foreach (Product prod in Products)
            {
                listProductVM.Add(_mapper.ConvertProductIntoProductVM(prod));
            }


            return listProductVM;
        }

        internal void SaveChangesToDb()
        {
            _storeDbContext.SaveChanges();
        }

        public Product CreateNewProduct(Product myProduct)
        {
            _storeDbContext.Products.Add(myProduct);
            SaveChangesToDb();

            Product DbProduct = _storeDbContext.Products.First(x => x.Name == myProduct.Name && x.Description == myProduct.Description && x.Price == myProduct.Price);

            return DbProduct;
        }

        public void UpdateProduct(Product myProduct)
        {
            _storeDbContext.Products.Update(myProduct);
            SaveChangesToDb();

        }

        public void DeleteProduct(int id)
        {
            Product myProduct = _storeDbContext.Products.First(x => x.ProductID == id);
            _storeDbContext.Remove(myProduct);
            SaveChangesToDb();
        }

        public Product GetProductByID(int id)
        {
            Product myProduct = _storeDbContext.Products.Include(product => product.Department ).FirstOrDefault(x => x.ProductID == id);
            return myProduct;
        }

        public ProductViewModel ConvertProductIntoVM(Product product)
        {
            ProductViewModel productViewModel = _mapper.ConvertProductIntoProductVM(product);

            return productViewModel;
        }

        public List<Inventory> GetAllTheInventoryFromStore(int storeID)
        {
            Location StoreInventory = GetStoreLocationIncludingInventoryAndProduct(storeID);

            return StoreInventory.Inventory;
        }

        public InventoryViewModel ConvertInventoryIntoVM(Inventory inventory)
        {
            InventoryViewModel inventoryViewModel = _mapper.ConvertInventoryIntoInventoryVM(inventory);
            return inventoryViewModel;
        }


        public List<Department> GetAllTheDepartments()
        {
            List<Department> departments = _storeDbContext.Departments.ToList();
            return departments;
        }

        public Department GetDepartmentByID(int id)
        {
            Department department = _storeDbContext.Departments.FirstOrDefault(x => x.DepartmentID == id);
            return department;
        }

        public void CreateNewDepartment(Department department)
        {
            _storeDbContext.Departments.Add(department);
            SaveChangesToDb();
        }

        public void UpdateDepartment(Department department)
        {
            _storeDbContext.Departments.Update(department);
            SaveChangesToDb();
        }

        public void DeleteDepartment(int id)
        {
            Department department = _storeDbContext.Departments.First(x => x.DepartmentID == id);
            _storeDbContext.Remove(department);
            SaveChangesToDb();
        }

        public List<LocationViewModel> GetAllTheLocation()
        {
            List<Location> locations = _storeDbContext.Locations.ToList();

            //Mapper
            List<LocationViewModel> locationViewModel = new List<LocationViewModel>();

            foreach (Location loc in locations)
            {
                locationViewModel.Add(_mapper.ConvertLocationIntoLocationVM(loc)); 
            }

            return locationViewModel;
        }

        public Location GetLocationByID(int id)
        {
            Location myLocation = _storeDbContext.Locations.FirstOrDefault(x => x.LocationID == id);

            return myLocation;
        }

        public LocationViewModel ConvertLocationIntoVM(Location location)
        {

            LocationViewModel locationViewModel = _mapper.ConvertLocationIntoLocationVM(location);
            return locationViewModel;
        }

        public void CreateNewLocation(LocationViewModel locationViewModel)
        {
            Location myLocation = new Location();

            myLocation.Name = locationViewModel.Name;
            myLocation.Address = locationViewModel.Address;

            _storeDbContext.Locations.Add(myLocation);

            SaveChangesToDb();
        }

        public void UpdateLocation(Location location)
        {
            _storeDbContext.Locations.Update(location);
            SaveChangesToDb();
        }

        public void DeleteLocation(Location location)
        {
            _storeDbContext.Locations.Remove(location);
            SaveChangesToDb();
        }

        public Inventory CreateNewInventory(InventoryViewModel inventoryViewModel, int ProductID,int StoreID)
        {
            Location StoreInventory = GetStoreLocationIncludingInventoryAndProduct(StoreID);

            // Verify if its exist 
            if ( StoreInventory.Inventory.Exists( x => x.Product.ProductID == ProductID) )
            {
                //We will add the product
                //Validate if it it in the limit..
                Inventory inventory = StoreInventory.Inventory.First(x => x.Product.ProductID == ProductID);


                if ( inventory.Quantity + inventoryViewModel.Quantity > 100 )
                {
                    //It is the defined limit, throw a exception
                    throw new Exception("Error while updating the product. The Product limit is 99.");
                }
                else
                {
                    // Proceed to update the product
                    StoreInventory.Inventory.First(x => x.Product.ProductID == ProductID).Quantity += inventoryViewModel.Quantity;

                    UpdateLocation(StoreInventory);

                }
            }
            else
            {
                //Is a new inventory
                Inventory inventory = new Inventory()
                {
                    Product = this.GetProductByID(ProductID),
                    Quantity = inventoryViewModel.Quantity
                };

                StoreInventory.Inventory.Add(inventory);

                _storeDbContext.Locations.Update(StoreInventory);

                SaveChangesToDb();
            }


            Inventory returnInv = GetStoreLocationIncludingInventoryAndProduct(StoreID)
                .Inventory
                .FirstOrDefault(x => x.Product.ProductID == ProductID);

            return returnInv;


        }

        public void UpdateInventory(Inventory inventory)
        {
            _storeDbContext.Inventory.Update(inventory);
            SaveChangesToDb();
            
        }

        private Location GetStoreLocationIncludingInventoryAndProduct(int StoreID)
        {
            Location StoreInventory = _storeDbContext.Locations
              .Include(store => store.Inventory)
              .ThenInclude(inventory => inventory.Product)
              .First(x => x.LocationID == StoreID);
            return StoreInventory;
        }

        public void UpdateInventoryQuantity(int id, int StoreID, int Quantity)
        {
            Location StoreLocation = GetStoreLocationIncludingInventoryAndProduct(StoreID);

            //Change the inventory in the location
            StoreLocation.Inventory.First(x => x.InventoryID == id).Quantity = Quantity;

            _storeDbContext.Locations.Update(StoreLocation);
            SaveChangesToDb();

        }

        public void DeleteInventory(int id, int StoreID)
        {
            Location StoreLocation = GetStoreLocationIncludingInventoryAndProduct(StoreID);

            Inventory inventory = StoreLocation.Inventory.First(x => x.InventoryID == id);

            StoreLocation.Inventory.Remove(inventory);

            _storeDbContext.Locations.Update(StoreLocation);
            SaveChangesToDb();
        }
    }
}
