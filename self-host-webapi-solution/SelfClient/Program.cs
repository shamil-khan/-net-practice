using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Configuration;


namespace MyWork.SelfClient
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }

    class Program
    {
        static HttpClient _Service = new HttpClient();

        static void Main(string[] args)
        {
            var address = ConfigurationManager.AppSettings["service-address"];
            _Service.BaseAddress = new Uri(address);

            ListAllProducts();
            ListProduct(1);
            ListProducts("books");

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Press Enter to quit.");
            Console.ReadLine();
        }

        static void ListAllProducts()
        {
            Console.WriteLine("--------------------- Get All Products ----------------------------------");
            HttpResponseMessage response = _Service.GetAsync("services/products").Result;
            response.EnsureSuccessStatusCode();

            var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            foreach (var p in products)
            {
                Console.WriteLine("{0} {1} {2} ({3})", p.Id, p.Name, p.Price, p.Category);
            }
        }

        static void ListProduct(int id)
        {
            Console.WriteLine("--------------------- Get Product Id = {0} -------------------------", id);
            var response = _Service.GetAsync(string.Format("services/products/{0}", id)).Result;
            response.EnsureSuccessStatusCode();

            var product = response.Content.ReadAsAsync<Product>().Result;
            Console.WriteLine("ID {0}: {1}", id, product.Name);
        }

        static void ListProducts(string category)
        {
            Console.WriteLine("--------------------- Get Products in Category = {0} ---------------", category);
            Console.WriteLine("Products in '{0}':", category);

            string query = string.Format("services/products?category={0}", category);

            var response = _Service.GetAsync(query).Result;
            response.EnsureSuccessStatusCode();

            var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            foreach (var product in products)
            {
                Console.WriteLine(product.Name);
            }
        }
    }
}
