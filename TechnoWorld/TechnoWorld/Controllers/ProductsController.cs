using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoWorld.Abstractions;
using TechnoWorld.Domain;
using TechnoWorld.Models.Category;
using TechnoWorld.Models.Product;

namespace TechnoWorld.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;


        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        // GET: ProductsController

        public ActionResult Create()
        {
            var product = new ProductCreateVM();
            product.Categories = _categoryService.GetCategories()
             .Select(c => new CategoryChoiceVM()
             {
                 Id = c.Id,
                 Name = c.Name,

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
                var created = _productService.Create(product.CategoryId, product.Brand, product.Model, product.Description, product.Picture, product.Price, product.Quantity, product.Discount);
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
                Brand = item.Brand,
                Description = item.Description,
                Picture = item.Picture,
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
                var updated = _productService.UpdateProduct(id, bindingModel.CategoryId, bindingModel.Model, bindingModel.Brand, bindingModel.Description, bindingModel.Picture, bindingModel.Price, bindingModel.Quantity, bindingModel.Discount);
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
                Brand = item.Brand,
                Description = item.Description,
                Picture = item.Picture,
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
        public IActionResult All(string searchStringModel, string searchStringBrand)
        {
            List<ProductAllVM> products = _productService.GetProducts(searchStringModel, searchStringBrand)
            .Select(productFromDb => new ProductAllVM
            {
                Id = productFromDb.Id,
                CategoryId = productFromDb.CategoryId,
                Model = productFromDb.Model,
                Brand = productFromDb.Brand,
                Description = productFromDb.Description,
                Picture = productFromDb.Picture,
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
          



