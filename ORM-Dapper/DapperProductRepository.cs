using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _conn.Execute("INSERT INTO products (Name, Price, CategoryID) VALUES (@name, @price, @categoryID);", 
                new {name = name, price = price, categoryID = categoryID});
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM products;");
        }

        public void UpdateProduct(string name, double price, int categoryID, int productID)
        {
            _conn.Execute("UPDATE products SET Name = @name, Price = @price, CategoryID = @categoryID WHERE ProductID = @productID;",
                new { name = name, price = price, categoryID = categoryID, productID = productID });
        }

        public void DeleteProduct(int productID)
        {
            _conn.Execute("DELETE FROM products WHERE ProductID = @productID", new { productID = productID });
        }
    }
}
