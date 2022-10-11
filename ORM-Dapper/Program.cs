using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            #region Implemented departmentRepo Code

            //Console.WriteLine("-------------------DEPARTMENTS------------------------------");
            //Console.WriteLine();
            //var departmentRepo = new DapperDepartmentRepository(conn);

            //var departments = departmentRepo.GetAllDepartments();

            //foreach (var item in departments)
            //{
            //    Console.WriteLine($"{item.DepartmentID} {item.Name}");
            //    Console.WriteLine();
            //}

            #endregion


            #region Implemented productRepo Code

            // UpdateProduct and DeleteProduct methods will only work if you manually update the ProductID each time
            // I have it ready and waiting to run next time, but it will have to be adjusted again before you can run it again

            Console.WriteLine("-----------------------PRODUCTS------------------------------");
            Console.WriteLine();
            var productRepo = new DapperProductRepository(conn);

            var userInput = "0";

            while (userInput == "0" || userInput == "1" || userInput == "2" || userInput == "3" || userInput == "4")
            {
                Console.WriteLine("What would you like to do with the Products Table?");
                Console.WriteLine("1. View all the products.");
                Console.WriteLine("2. Create a new product.");
                Console.WriteLine("3. Update a product.");
                Console.WriteLine("4. Delete a product.");
                Console.WriteLine("5. Leave the application.");
                Console.Write("Please enter the number of your choice: ");
                userInput = Console.ReadLine();
                Console.WriteLine();

                while (userInput != "1" && userInput != "2" && userInput != "3" && userInput != "4" && userInput != "5")
                {
                    Console.WriteLine("Invalid entry. Please enter a valid number.");
                    Console.WriteLine("What would you like to do with the Products Table?");
                    Console.WriteLine("1. View all the products.");
                    Console.WriteLine("2. Create a new product.");
                    Console.WriteLine("3. Update a product.");
                    Console.WriteLine("4. Delete a product.");
                    Console.WriteLine("5. Leave the application.");
                    Console.Write("Please enter the number of your choice: ");
                    userInput = Console.ReadLine();
                }

                if (userInput == "1") // View all the products
                {
                    var products = productRepo.GetAllProducts();

                    foreach (var product in products)
                    {
                        Console.WriteLine($"Product ID: {product.ProductID}  |  Name: {product.Name}  |  " +
                            $"Price: ${product.Price}  |  Category ID: {product.CategoryID}");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Please view product data above.");
                    Console.Write("Press ENTER when you are ready to proceed: ");
                    Console.ReadLine();
                    Console.WriteLine();
                }

                if (userInput == "2") // Create a new product
                {
                    Console.Write("Please enter the NAME of the product you wish to CREATE: ");
                    var productName = Console.ReadLine();
                    Console.WriteLine(); 

                    Console.Write("Please enter the PRICE of the product you wish to CREATE: ");
                    var parseablePrice = double.TryParse(Console.ReadLine(), out double productPrice);
                    Console.WriteLine(); 

                    while (!parseablePrice)
                    {
                        Console.Write("Invalid price entry. Please enter a valid price: ");
                        parseablePrice = double.TryParse(Console.ReadLine(), out productPrice);
                        Console.WriteLine(); 
                    }

                    Console.WriteLine("Category IDs:");
                    Console.WriteLine("1. Computers");
                    Console.WriteLine("2. Appliances");
                    Console.WriteLine("3. Phones");
                    Console.WriteLine("4. Audio");
                    Console.WriteLine("5. Home Theater");
                    Console.WriteLine("6. Printers");
                    Console.WriteLine("7. Music");
                    Console.WriteLine("8. Games");
                    Console.WriteLine("9. Services");
                    Console.WriteLine("10. Other");
                    Console.Write("Please enter the CATEGORY ID for the product you wish to CREATE: ");
                    var parseableCategoryID = int.TryParse(Console.ReadLine(), out int productCategoryID);
                    Console.WriteLine();                    

                    while (!parseableCategoryID || productCategoryID != 1 && productCategoryID != 2 && productCategoryID != 3 && productCategoryID != 4 &&
                           productCategoryID != 5 && productCategoryID != 6 && productCategoryID != 7 && productCategoryID != 8 &&
                           productCategoryID != 9 && productCategoryID != 10)
                    {                       
                        Console.WriteLine($"Category does not exist. The valid Category ID options are:");
                        Console.WriteLine("1. Computers");
                        Console.WriteLine("2. Appliances");
                        Console.WriteLine("3. Phones");
                        Console.WriteLine("4. Audio");
                        Console.WriteLine("5. Home Theater");
                        Console.WriteLine("6. Printers");
                        Console.WriteLine("7. Music");
                        Console.WriteLine("8. Games");
                        Console.WriteLine("9. Services");
                        Console.WriteLine("10. Other");
                        Console.Write("Please enter a valid Category ID: ");
                        parseableCategoryID = int.TryParse(Console.ReadLine(), out productCategoryID);
                        Console.WriteLine(); 
                    }

                    productRepo.CreateProduct(productName, productPrice, productCategoryID);
                    Console.WriteLine($"{productName} has been created. You can now view it on the table.");
                    Console.Write("Press ENTER when you are ready to proceed: ");
                    Console.ReadLine();
                    Console.WriteLine();
                }

                if (userInput == "3") // Update product
                {
                    Console.Write("Please enter the PRODUCT ID of the product you wish to UPDATE: ");
                    var parseableProductID = int.TryParse(Console.ReadLine(), out int productID);
                    Console.WriteLine(); 

                    while (!parseableProductID)
                    {
                        Console.Write("Invalid entry. Please enter a valid Product ID: ");
                        parseableProductID = int.TryParse(Console.ReadLine(), out productID);
                        Console.WriteLine(); 
                    }

                    Console.Write("Please enter the updated NAME of the product: ");
                    var productName = Console.ReadLine();
                    Console.WriteLine(); 

                    Console.Write("Please enter the updated PRICE of the product: ");
                    var parseablePrice = double.TryParse(Console.ReadLine(), out double productPrice);
                    Console.WriteLine(); 

                    while (!parseablePrice)
                    {
                        Console.Write("Invalid price entry. Please enter a valid price: ");
                        parseablePrice = double.TryParse(Console.ReadLine(), out productPrice);
                        Console.WriteLine(); 
                    }

                    Console.WriteLine("Category IDs:");
                    Console.WriteLine("1. Computers");
                    Console.WriteLine("2. Appliances");
                    Console.WriteLine("3. Phones");
                    Console.WriteLine("4. Audio");
                    Console.WriteLine("5. Home Theater");
                    Console.WriteLine("6. Printers");
                    Console.WriteLine("7. Music");
                    Console.WriteLine("8. Games");
                    Console.WriteLine("9. Services");
                    Console.WriteLine("10. Other");
                    Console.Write("Please enter the updated CATEGORY ID for the product: ");
                    var parseableCategoryID = int.TryParse(Console.ReadLine(), out int productCategoryID);
                    Console.WriteLine(); 
                                        
                    while (!parseableCategoryID || productCategoryID != 1 && productCategoryID != 2 && productCategoryID != 3 && productCategoryID != 4 &&
                           productCategoryID != 5 && productCategoryID != 6 && productCategoryID != 7 && productCategoryID != 8 &&
                           productCategoryID != 9 && productCategoryID != 10)
                    {
                        Console.WriteLine($"Category does not exist. The valid Category ID options are:");
                        Console.WriteLine("1. Computers");
                        Console.WriteLine("2. Appliances");
                        Console.WriteLine("3. Phones");
                        Console.WriteLine("4. Audio");
                        Console.WriteLine("5. Home Theater");
                        Console.WriteLine("6. Printers");
                        Console.WriteLine("7. Music");
                        Console.WriteLine("8. Games");
                        Console.WriteLine("9. Services");
                        Console.WriteLine("10. Other");
                        Console.Write("Please enter a valid Category ID: ");
                        parseableCategoryID = int.TryParse(Console.ReadLine(), out productCategoryID);
                        Console.WriteLine(); 
                    }

                    productRepo.UpdateProduct(productName, productPrice, productCategoryID, productID);
                    Console.WriteLine($"{productName} has been updated. You can view the updates on the table.");
                    Console.Write("Press ENTER when you are ready to proceed: ");
                    Console.ReadLine();
                    Console.WriteLine();
                }

                if (userInput == "4") // Delete product
                {
                    Console.Write("Are you sure you want to DELETE a product? You CANNOT undo this. Enter \"yes\" to proceed: ");
                    var userProceed = Console.ReadLine().ToLower();
                    Console.WriteLine();

                    if (userProceed == "yes")
                    {
                        Console.Write("Please enter the PRODUCT ID of the product you wish to DELETE: ");
                        var parseableProductID = int.TryParse(Console.ReadLine(), out int productID);
                        Console.WriteLine();

                        while (!parseableProductID)
                        {
                            Console.Write("Invalid entry. Please enter a valid Product ID: ");
                            parseableProductID = int.TryParse(Console.ReadLine(), out productID);
                            Console.WriteLine();
                        }

                        productRepo.DeleteProduct(productID);
                        Console.WriteLine($"This product has been deleted.");
                        Console.Write("Press ENTER when you are ready to proceed: ");
                        Console.ReadLine();
                        Console.WriteLine();
                    }                    
                }                
            }

            Console.WriteLine("Have a great day!");

            #endregion
        }
    }
}