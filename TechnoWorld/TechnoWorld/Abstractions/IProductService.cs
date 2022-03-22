using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoWorld.Domain;

namespace TechnoWorld.Abstractions
{
    public interface IProductService
    {
        bool Create( int categoryId, string model, string brand, string description, string picture, int price, int quantity, int discount);

        bool UpdateProduct(int productId, int categoryId, string model, string brand,  string description, string picture, int price, int quantity, int discount);

       List<Product> GetProducts();

        Product GetProductById(int productId);

        bool RemoveById(int productId);

        List<Product> GetProducts(string searchStringModel, string searchStringBrand);
    }
}

