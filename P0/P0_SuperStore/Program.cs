using System;
using P0_RepositoryLayer.Models;

namespace P0_SuperStore
{
    class Program
    {
        static void Main(string[] args)
        {
            P0_RLayer myRepoLayer = new P0_RLayer();
            Console.WriteLine("Welcome to the Super");
            
            bool ExitMainMenu = false;

            do
            {
                Console.Write("Log in  (Press x to quit the app)\nFirst Name: ");      
                string strUserFirstName = Console.ReadLine().Trim().Split(' ')[0];// Trim any white spaces that the user input in the beggining and split to get only the first...
                if (strUserFirstName.ToLower() == "x")
                {
                    Console.WriteLine("Exiting the console...\n");
                    ExitMainMenu = true;
                    break;
                }
                Console.Write("Last Name: ");
                string strUserLastName = Console.ReadLine().Trim().Split(' ')[0];
                
                //Compare the user name given if its exists...
                // ->
                Console.WriteLine( myRepoLayer.CreateCustomer(strUserFirstName, strUserLastName) );

                //The customer {customer.ToString()} was created successfully.\n
                

                // Showing the fisrt main menu...
                do
                {
                    Console.WriteLine("\nSelect one of the following options:\n1.-Generate a order\t\t2.-Search customer by Name\n3.-Customer History\t\t4.-Store History\n5.-Log out");

                    string strUIMainMenu = Console.ReadLine().Trim();

                    // strUIMainMenu = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strUIMainMenu.ToLower());
                    MenuOptions menuOptions = new MenuOptions();

                    // ->
                    if ( !MenuOptions.TryParse(strUIMainMenu, out menuOptions) )
                    {
                        menuOptions = MenuOptions.NotValid;
                    }

                    if ( menuOptions == MenuOptions.GenerateOrder )
                    {
                        #region GenerateOrder

                        #region ChangeLocation
                        Console.WriteLine($"Your default location is {myRepoLayer.GetActualLocation()}, stay in the location?\nY/N");
                        string strUIChanceLocation = Console.ReadLine();

                        bool bolUILocation = true;
                        bool bolUIInventory = true;
                        do
                        {
                            // Check if the selection is correct
                            if(strUIChanceLocation.Trim().Length != 1)
                            {
                                Console.WriteLine("Invalid input\nY/N\n");
                                strUIChanceLocation = Console.ReadLine();
                                continue;
                            }
                            if( myRepoLayer.ChangeLocationBranch(strUIChanceLocation) )
                                // Stay in the location
                                bolUILocation = false;
                            else
                            {
                                //Change into another location
                                bool bolUILocationSelected = true;
                                do
                                {
                                    Console.Write($"{myRepoLayer.ListEveryLocationExceptCustomerLocation()}\nSelect the LocationID to change into the location: ");

                                    string strUILocation = Console.ReadLine();
                                    
                                    if( myRepoLayer.LocationChangeVerification(strUILocation) )
                                        bolUILocationSelected = false;
                                    else
                                        Console.WriteLine("\t\tInvalid option\n");

                                } while (bolUILocationSelected);
                                bolUILocation = false;

                            }

                        } while (bolUILocation);
                        #endregion


                        do
                        {

                            Console.Write($"\t\t\tInventory: \n{myRepoLayer.ListAllInventoryInLocation()}\n\nSelect the product to add into the order:");
                            string strUIProductID = Console.ReadLine();

                            if( myRepoLayer.SearchProductWithID(strUIProductID) )
                            {
                                bool bolUIQuantity = true;
                                do
                                {
                                    Console.Write("Quantity: ");
                                    string strInventoryQuantity = Console.ReadLine();

                                    int intQuantity = 0;
                                    ///
                                    if( !int.TryParse( strInventoryQuantity, out intQuantity ) )
                                    {
                                        Console.WriteLine("\t\tInvalid input for quantity.");
                                        continue;
                                    }

                                    
                                    myRepoLayer.SetOrderForCustomer( int.Parse( strUIProductID ), intQuantity );
                                    bolUIQuantity = false;

                                } while (bolUIQuantity);

                                Console.WriteLine("Add another product?: \nY/N");
                                string strUIAddProduct = Console.ReadLine();
                                
                                if( myRepoLayer.ChangeLocationBranch(strUIAddProduct) ) //Y
                                    continue;
                                else //N
                                    bolUIInventory = false;
                            }                       
                            else
                            {
                                Console.WriteLine("Invalid input (Exit to main menu)\n");
                                continue;//
                            }     
                        } while (bolUIInventory);

                        // Mostrar el total de la orden y guardar en DB
                        Console.WriteLine( myRepoLayer.PrintOrder() );
                        

                        #endregion
                    }
                    if ( menuOptions == MenuOptions.SearchCustomer )
                    {
                        Console.Write("\n\tInput the Name of the customer: ");
                        string strSearchResult = Console.ReadLine();

                        // ->
                        Console.WriteLine( myRepoLayer.SearchCustomerByName(strSearchResult) + "\n");
                    }
                    else if ( menuOptions == MenuOptions.CustomerHistory )
                    {
                        Console.WriteLine( myRepoLayer.GetAllTheHistoryFromCustomer() );
                    }
                    else if ( menuOptions == MenuOptions.StoreHistory )
                    {
                        bool bolUILocationSelected = true;

                        int intLocationID = 0;
                        do
                        {
                            Console.Write($"{myRepoLayer.ListEveryLocation()}\nSelect the LocationID to view the orders history: ");

                            string strLocationID = Console.ReadLine();

                            intLocationID = myRepoLayer.LocationHistoryVerification(strLocationID);
                            if ( intLocationID != 0 )
                                bolUILocationSelected = false;
                            else
                                Console.WriteLine("\t\tInvalid option\n");

                        } while (bolUILocationSelected);

                        Console.WriteLine(myRepoLayer.GetAllTheHistoryFromCustomer( intLocationID ));

                        
                    }
                    else if ( menuOptions == MenuOptions.LogOut )
                    {
                        Console.WriteLine("Login Out...\n");
                        break;
                    }
                    else if ( menuOptions == MenuOptions.NotValid )
                    {
                        Console.WriteLine("\t\tInvalid input.");
                        continue;
                    }

                } while ( !ExitMainMenu );

            } while ( !ExitMainMenu );
        }
    }
}
