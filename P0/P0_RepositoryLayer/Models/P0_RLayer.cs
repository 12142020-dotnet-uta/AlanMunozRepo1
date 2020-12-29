using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P0_CLibrary.Models;

namespace P0_RepositoryLayer.Models
{
    public class P0_RLayer 
    {
        //All the logic interaction between the objects, later we are going to populate it.
        public List<Customer> Customers = new List<Customer>();
        public List<Location> Locations = new List<Location>();
        public List<Product> Products = new List<Product>();
        public List<Inventory> Inventory = new List<Inventory>();
        public List<Order> Orders = new List<Order>();

        // -> Not sure if it needs the OrderDetail list, or with the order object it can retrieve it...

        private Order CurrentOrder = new Order();
        private Customer LoggedCustomer;
        
        // ->
        private List<Inventory> LocalInventory = new List<Inventory>();

        StoreDbContext myDbContext = new StoreDbContext();
        
        // private Order ActualOrder;
        
        /// <summary>
        /// Constructor for our Repository Layer Object. It initializes the Lists and gets the context from the production database.
        /// </summary>
        public P0_RLayer()
        {
            
            // using(StoreDbContext Db = new StoreDbContext())
            // {   
                DbInitializer.Initialize(myDbContext);

                this.Customers =myDbContext.Customers.ToList();
                this.Locations = myDbContext.Locations.ToList();
                this.Products = myDbContext.Products.ToList();
                this.Inventory = myDbContext.Inventory.ToList();
                this.Orders = myDbContext.Orders.ToList();

            // }
        }

        /// <summary>
        /// Constructor for our Repository Layer Object. It initializes the Lists and gets the context from the In-Memory database.
        /// </summary>
        public P0_RLayer(StoreDbContext context)
        {
              
            DbInitializer.Initialize(context);

            this.Customers = context.Customers.ToList();
            this.Locations = context.Locations.ToList();
            this.Products = context.Products.ToList();
            this.Inventory = context.Inventory.ToList();
            this.Orders = context.Orders.ToList();
            
        }
        
        /// <summary>
        /// This method is used for creating a new Customer object if its not registered, if its registered, it will find the customer in the Customers list and saved it in a private
        /// LoggedCustomer object.  
        /// </summary>
        /// <param name="strFirstName">string parameter, it contains the fisrt name of the customer.</param>
        /// <param name="strLastName">string parameter, it contains the last name of the customer</param>
        /// <returns>returns a message if the customer was registered, or else it welcomes the user logged in.</returns>
        public string CreateCustomer(string strFirstName = "", string strLastName = "")
        {
            if ( !this.Customers.Exists( X => X.FirstName == strFirstName && X.LastName == strLastName ) )
            {

                //Every customer created it will have a default selection, it will be central.
                Customer customer = new Customer()
                {
                    FirstName = strFirstName,
                    LastName = strLastName,
                    Location = Locations.Find( x => x.LocationID == 1 ),
                };
                // using(StoreDbContext Db = new StoreDbContext())
                // {
                    // ->
                    myDbContext.Customers.Add(customer);
                    myDbContext.Locations.Update(customer.Location);
                    myDbContext.Inventory.UpdateRange(customer.Location.Inventory);
                    //Db.Products.UpdateRange(customer.Location.Inventory)
                    foreach (Inventory inventory in customer.Location.Inventory)
                    {
                        myDbContext.Products.Update(inventory.Product);
                    }

                    // ->
                    myDbContext.SaveChanges();

                    this.Customers = myDbContext.Customers.ToList();
                // }
                // this.Customers.Add(customer);

                return $"The customer {customer.ToString()} was created successfully.\n";
            }
            this.LoggedCustomer = this.Customers.Find(x => x.FirstName == strFirstName && x.LastName == strLastName);

            return $"Welcome {this.LoggedCustomer.ToString()}"; 

        }

