using Microsoft.AspNetCore.Mvc;
using TechnoWorld.Domain;
using TechnoWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoWorld.Data;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Http;

namespace TechnoWorld.Controllers
{
    
    public class AccessoryController : Controller
    {
        private readonly ApplicationDbContext context;
        public AccessoryController(ApplicationDbContext context)
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
        public IActionResult Create(AccessoryCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                Accessory accessoryFromDb = new Accessory
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
                context.Accessories.Add(accessoryFromDb);
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
            List<AccessoryAllViewModel> accessories = context.Accessories.Select(accessoryFromDb => new AccessoryAllViewModel
            {
                Id = accessoryFromDb.Id,
                Model = accessoryFromDb.Model,
                Category = accessoryFromDb.Category,
                Brand = accessoryFromDb.Brand,
                Description = accessoryFromDb.Description,
                Picture = accessoryFromDb.Picture,
                Price = accessoryFromDb.Price,
                Quantity = accessoryFromDb.Quantity,
                Discount = accessoryFromDb.Discount
            }).ToList();
            if (!String.IsNullOrEmpty(searchStringModel))
            {
                accessories = accessories.Where(x => x.Model.ToLower() == searchStringModel.ToLower())
                    .ToList();
            }
            return this.View(accessories);
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
            AccessoryCreateViewModel accessory = new AccessoryCreateViewModel()

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
            return View(accessory);
        }

        [HttpPost]
        public IActionResult Edit(AccessoryCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                Accessory accessory = new Accessory
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
                context.Accessories.Update(accessory);
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
            Accessory item = context.Accessories.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            AccessoryCreateViewModel accessory = new AccessoryCreateViewModel()

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
            return View(accessory);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Accessory item = context.Accessories.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            context.Accessories.Remove(item);
            context.SaveChanges();
            return this.RedirectToAction("All");


        }


    }
}