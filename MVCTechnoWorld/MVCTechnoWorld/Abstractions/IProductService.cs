using MVCTechnoWorld.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTechnoWorld.Abstractions
{
    public interface IProductService
    {
        bool Create(string type, string brand, string color, decimal price, string description, string picture);
        bool UpdateProduct(int productId, string type, string brand, string color, decimal price, string description, string picture);
        List<Product> GetProducts();
        Product GetProductById(int productId);
        bool RemoveById(int productId);
        List<Product> GetProducts(string searchStringType, string searchStringBrand);
    }
}