        /// <summary>
        /// This method is used for searching a customer by name, it uses a Contains( in SQL will "LIKE('%string%')"), so it will find every elements in the list and 
        /// it will print the customers.
        /// </summary>
        /// <param name="strSearchName">string parameter, this is the name we want to search in the customer List.</param>
        /// <returns>It will return a string with all the results. If there is no results, it will return a empty customer format list. </returns>
        public string SearchCustomerByName(string strSearchName)
        {
            // IEnumerable<Customer> SearchCustomer = this.Customers.Where( x => x.ToString().Contains( strSearchName ) );

            string strSearchCustomer = "\nUserID\t\tFirst Name\t\tLast Name\n";

            foreach (Customer cust in ( this.Customers.Where( x => x.ToString().ToLower().Contains( strSearchName.ToLower() ) ) ) )
            {
                strSearchCustomer += $"{cust.CustomerID}\t\t{cust.FirstName}\t\t{cust.LastName}\n";
            }

            return strSearchCustomer;
        }
        /// <summary>
        /// This method is used for accessing the logged user and get the location of the customer logged.
        /// </summary>
        /// <returns>Returns a string with the locatrion name where the customer is registered.(The default location is central when a customer is created)</returns>
        public string GetActualLocation()
        {
            return this.LoggedCustomer.Location.Name;
        }

        /// <summary>
        /// This methods is used for handling the menu user interface. It uses a Enun MenuOptions with the available options in the menu. It will validate the user selection.
        /// If its not valid, it will be marked as NotValid
        /// </summary>
        /// <param name="strUISelection">string parameter, Is the selection of the user, it can be 1-5, or if its spelled the Enum option correctly</param>
        /// <returns>Returns a Enum MenuOptions with the requested option</returns>
        public MenuOptions GetUIMenuOptions(string strUISelection)
        {
            MenuOptions myMenuOptions = new MenuOptions();
            if ( !MenuOptions.TryParse(strUISelection, out myMenuOptions) )
            {
                myMenuOptions = MenuOptions.NotValid;
            }
            return myMenuOptions;
        }

    /// <summary>
    /// This method is used for verifying if the user want to stay in their registered location or if it wants to change to another one.(Only accepts Y/N)
    /// </summary>
    /// <param name="strChangeLocation">string parameter, is the user selection</param>
    /// <returns>Returns a boolean value indicating a false for change into another location and true to stay in the location.</returns>    
        public bool ChangeLocationBranch(string strChangeLocation)
        {
            if ( strChangeLocation.ToLower().Trim() != "y" )
            {
                //User input N/n or else.
                return false;
            }
            else
            {
                //Continue in the same branch
                return true;
            }
        }

        /// <summary>
        /// This method return a table-like string with all the locations available to the customer to choose excluding the actual store
        /// </summary>
        /// <returns>Returns a string with a table-like format for selecting a location</returns>
        public string ListEveryLocationExceptCustomerLocation()
        {
            // IEnumerable<Customer> SearchCustomer = this.Customers.Where( x => x.ToString().Contains( strSearchName ) );

            string strLocations = "\nLocationID\t\tName\n";

            foreach (Location local in this.Locations.Where( x => x.LocationID != this.LoggedCustomer.Location.LocationID ).OrderBy( x => x.LocationID ) ) //Except( this.LoggedCustomer.Location ) )
            {
                strLocations += $"{local.LocationID}\t\t{local.Name}\n";
            }

            return strLocations;
        }

        /// <summary>
        /// This method return a table-like string with all the locations available to the customer to choose
        /// </summary>
        /// <returns>Returns a string with a table-like format for selecting a location</returns>
        public string ListEveryLocation()
        {
            string strLocations = "\nLocationID\t\tName\n";

            foreach (Location local in this.Locations.OrderBy( x => x.LocationID ) ) //Except( this.LoggedCustomer.Location ) )
            {
                strLocations += $"{local.LocationID}\t\t{local.Name}\n";
            }

            return strLocations;
        }

        /// <summary>
        /// This method execute a int.TryParse and verifyies if the locationID exists in the location table. If exists in the table, it will perform a change to the selected
        /// location and update the database for the default location
        /// </summary>
        /// <param name="strLocationID">string param, is the user input to validate if it is a valid ID.</param>
        /// <returns>Is false when it doesn't find any element with the ID or when the string
        /// passed is not a valid number. It returns true when is a number and exists in the table</returns>
        public bool LocationChangeVerification(string strLocationID)
        {
            int result = 0;

            if( !int.TryParse( strLocationID, out result ) || !Locations.Exists( x => x.LocationID == result ) )
            {
                // Not valid...
                return false;
            }
            else
            {
                SetLocationForLoggedCustomer(result);
                return true;
            }

        }

