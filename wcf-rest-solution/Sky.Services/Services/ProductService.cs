using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Sky.Services
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        [WebGet(UriTemplate = "product/{id}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        Product GetSingle(string id);

        [OperationContract]
        [WebGet(UriTemplate = "products/{count}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Product> GetProducts(string count);
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ProductService : IProductService
    {
        static readonly Random _Random = new Random();

        public Product GetSingle(string id)
        {
            return new Product { Id = id.ToInt32(), Name = "PN : " + id };
        }

        public IEnumerable<Product> GetProducts(string value)
        {
            var count = value.ToInt32();
            var products = new List<Product>(count);

            for (int i = 0; i < count; i++)
            {
                var id = _Random.Next(1000, 9999);
                products.Add(new Product { Id = id, Name = "PN : " + id });
            }
            
            return products;
        }
    }


    public static class SafeConverter
    {
        public static int ToInt32(this string value)
        {
            return Convert.ToInt32(value);
        }
    }
}
