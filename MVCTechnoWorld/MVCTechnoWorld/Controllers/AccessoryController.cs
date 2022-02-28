using Microsoft.AspNetCore.Mvc;
using MVCTechnoWorld.Domain;
using MVCTechnoWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCTechnoWorld.Data;
using Microsoft.AspNetCore.Authorization;
using MVCTechnoWorld.Abstractions;
using Microsoft.AspNetCore.Http;

namespace MVCTechnoWorld.Controllers
{
    [Authorize]
    public class AccessoryController : Controller
    {
        private readonly IAccessoryService _accessoryService;

        public AccessoryController(IAccessoryService accessoriesService)
        {
            this._accessoryService = accessoriesService;
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
                //string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var created = _accessoryService.Create(bindingModel.Type, bindingModel.Category, bindingModel.Brand, bindingModel.Description, bindingModel.Picture, bindingModel.Price, bindingModel.Quantity, bindingModel.Discount);
                if (created)
                {
                    return this.RedirectToAction("Success");
                }
            }

            return this.View();
        }
        public IActionResult Edit(int id)
        {
            Accessory item = _accessoryService.GetAccessoryById(id);
            if (item == null)
            {
                return NotFound();
            }
            AccessoryCreateViewModel accessory = new AccessoryCreateViewModel()
            {
                Id = item.Id,
                Type = item.Type,
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
        public IActionResult Edit(int id, AccessoryCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var updated = _accessoryService.UpdateAccessory(id, bindingModel.Type, bindingModel.Category, bindingModel.Brand, bindingModel.Description, bindingModel.Picture, bindingModel.Price, bindingModel.Quantity, bindingModel.Discount);
                if (updated)
                {
                    return this.RedirectToAction("All");
                }

            }
            return View(bindingModel);

        }


        public IActionResult Delete(int id)
        {
            Accessory item = _accessoryService.GetAccessoryById(id);
            if (item == null)
            {
                return NotFound();
            }
            AccessoryCreateViewModel accessory = new AccessoryCreateViewModel()
            {
                Id = item.Id,
                Type = item.Type,
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
        public IActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _accessoryService.RemoveById(id);

            if (deleted)
            {
                return this.RedirectToAction("All", "Accessory");
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

            List<AccessoryAllViewModel> accessory = _accessoryService.GetAccessories(searchStringType, searchStringBrand)
                .Select(accessoriesFromDb => new AccessoryAllViewModel
                {
                    Id = accessoriesFromDb.Id,
                    Type = accessoriesFromDb.Type,
                    Category = accessoriesFromDb.Category,
                    Brand = accessoriesFromDb.Brand,
                    Description = accessoriesFromDb.Description,
                    Picture = accessoriesFromDb.Picture,
                    Price = accessoriesFromDb.Price,
                    Quantity = accessoriesFromDb.Quantity,
                    Discount = accessoriesFromDb.Discount
                    
                    //FullName = accessoriesFromDb.Owner.FirstName + " " + accessoriesFromDb.Owner.LastName
                }).ToList();

            return this.View(accessory);

        }


    }
}
