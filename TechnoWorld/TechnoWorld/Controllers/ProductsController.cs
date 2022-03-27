using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoWorld.Abstractions;

using TechnoWorld.Entities;
using TechnoWorld.Models.Brand;
using TechnoWorld.Models.Product;

namespace TechnoWorld.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
         private readonly IWebHostEnvironment _hostEnvironment;


        public ProductsController(IProductService productService, ICategoryService categoryService, IBrandService brandService, IWebHostEnvironment hostEnvironment )
        {
           this._productService = productService;
            this._categoryService = categoryService;
            this._hostEnvironment = hostEnvironment;
            this._brandService = brandService;
           
        }
        // GET: ProductsController

        public ActionResult Create()
        {
            var product = new ProductCreateVM();

            product.Categories = _categoryService.GetCategories()
             .Select(c => new CategoryChoiceVM()
             {
                 Name = c.Name,
                 Id = c.Id,
             })
        .ToList();
            product.Brands = _brandService.GetBrands()
             .Select(c => new BrandChoiceVM()
             {
                 Name = c.Name,
                 Id = c.Id,
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
                input.Categories = _categoryService.GetCategories()
                    .Select(c => new CategoryChoiceVM()
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList();
                input.Brands = _brandService.GetBrands()
                    .Select(c => new BrandChoiceVM()
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
        public ActionResult All(string searchStringModel, string searchStringDescription)
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
            return this.View(products);
        }

        //List<ProductAllVM> orders = context
        //        .Orders
        //        .Where(x => x.CustomerId == user.Id)
        //        .Select(x => new ProductAllVM
        //        {
        //            Id = x.Id,
        //            EventId = x.EventureId,
        //            EventName = x.Eventure.Name,
        //            EventStart = x.Eventure.Start.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
        //            EventEnd = x.Eventure.End.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
        //            EventPlace = x.Eventure.Place,
        //            OrderedOn = x.OrderedOn.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
        //            CustomerId = x.CustomerId,
        //            CustomerUsername = x.Customer.UserName,
        //            TicketsCount = x.TicketsCount


                    //if (!String.IsNullOrEmpty(searchStringModel) && !String.IsNullOrEmpty(searchStringBrand))
                    //{
                    //    products = products.Where(d => d.Model.Contains(searchStringModel) && d.Brand.Contains(searchStringBrand)).ToList();
                    //}
                    //else if (!String.IsNullOrEmpty(searchStringModel))
                    //{
                    //    products = products.Where(d => d.Model.Contains(searchStringModel)).ToList();
                    //}
                    //else if (!string.IsNullOrEmpty(searchStringBrand))
                    //{
                    //    products = products.Where(d => d.Brand.Contains(searchStringBrand)).ToList();
                    //}




                }
}
          



