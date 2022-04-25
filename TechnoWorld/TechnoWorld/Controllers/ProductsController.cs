using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TechnoWorld.Abstractions;
using TechnoWorld.Data;
using TechnoWorld.Entities;
using TechnoWorld.Models;
using TechnoWorld.Models.Brand;
using TechnoWorld.Models.Order;
using TechnoWorld.Models.Product;
using TechnoWorld.Models.Products;

namespace TechnoWorld.Controllers

{
    public class ProductsController : Controller
    { 
       
        
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext context;

        public ProductsController(IProductService productService, ICategoryService categoryService, IBrandService brandService, IWebHostEnvironment hostEnvironment, ApplicationDbContext context)
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._hostEnvironment = hostEnvironment;
            this.context = context;
            this._brandService = brandService;

        }
       

        // GET: ProductsController
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var product = new ProductCreateVM();
            product.Brands = _brandService.GetBrands()
                .Select(b => new BrandChoiceVM()
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToList();

            product.Categories = _categoryService.GetCategories()
               .Select(c => new CategoryChoiceVM()
               {
                   Id = c.Id,
                   Name = c.Name
               })
               .ToList();
            return View(product);


         
        }




        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create([FromForm] ProductCreateVM input)
        {

            var imagePath = $"{this._hostEnvironment.WebRootPath}";
            if (!ModelState.IsValid)
            {
                input.Brands = _brandService.GetBrands()
                .Select(b => new BrandChoiceVM()
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToList();

                input.Categories = _categoryService.GetCategories()
               .Select(c => new CategoryChoiceVM()
               {
                   Id = c.Id,
                   Name = c.Name
               })
               .ToList();


                return View(input);

            }
            await this._productService.Create(input, imagePath);

            return RedirectToAction(nameof(All));
        }

        //    var imagePath = $"{this._hostEnvironment.WebRootPath}";
        //    if (!ModelState.IsValid)
        //    {
        //        input.Categories = _categoryService.GetCategories()
        //            .Select(c => new CategoryChoiceVM()
        //            {
        //                Id = c.Id,
        //                Name = c.Name
        //            })
        //            .ToList();
        //        input.Brands = _brandService.GetBrands()
        //            .Select(c => new BrandChoiceVM()
        //            {
        //                Id = c.Id,
        //                Name = c.Name
        //            })
        //            .ToList();
        //        return View(input);
        //    }
        //    await this._productService.Create(input, imagePath);
        //    return RedirectToAction(nameof(All));
        //}
        //if (ModelState.IsValid)
        //{
        //    var created = _productService.Create(product.CategoryId, product.Model, product.BrandId, product.Description, product.Image, product.Price, product.Quantity, product.Discount);
        //    if (created)
        //    {
        //        return this.RedirectToAction("Success");
        //    }
        //}
        //return View(product);


        public ActionResult Success()
        {
            return this.View();

        }



        public IActionResult Edit(int id)
        {
            Product item = _productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }

            ProductCreateVM product = new ProductCreateVM()

            {
                Id = item.Id,
                CategoryId = item.CategoryId,
                Model = item.Model,
                BrandId = item.BrandId,
                Description = item.Description,

                Price = item.Price,
                Quantity = item.Quantity,
                Discount = item.Discount
            };
            product.Brands = _brandService.GetBrands()
               .Select(b => new BrandChoiceVM()
               {
                   Id = b.Id,
                   Name = b.Name
               })
               .ToList();

            product.Categories = _categoryService.GetCategories()
               .Select(c => new CategoryChoiceVM()
               {
                   Id = c.Id,
                   Name = c.Name
               })
               .ToList();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductCreateVM bindingModel)
        {
            if (ModelState.IsValid)
            {
                var updated = _productService.UpdateProduct(id, bindingModel.CategoryId, bindingModel.BrandId, bindingModel.Model, bindingModel.Description, bindingModel.Price, bindingModel.Quantity, bindingModel.Discount);
                if (updated)
                {
                    return this.RedirectToAction("All");
                }
            }
            
           
           
            return View(bindingModel);
        }