        /// <summary>
        /// This method execute a int.TryParse and verifyies if the locationID exists in the location table. If exists in the table, it will return the LocationID, if else, it will return a 0.
        /// </summary>
        /// <param name="strLocationID">string param, is the user input to validate if it is a valid ID.</param>
        /// <returns>Is 0 when it doesn't find any element with the ID or when the string
        /// passed is not a valid number. It returns the InventoryID when is a number and exists in the table</returns>
        public int LocationHistoryVerification(string strLocationID)
        {
            int result = 0;

            if (!int.TryParse(strLocationID, out result) || !Locations.Exists(x => x.LocationID == result))
            {
                // Not valid...
                return 0;
            }
            else
            {
                return result;
            }
        }
        /// <summary>
        /// This method is executed for changing the logged customer the default store to perform the operations.
        /// </summary>
        /// <param name="LocationID">integer parameter, it is the new LocationID the logged customer is going to be linked to.</param>
        private void SetLocationForLoggedCustomer(int LocationID)
        {
            Location myLocation = Locations.First( x => x.LocationID == LocationID );

            this.LoggedCustomer.Location = myLocation;
            // using(StoreDbContext Db = new StoreDbContext())
            // {
                // Verify if the changelocation Works in DB.
                myDbContext.Customers.Update(this.LoggedCustomer);
                myDbContext.SaveChanges();
            // }

        }

        /// <summary>
        /// This method is used to list all the inventory in a table-style string
        /// </summary>
        /// <returns>Returns a string-like-table for printing the results.</returns>
        public string ListAllInventoryInLocation()
        {

            LocalInventory.Clear();

            string strSearchInventory = "\nProduct ID\t\tProduct Name\t\tQuantity\t\tPrice\n";

            List<Inventory> myInventory = this.LoggedCustomer.Location.Inventory.OrderByDescending(x => x.InventoryID).ToList();

            foreach (Inventory inv in myInventory)
            {
                strSearchInventory += $"{inv.Product.ProductID}\t\t{inv.Product.Name}\t\t{inv.Quantity}\t\t{inv.Product.Price}\n";
                //
                LocalInventory.Add(inv);
            }

            return strSearchInventory;
        }

