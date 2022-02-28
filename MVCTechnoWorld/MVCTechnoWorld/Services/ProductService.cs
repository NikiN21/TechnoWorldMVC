using MVCTechnoWorld.Abstractions;
using MVCTechnoWorld.Data;
using MVCTechnoWorld.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTechnoWorld.Services
{
    public class ProductService: IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(string model, string category, string brand, string description, string picture, decimal price, int quantity, decimal discount) // string userId
        {
            Product item = new Product
            {
                Model = model,
                Category = category,
                Brand = brand,
                Description = description,
                Picture = picture,
                Price = price,
                Quantity = quantity
        };

            _context.Products.Add(item);
            return _context.SaveChanges() != 0;// Връща броя на записите, които си обработил
        }

       

        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public List<Product> GetProducts()
        {
            List<Product> products = _context.Products.ToList();
            return products;
        }

        public List<Product> GetProducts(string searchStringType, string searchStringBrand)
        {
            List<Product> products = _context.Products.ToList();
            if (!String.IsNullOrEmpty(searchStringType) && !String.IsNullOrEmpty(searchStringBrand))
            {
                products = products.Where(d => d.Model.Contains(searchStringType) && d.Brand.Contains(searchStringBrand)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringType))
            {
                products = products.Where(d => d.Model.Contains(searchStringType)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringBrand))
            {
                products = products.Where(d => d.Brand.Contains(searchStringBrand)).ToList();
            }
            return products;
        }

        public bool RemoveById(int productId)
        {
            var product = GetProductById(productId);
            if (product == default(Product))
            {
                return false;
            }
            _context.Remove(product);
            return _context.SaveChanges() != 0;
        }


        public bool UpdateProduct(int productId, string model, string category, string brand, string description, string picture, decimal price, int quantity, decimal discount)
        {
            var product = GetProductById(productId);
            if (product == default(Product))
            {
                return false;
            }
            product.Model = model;
            product.Category = category;
            product.Brand = brand;
            product.Description = description;
            product.Picture = picture;
            product.Price = price;
            product.Quantity = quantity;
            _context.Update(product);
            return _context.SaveChanges() != 0;
        }
    }
}


