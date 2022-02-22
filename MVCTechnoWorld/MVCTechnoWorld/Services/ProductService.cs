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
        public bool Create(string type, string brand, string color, decimal price, string description, string picture) // string userId
        {
            Product item = new Product
            {
                Type = type,
                Brand = brand,
                Color = color,
                Price = price,
                Description = description,
                Picture = picture
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
                products = products.Where(d => d.Type.Contains(searchStringType) && d.Brand.Contains(searchStringBrand)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringType))
            {
                products = products.Where(d => d.Type.Contains(searchStringType)).ToList();
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


        public bool UpdateProduct(int productId, string type, string brand, string color, decimal price, string description, string picture)
        {
            var product = GetProductById(productId);
            if (product == default(Product))
            {
                return false;
            }
            product.Type = type;
            product.Brand = brand;
            product.Color = color;
            product.Price = price;
            product.Description = description;
            product.Picture = picture;
            _context.Update(product);
            return _context.SaveChanges() != 0;
        }
    }
}


