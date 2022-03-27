﻿using System;
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
       // bool Create( int categoryId, string model, int brandId, string description, string image, decimal price, decimal quantity, decimal discount);

        bool UpdateProduct(int productId, int categoryId,int brandId, string model,   string description,  decimal price, decimal quantity, decimal discount);

       List<ProductAllVM> GetProducts();

        Product GetProductById(int productId);

        bool RemoveById(int productId);

        List<Product> GetProducts(string searchStringModel, string searchStringDescription);
      
    }
}

