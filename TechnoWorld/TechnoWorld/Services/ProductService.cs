﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechnoWorld.Abstractions;
using TechnoWorld.Data;

using TechnoWorld.Entities;
using TechnoWorld.Models.Product;

namespace TechnoWorld.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(int categoryId, string model, int brandId, string description, string image, decimal price, decimal quantity, decimal discount)
        {
            var product = new Product
            {

                Model = model,
                Category = _context.Categories.Find(categoryId),
                Brand = _context.Brands.Find(brandId),
                Description = description,
                Image = image,
                Quantity = quantity,
                Discount = discount,
            };
            _context.Products.Add(product);
            return _context.SaveChanges() != 0;
        }






        //public Task Create(ProductCreateVM model, string imagePath)
        //{
        //    var extension = Path.GetExtension(model.Image.FileName).TrimStart('.');

        //    var product = new Product
        //    {
        //        Model = model.Model,
        //        CategoryId = model.CategoryId,
        //        Brand = model.Brand,
        //        Description = model.Description,
        //       // Image=model.Image,
        //        Quantity = model.Quantity,
        //        Discount = model.Discount,
        //    };

        //    //id се създава автоматично при създаване на обект
        //    var dbImage = new Image()
        //    {
        //        Product = product,
        //        Extension = extension
        //    };

        //    //връзваме снимката за кучето
        //    product.ImageId = dbImage.Id;

        //    //създаваме папката images, ако не съществува
        //    Directory.CreateDirectory($"{imagePath}/images/");

        //    //правим физически запис на файла в папка images     
        //    var physicalPath = $"{imagePath}/images/{dbImage.Id}.{extension}";
        //    using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
        //    await model.Image.CopyToAsync(fileStream);


        //    //записваме данните за снимката
        //    await this._context.Images.AddAsync(dbImage);

        //    await this._context.Products.AddAsync(product);
        //    await this._context.SaveChangesAsync();
        //}


        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public List<Product> GetProducts(string searchStringModel, string searchStringDescription)
        {
            List<Product> products = _context.Products.ToList();
            if (!String.IsNullOrEmpty(searchStringModel) && !String.IsNullOrEmpty(searchStringDescription))
            {
                products = products.Where(d => d.Model.Contains(searchStringModel) && d.Description.Contains(searchStringDescription)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringModel))
            {
                products = products.Where(d => d.Model.Contains(searchStringModel)).ToList();
            }
            else if (!string.IsNullOrEmpty(searchStringDescription))
            {
                products = products.Where(d => d.Description.Contains(searchStringDescription)).ToList();
            }
                return products;
            }

        public List<Product> GetProducts()
        {
            List<Product> products = _context.Products.ToList();
            return products;
        }

        public bool RemoveById(int productId)
        {
            {
                var product = GetProductById(productId);
                if (product == default(Product))
                {
                    return false;
                }
                _context.Remove(product);
                return _context.SaveChanges() != 0;
            }
        }

        public bool UpdateProduct(int productId, int categoryId, int brandId, string model, string description, string image, decimal price, decimal quantity, decimal discount)
        {
            var product = GetProductById(productId);
            if (product == default(Product))
            {
                return false;
            }
            product.CategoryId = categoryId;
            product.Model = model;
            product.BrandId = brandId;
            product.Description = description;
            product.Image = image;
            product.Price = price;
            product.Quantity = quantity;
            product.Discount = discount;
            _context.Update(product);
            return _context.SaveChanges() != 0;
        }
    }
   
   


}


