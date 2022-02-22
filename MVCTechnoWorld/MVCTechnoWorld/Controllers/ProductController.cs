using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCTechnoWorld.Data;
using MVCTechnoWorld.Domain;
using MVCTechnoWorld.Models;
using Microsoft.AspNetCore.Authorization;
using MVCTechnoWorld.Abstractions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MVCTechnoWorld.Controllers
{
    [Authorize]
    public class ProductController : Controller
    
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productsService)
        {
            this._productService = productsService;
        }


        public IActionResult Create()
        {
            return this.View();

        }

        [HttpPost]
        public IActionResult Create(ProductCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                //string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var created = _productService.Create(bindingModel.Type, bindingModel.Brand, bindingModel.Color, bindingModel.Price, bindingModel.Description, bindingModel.Picture);
                if (created)
                {
                    return this.RedirectToAction("Success");
                }
            }

            return this.View();
        }
        public IActionResult Edit(int id)
        {
            Product item = _productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductCreateViewModel product = new ProductCreateViewModel()
            {
                Id = item.Id,
                Type = item.Type,
                Brand = item.Brand,
                Color = item.Color,
                Price = item.Price,
                Description = item.Description,
                Picture = item.Picture
            };
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var updated = _productService.UpdateProduct(id, bindingModel.Type, bindingModel.Brand, bindingModel.Color, bindingModel.Price, bindingModel.Description, bindingModel.Picture);
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
            ProductCreateViewModel product = new ProductCreateViewModel()
            {
                Id = item.Id,
                Type = item.Type,
                Brand = item.Brand,
                Color = item.Color,
                Price = item.Price,
                Description = item.Description,
                Picture = item.Picture
            };
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _productService.RemoveById(id);

            if (deleted)
            {
                return this.RedirectToAction("All", "Product");
            }
            else
            {
                return View();
            }

        }
        public IActionResult Success()
        {
            return this.View();

        }
        [AllowAnonymous]
        public IActionResult All(string searchStringType, string searchStringBrand)
        {

            List<ProductAllViewModel> products = _productService.GetProducts(searchStringType, searchStringBrand)
                .Select(productFromDb => new ProductAllViewModel
                {
                    Id = productFromDb.Id,
                    Type = productFromDb.Type,
                    Brand = productFromDb.Brand,
                    Color = productFromDb.Color,
                    Price = productFromDb.Price,
                    Description = productFromDb.Description,
                    Picture = productFromDb.Picture
                    //FullName = productFromDb.Owner.FirstName + " " + productFromDb.Owner.LastName
                }).ToList();

            return this.View(products);

        }

      
    }
}


