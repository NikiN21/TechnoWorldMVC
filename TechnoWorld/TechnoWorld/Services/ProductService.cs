using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoWorld.Abstractions;
using TechnoWorld.Data;
using TechnoWorld.Domain;

namespace TechnoWorld.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }


        public bool Create(int categoryId, string model, string brand, string description, string picture, int price, int quantity, int discount)
        {
            var product = new Product
            {
                Model = model,
                Category = _context.Categories.Find(categoryId),
                Brand = brand,
                Description = description,
                Picture = picture,
                Quantity = quantity,
                Discount = discount,
            };
            _context.Products.Add(product);
            return _context.SaveChanges() != 0;
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public List<Product> GetProducts(string searchStringModel, string searchStringBrand)
        {
            List<Product> products = _context.Products.ToList();
            if (!String.IsNullOrEmpty(searchStringModel) && !String.IsNullOrEmpty(searchStringBrand))
            {
                products = products.Where(d => d.Model.Contains(searchStringModel) && d.Brand.Contains(searchStringBrand)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringModel))
            {
                products = products.Where(d => d.Model.Contains(searchStringModel)).ToList();
            }
            else if (!string.IsNullOrEmpty(searchStringBrand))
            {
                products = products.Where(d => d.Brand.Contains(searchStringBrand)).ToList();
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

        public bool UpdateProduct(int productId, int categoryId, string model, string brand, string description, string picture, int price, int quantity, int discount)
        {
            var product = GetProductById(productId);
            if (product == default(Product))
            {
                return false;
            }
            product.CategoryId = categoryId;
            product.Model = model;
            product.Brand = brand;
            product.Description = description;
            product.Picture = picture;
            product.Price = price;
            product.Quantity = quantity;
            product.Discount = discount;
            _context.Update(product);
            return _context.SaveChanges() != 0;
        }
    }

}

       

