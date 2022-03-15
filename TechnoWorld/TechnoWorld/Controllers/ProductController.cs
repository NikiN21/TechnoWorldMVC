using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoWorld.Data;
using TechnoWorld.Domain;
using TechnoWorld.Models;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TechnoWorld.Controllers
{

    public class ProductController : Controller

    {     
    private readonly ApplicationDbContext context;
    public ProductController(ApplicationDbContext context)
    {
        this.context = context;
    }
    public IActionResult Index()
    {
        return View();
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
            Product productFromDb = new Product
            {
                Id = bindingModel.Id,
                Model = bindingModel.Model,
                Category = bindingModel.Category,
                Brand = bindingModel.Brand,
                Description = bindingModel.Description,
                Picture = bindingModel.Picture,
                Price = bindingModel.Price,
                Quantity = bindingModel.Quantity,
                Discount = bindingModel.Discount
            };
            context.Products.Add(productFromDb);
            context.SaveChanges();
            return this.RedirectToAction("Success");
        }
        return this.View();
    }
    public IActionResult Success()
    {
        return this.View();
    }

    public IActionResult All(string searchStringModel)
    {
        List<ProductAllViewModel> products = context.Products.Select(productFromDb => new ProductAllViewModel
        {
            Id = productFromDb.Id,
            Model = productFromDb.Model,
            Category = productFromDb.Category,
            Brand = productFromDb.Brand,
            Description = productFromDb.Description,
            Picture = productFromDb.Picture,
            Price = productFromDb.Price,
            Quantity = productFromDb.Quantity,
            Discount = productFromDb.Discount
        }).ToList();
        if (!String.IsNullOrEmpty(searchStringModel))
        {
            products = products.Where(x => x.Model.ToLower() == searchStringModel.ToLower())
                .ToList();
        }
        return this.View(products);
    }


    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Product item = context.Products.Find(id);
        if (item == null)
        {
            return NotFound();
        }
            ProductCreateViewModel product = new ProductCreateViewModel()

        {
                Id = item.Id,
                Model = item.Model,
                Category = item.Category,
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
    public IActionResult Edit(ProductCreateViewModel bindingModel)
    {
        if (ModelState.IsValid)
        {
                Product product = new Product
                {
                    Id = bindingModel.Id,
                    Model = bindingModel.Model,
                    Category = bindingModel.Category,
                    Brand = bindingModel.Brand,
                    Description = bindingModel.Description,
                    Picture = bindingModel.Picture,
                    Price = bindingModel.Price,
                    Quantity = bindingModel.Quantity,
                    Discount = bindingModel.Discount
                };
            context.Products.Update(product);
            context.SaveChanges();
            return this.RedirectToAction("All");
        }
        return View(bindingModel);
    }
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
            Product item = context.Products.Find(id);
        if (item == null)
        {
            return NotFound();
        }
            ProductCreateViewModel product = new ProductCreateViewModel()

        {
                Id = item.Id,
                Model = item.Model,
                Category = item.Category,
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
    public IActionResult Delete(int id)
    {
            Product item = context.Products.Find(id);
        if (item == null)
        {
            return NotFound();
        }
        context.Products.Remove(item);
        context.SaveChanges();
        return this.RedirectToAction("All");


    }


}
}


