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
        // private readonly IWebHostEnvironment _hostEnvironment;


        public ProductsController(IProductService productService, ICategoryService categoryService, IBrandService brandService) //IWebHostEnvironment hostEnvironment 
        {
           this._productService = productService;
            this._categoryService = categoryService;
            this._brandService = brandService;
            //this._hostEnvironment = hostEnvironment;
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

        public ActionResult Create([FromForm] ProductCreateVM product)
        {
            if (ModelState.IsValid)
            {
                var created = _productService.Create(product.CategoryId, product.Model, product.BrandId, product.Description, product.Image, product.Price, product.Quantity, product.Discount);
                if (created)
                {
                    return this.RedirectToAction("Success");
                }
            }
            return View(product);
        }

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
                Image = item.Image,
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
                var updated = _productService.UpdateProduct(id, bindingModel.CategoryId, bindingModel.BrandId, bindingModel.Model, bindingModel.Description, bindingModel.Image, bindingModel.Price, bindingModel.Quantity, bindingModel.Discount);
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
                Image = item.Image,
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
        public IActionResult All(string searchStringModel, string searchStringDescription)
        {
            List<ProductAllVM> products = _productService.GetProducts(searchStringModel, searchStringDescription)
            .Select(productFromDb => new ProductAllVM
            {
                Id = productFromDb.Id,
                CategoryId = productFromDb.CategoryId,
                Model = productFromDb.Model,
                BrandId = productFromDb.BrandId,
                Description = productFromDb.Description,
                Image = productFromDb.Image,
                Price = productFromDb.Price,
                Quantity = productFromDb.Quantity,
                Discount = productFromDb.Discount
            }).ToList();
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
            return this.View(products);
        }

    }
}
          