        public IActionResult Delete(int id)
        {

            Product item = _productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductCreateVM product = new ProductCreateVM()

            {
                Id = item.Id,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.Name,
                Model = item.Model,
                BrandId = item.BrandId,
                BrandName = item.Brand.Name,
                Description = item.Description,
                Price = item.Price,
                Quantity = item.Quantity,
                Discount = item.Discount
            };
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _productService.RemoveById(id);
            if (deleted)
            {
                return this.RedirectToAction("All");
            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous]
        public ActionResult All(string searchStringCategoryName, string searchStringBrandName)
        {
            //List<ProductAllVM> products = _productService.GetProducts(searchStringModel, searchStringDescription)
            //.Select(productFromDb => new ProductAllVM
            //{
            //    Id = productFromDb.Id,
            //    CategoryId = productFromDb.CategoryId,
            //    CategoryName = productFromDb.Category.Name,
            //    Model = productFromDb.Model,
            //    BrandId = productFromDb.BrandId,
            //    BrandName = productFromDb.Brand.Name,
            //    Description = productFromDb.Description,
            //    ImageUrl=productFromDb.ImageUrl,
            //    Price = productFromDb.Price,
            //    Quantity = productFromDb.Quantity,
            //    Discount = productFromDb.Discount
            //}).ToList();

            var products = _productService.GetProducts();
            if (!String.IsNullOrEmpty(searchStringCategoryName))
            {
                products = products.Where(x => x.CategoryName.ToLower() == searchStringCategoryName.ToLower())
                    .ToList();
            }
            if (!String.IsNullOrEmpty(searchStringBrandName))
            {
                products = products.Where(x => x.BrandName.ToLower() == searchStringBrandName.ToLower())
                 .ToList();
            }
            return this.View(products);
        }
        public IActionResult Laptop()
        {
            var products = _productService.GetProducts().Where(x => x.CategoryName == "Laptop").ToList();
         
          
            return this.View(products);
        }
        public IActionResult Monitor()
        {
            var products = _productService.GetProducts().Where(x => x.CategoryName == "Monitor").ToList();


            return this.View(products);
        }
        public IActionResult Accessory()
        {
            var products = _productService.GetProducts().Where(x => x.CategoryName == "Accessory").ToList();
            return this.View(products);
        }
        public IActionResult Promotion()
        {
            var products = _productService.GetProducts().Where(x => x.Discount > 0).ToList();
            return this.View(products);
        }
        public IActionResult Computer()
        {
            var products = _productService.GetProducts().Where(x => x.CategoryName == "Computer").ToList();
            return this.View(products);
        }
        public IActionResult TV()
        {
            var products = _productService.GetProducts().Where(x => x.CategoryName == "TV").ToList();
            return this.View(products);
        }
        public IActionResult MobilePhone()
        {
            var products = _productService.GetProducts().Where(x => x.CategoryName == "Mobile phone").ToList();
            return this.View(products);
        }
        public IActionResult SmartWatch()
        {
            var products = _productService.GetProducts().Where(x => x.CategoryName == "Smart watch").ToList();
            return this.View(products);
        }
        public IActionResult MakeDiscount(int id)
        {
            var x = _productService.GetProductById(id);

            ProductPromotionViewModel product = new ProductPromotionViewModel
            {
                Id = x.Id,
                Model = x.Model,
                BrandId = x.BrandId,
                OldPrice = x.Price,
                NewPrice = x.Price,
                ImageUrl = x.ImageUrl,
            };
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeDiscount(int id, decimal discount)
        {
            _productService.MakeDiscount(id, discount);

            return this.RedirectToAction("All");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveDiscount(int id, int discount)
        {
            _productService.RemoveDiscount(id);

            return this.RedirectToAction("All");
        }
        public IActionResult AllDiscounts()
        {
            var products = new List<ProductAllVM>();

            foreach (var product in _productService.GetProducts().Where(x => x.Discount != 0))
            {
                var viewModel = new ProductAllVM
                {
                    Id = product.Id,
                    ImageUrl = product.ImageUrl,
                    CategoryId = product.CategoryId,
                    BrandId = product.BrandId,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    Discount = product.Discount
                };
                products.Add(viewModel);
            }
            return View("All", products);
        }
        public IActionResult Statistic()
        {
            StatisticVM statistic = new StatisticVM();

            statistic.countProducts = _productService.countProducts();
            statistic.countUsers = _productService.countUsers();
            statistic.countOrders = _productService.countOrders();
             statistic.Price = context.Orders.Sum(x => x.Price * x.Count);
            return View(statistic);
        }

        
    }
}