        /// <summary>
        /// This method is used to verify if the requested InventoryID is a valid input or if its exist in the Inventory List
        /// </summary>
        /// <param name="strInventoryID">string parameter, is the InventoryID to verify if its a int and if it exists in the Inventory list.</param>
        /// <returns>return a true when is a int and a valid element in the inventory, if else, returns false</returns>
        public bool SearchProductWithID(string strProductID)
        {
            int result = 0;

            if( !int.TryParse( strProductID, out result ) || !Products.Exists( x => x.ProductID == result ) )
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// This method is used for set the private CurrentOrder object and assign the product details and quantity into the customer order.
        /// </summary>
        /// <param name="InventoryID">int parameter, is the InventoryID</param>
        /// <param name="Quantity">int parameter, is the Quantity of products to add in the order</param>
        public void SetOrderForCustomer(int ProductID, int Quantity)
        {
            Inventory myInventory = LocalInventory.First( x=> x.Product.ProductID == ProductID );            

            //Inventory myInventory = Inventory.First( x=> x.InventoryID == intInventoryID );

            
            OrderDetail myOrderDetail = new OrderDetail();

            
            myOrderDetail.Product = myInventory.Product;
            myOrderDetail.Quantity = Quantity;

            myInventory.Quantity -= Quantity;

            //Update to DB...
            // using(StoreDbContext Db = new StoreDbContext())
            // {
                // Verify if the changelocation Works in DB.
                myDbContext.Inventory.Update(myInventory);
                myDbContext.SaveChanges();
            // }

            this.CurrentOrder.OrderDetails.Add(myOrderDetail);

        }

        /// <summary>
        /// This private method is used to set the values of the CurrentOrder.
        /// </summary>
        private void SetCurrentOrderInformation()
        {
            this.CurrentOrder.Customer = this.LoggedCustomer;
            this.CurrentOrder.Date = DateTime.Now;
            this.CurrentOrder.Location = LoggedCustomer.Location;
        }

        /// <summary>
        /// Private method that perform the Insert of the Order the customer have generated with the list of orderDetails withhin.
        /// </summary>
        private void SaveOrderChanges()
        {
                myDbContext.Orders.Add(this.CurrentOrder);
                myDbContext.SaveChanges();
        }

        public string PrintOrder()
        {
            SetCurrentOrderInformation();
            SaveOrderChanges();
            string strOrderResult = $"Order by customer {LoggedCustomer.ToString()}\t\tLocation: {LoggedCustomer.Location.Name}\nDate: {this.CurrentOrder.Date}\n";

            double dblTotalAmount = this.CurrentOrder.GetTotalAmountFromOrderDetail();


            foreach (OrderDetail odetail in this.CurrentOrder.OrderDetails)
            {
                strOrderResult += $"Product Name: {odetail.Product.Name}\nQuantity: {odetail.Quantity}\nPrice: {odetail.Product.Price}\n";
            }
            strOrderResult += $"Total for order: {dblTotalAmount}";


            FinishOrders();
            return strOrderResult;
        }

        /// <summary>
        /// Private method that clears the localInventory List to be empty and recreate the CurrentOrder asa new Order to initialize its nulls values.
        /// </summary>
        private void FinishOrders()
        {
            this.LocalInventory.Clear();
            this.CurrentOrder = new Order();
        }

        /// <summary>
        /// This method is called for generate a table-like format with the total orders the customer have 
        /// </summary>
        /// <returns>Returns a string with a table-like format.</returns>
        public string GetAllTheHistoryFromCustomer()
        {
            string strCustomerHistory = $"For the logged customer {this.LoggedCustomer.ToString()}:\n";

            List<Order> CustomerOrders = myDbContext.Orders.Where( x=> x.Customer.CustomerID == this.LoggedCustomer.CustomerID ).ToList();


            foreach ( Order order in CustomerOrders )
            {
                List<OrderDetail> OrderDetails = myDbContext.Orders.Where(x => x.OrderID == order.OrderID ).SelectMany(x => x.OrderDetails).ToList();
                order.OrderDetails = OrderDetails;

                strCustomerHistory += $"\tCustomer: {order.Customer.ToString()}, OrderID: {order.OrderID}, Store: {order.Location.Name}, Date: {order.Date.ToString("MM/dd/yyyy hh:mm")}, Total: {order.GetTotalAmountFromOrderDetail()}\nDetails:\n";



                foreach (OrderDetail orderDetail in OrderDetails)
                {
                    strCustomerHistory += $"\t\tProduct: {orderDetail.Product.Name}, Quantity: {orderDetail.Quantity}\n";
                }
            }

            return strCustomerHistory;
        }

        /// <summary>
        /// This method is called for generate a table-like format with the total orders the selected store have generated so long. 
        /// </summary>
        /// <returns>Returns a string with a table-like format.</returns>
        public string GetAllTheHistoryFromCustomer(int intLocationID)
        {
            Location myLocation = myDbContext.Locations.First( x => x.LocationID == intLocationID );

            List<Order> StoreOrders = myDbContext.Orders
                .Where(x => x.Location.LocationID == intLocationID).ToList();

            string strLocationHistory = $"For the selected store: {myLocation.Name}:\n";


            foreach (Order order in StoreOrders)
            {
                List<OrderDetail> OrderDetails = myDbContext.Orders.Where(x => x.OrderID == order.OrderID).SelectMany(x => x.OrderDetails).ToList();
                order.OrderDetails = OrderDetails;

                strLocationHistory += $"\tCustomer: {order.Customer.ToString()}, OrderID: {order.OrderID}, Store: {order.Location.Name}, Date: {order.Date.ToString("MM/dd/yyyy hh:mm")}, Total: {order.GetTotalAmountFromOrderDetail()}\nDetails:\n";



                foreach (OrderDetail orderDetail in OrderDetails)
                {
                    strLocationHistory += $"\t\tProduct: {orderDetail.Product.Name}, Quantity: {orderDetail.Quantity}\n";
                }
                strLocationHistory += "\n";
            }

            return strLocationHistory;
        }
    }
}