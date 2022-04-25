
using Microsoft.AspNetCore.Http;
using System;
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

        //public bool Create(int categoryId,  int brandId, string model, string description, decimal price, decimal quantity, decimal discount)
        //{
        //    var product = new Product
        //    {

        //        Model = model,
        //        Category = _context.Categories.Find(categoryId),
        //        Brand = _context.Brands.Find(brandId),
        //        Description = description,
               
        //        Quantity = quantity,
        //        Discount = discount,
        //    };
        //    _context.Products.Add(product);
        //    return _context.SaveChanges() != 0;
        //}

        public async Task Create(ProductCreateVM model, string imagePath)
        {
            var extension = Path.GetExtension(model.Image.FileName).TrimStart('.');

            var product = new Product
            {
                Model = model.Model,


                BrandId = model.BrandId,
                CategoryId = model.CategoryId,

                Price = model.Price,
                Description = model.Description,
                Quantity = model.Quantity,
                Discount = 0,
               // Discount = model.Discount,
            };

            var dbImage = new Image()
            {
                Products = product,
                Extension = extension
            };
            //id се създава автоматично при създаване на обект

            product.ImageId = dbImage.Id; //връзваме снимката за кучето

            Directory.CreateDirectory($"{imagePath}/images/");
            //създаваме папката images, ако не съществува

            var physicalPath = $"{imagePath}/images/{dbImage.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.Image.CopyToAsync(fileStream);

            await this._context.Images.AddAsync(dbImage);

            await this._context.Products.AddAsync(product);
            await this._context.SaveChangesAsync();


        }

        public Product GetProductById(int productId)
        {
          
            return _context.Products.Find(productId);
        }


        public List<Product> GetProducts(string searchStringCategoryName, string searchStringBrandName)
        {
            List<Product> products = _context.Products.ToList();
            if (!String.IsNullOrEmpty(searchStringCategoryName) && !String.IsNullOrEmpty(searchStringBrandName))
            {
                products = products.Where(d => d.Category.Name.Contains(searchStringCategoryName) && d.Brand
                .Name.Contains(searchStringBrandName)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringCategoryName))
            {
                products = products.Where(d => d.Category.Name.Contains(searchStringCategoryName)).ToList();
            }
            else if (!string.IsNullOrEmpty(searchStringBrandName))
            {
                products = products.Where(d => d.Brand.Name.Contains(searchStringBrandName)).ToList();
            }
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

        public bool UpdateProduct(int productId, int categoryId, int brandId, string model, string description, decimal price, int quantity, decimal discount)
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
            
            product.Price = price;
            product.Quantity = quantity;
            product.Discount = discount;
            _context.Update(product);
            return _context.SaveChanges() != 0;
        }

       public List<ProductAllVM> GetProducts()
        {
           
            List<ProductAllVM> products = _context.Products
                .Select(d => new ProductAllVM
                {
                    Id = d.Id,
                    CategoryId = d.CategoryId,
                    CategoryName = d.Category.Name,
                    Model = d.Model,
                    BrandId = d.BrandId,
                    BrandName = d.Brand.Name,
                    Description = d.Description,
                    ImageUrl = $"/images/{d.ImageId}.{d.Image.Extension}",
                    Price = d.Price,
                    Quantity = d.Quantity,
                    Discount = d.Discount
                }).ToList();

            return products;
        }
        public bool MakeDiscount(int id, decimal discount)
        {
            var product = GetProductById(id);

            if (product == null)
            {
                return false;
            }

            //product.Discount = product.Price * discount / 100;
            // product.Price -= product.Discount;

            product.Discount = discount;
            product.Price = product.Price - product.Price*product.Discount / 100;

            _context.Products.Update(product);

            return _context.SaveChanges() != 0;
        }

        public bool RemoveDiscount(int id)
        {
            var product = GetProductById(id);

            if (product == null)
            {
                return false;
            }

            product.Price = product.Price * 100 / (100 - product.Discount);
            // product.Price += product.Discount;
            product.Discount = 0;

            _context.Products.Update(product);

            return _context.SaveChanges() != 0;
        }

        public int countProducts()
        {
            return _context.Products.Count();
        }

        public int countUsers()
        {
            return _context.Users.Count();
        }

        public int countOrders()
        {
            return _context.Orders.Count();
        }
        //public decimal Price()
        //{
        //    return _context.Orders.Sum();
        //}
    }
}
   
   





