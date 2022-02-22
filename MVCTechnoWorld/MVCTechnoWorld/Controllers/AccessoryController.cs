using Microsoft.AspNetCore.Mvc;
using MVCTechnoWorld.Domain;
using MVCTechnoWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCTechnoWorld.Data;
using MVCTechnoWorld.Domain;
using MVCTechnoWorld.Models;
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
                var created = _accessoryService.Create(bindingModel.Type, bindingModel.Brand, bindingModel.Color, bindingModel.Price, bindingModel.Description, bindingModel.Picture);
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
                Brand = item.Brand,
                Color = item.Color,
                Price = item.Price,
                Description = item.Description,
                Picture = item.Picture
            };
            return View(accessory);
        }

        [HttpPost]
        public IActionResult Edit(int id, AccessoryCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var updated = _accessoryService.UpdateAccessory(id, bindingModel.Type, bindingModel.Brand, bindingModel.Color, bindingModel.Price, bindingModel.Description, bindingModel.Picture);
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
                Brand = item.Brand,
                Color = item.Color,
                Price = item.Price,
                Description = item.Description,
                Picture = item.Picture
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
                    Brand = accessoriesFromDb.Brand,
                    Color = accessoriesFromDb.Color,
                    Price = accessoriesFromDb.Price,
                    Description = accessoriesFromDb.Description,
                    Picture = accessoriesFromDb.Picture
                    //FullName = accessoriesFromDb.Owner.FirstName + " " + accessoriesFromDb.Owner.LastName
                }).ToList();

            return this.View(accessory);

        }


    }
}
