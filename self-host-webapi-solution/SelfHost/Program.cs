using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace MyWork.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = ConfigurationManager.AppSettings["host-address"];
            var config = new HttpSelfHostConfiguration(address);

            config.Routes.MapHttpRoute(
                "API Default", "services/{controller}/{id}",
                new { id = RouteParameter.Optional });

            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Hosting a Product Rest Service through .Net 4.5 WebApi at \n\r{0}/services/products \n\r", address);
                Console.WriteLine("Product Service has three actions");
                Console.WriteLine("1 - /services/products/id\t\t - get product by id(int)  ");
                Console.WriteLine("2 - /services/products?category=? \t - get products by category(string)");
                Console.WriteLine("3 - /services/products/ \t\t - get all products");
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}
