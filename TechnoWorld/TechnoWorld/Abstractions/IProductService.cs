using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TechnoWorld.Entities;
using TechnoWorld.Models.Product;

namespace TechnoWorld.Abstractions
{
    public interface IProductService
    {
        Task Create(ProductCreateVM model, string imagePath);
        bool UpdateProduct(int productId, int categoryId,int brandId, string model,   string description,  decimal price, int quantity, decimal discount);
       List<ProductAllVM> GetProducts();
        Product GetProductById(int productId);
        bool RemoveById(int productId);
        List<Product> GetProducts(string searchStringCategoryName, string searchStringBrandName);
        public bool MakeDiscount(int id, decimal discount);
        public bool RemoveDiscount(int id);
        int countProducts();
        int countUsers();
        int countOrders();
        //decimal Price();

    }
}

