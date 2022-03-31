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
using TechnoWorld.Models.Brand;
using TechnoWorld.Models.Order;
using TechnoWorld.Models.Product;

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
                Model = item.Model,
                BrandId = item.BrandId,
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
            return this.View(products);
        }
        [Authorize]
        public IActionResult My(string searchString)
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.context.Users.SingleOrDefault(u => u.Id == currentUserId);
            if (user == null)
            {
                return null;
            }

            List<OrderListingViewModel> orders = this.context.Orders
                .Where(o => o.ProductUserId == user.Id)
            .Select(o => new OrderListingViewModel
            {
                Id = o.Id,
                ProductId = o.ProductId,
                // ProductName o.Product.Name,
                OrderedOn = o.OrderedOn.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
           BrandName=o.BrandName,
                BrandId=o.BrandId,

                ProductUserId = o.ProductUserId,
                //  ProductUserUsername = o.ProductUserUsername.UserName,
                ProductCount = o.ProductCount
            })
            .ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(o => o.BrandName.Contains(searchString)).ToList();
            }
            return this.View(orders);
        }
    }
}
          
       







