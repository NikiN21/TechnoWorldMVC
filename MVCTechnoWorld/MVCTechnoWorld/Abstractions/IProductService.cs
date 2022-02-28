using MVCTechnoWorld.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTechnoWorld.Abstractions
{
    public interface IProductService
    {
        bool Create(string type, string category, string brand, string description, string picture, decimal price, int quantity, decimal discount);
        bool UpdateProduct(int productId, string type, string category, string brand, string description, string picture, decimal price, int quantity, decimal discount);
        List<Product> GetProducts();
        Product GetProductById(int productId);
        bool RemoveById(int productId);
        List<Product> GetProducts(string searchStringType, string searchStringBrand);
    }
}
