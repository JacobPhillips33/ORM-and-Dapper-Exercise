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

            Console.WriteLine("-------------------DEPARTMENTS------------------------------");
            Console.WriteLine();
            var departmentRepo = new DapperDepartmentRepository(conn);

            var departments = departmentRepo.GetAllDepartments();

            foreach (var item in departments)
            {
                Console.WriteLine($"{item.DepartmentID} {item.Name}");
                Console.WriteLine();
            }

            #endregion


            #region Implemented productRepo Code

            // UpdateProduct and DeleteProduct methods will only work if you manually update the ProductID each time
            // I have it ready and waiting to run next time, but it will have to be adjusted again before you can run it again

            Console.WriteLine("-----------------------PRODUCTS------------------------------");
            Console.WriteLine();
            var productRepo = new DapperProductRepository(conn);

            productRepo.CreateProduct("AWESOME PRODUCT", 333.33, 10);

            productRepo.UpdateProduct("cool product", 321.98, 10, 944);

            productRepo.DeleteProduct(943);

            var products = productRepo.GetAllProducts();

            Console.WriteLine("Product ID | Product Name | Price | Category ID");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID} | {product.Name} | {product.Price} | {product.CategoryID}");
            }

            #endregion
        }
    }
}