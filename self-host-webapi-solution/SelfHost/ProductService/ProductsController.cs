using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace MyWork.ProductService
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]  
        {  
            new Product { Id = 1, Name = "Maths", Category = "books", Price = 1 },  
            new Product { Id = 2, Name = "Physics", Category = "books", Price = 1 },  
            new Product { Id = 3, Name = "Programming", Category = "books", Price = 1 },  
            new Product { Id = 4, Name = "Cricket", Category = "games", Price = 1 },  
            new Product { Id = 5, Name = "Football", Category = "games", Price = 1 },  
            new Product { Id = 6, Name = "Wrestling", Category = "games", Price = 1 },  
            new Product { Id = 7, Name = "Coca Cola", Category = "drinks", Price = 1 },  
            new Product { Id = 8, Name = "Sprite", Category = "drinks", Price = 1 },  
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return products.Where(p => string.Equals(p.Category, category,
                    StringComparison.OrdinalIgnoreCase));
        }
    }
}
