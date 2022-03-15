using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoWorld.Data;
using TechnoWorld.Domain;
using TechnoWorld.Models;

namespace TechnoWorld.Controllers
{
    public class PromoController : Controller
    {
        private readonly ApplicationDbContext context;
        public PromoController(ApplicationDbContext context)
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
        public IActionResult Create(PromoCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                Promo promoFromDb = new Promo
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
                context.Promos.Add(promoFromDb);
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
            List<PromoAllViewModel> promos = context.Promos.Select(promoFromDb => new PromoAllViewModel
            {
                Id = promoFromDb.Id,
                Model = promoFromDb.Model,
                Category = promoFromDb.Category,
                Brand = promoFromDb.Brand,
                Description = promoFromDb.Description,
                Picture = promoFromDb.Picture,
                Price = promoFromDb.Price,
                Quantity = promoFromDb.Quantity,
                Discount = promoFromDb.Discount
            }).ToList();
            if (!String.IsNullOrEmpty(searchStringModel))
            {
                promos = promos.Where(x => x.Model.ToLower() == searchStringModel.ToLower())
                    .ToList();
            }
            return this.View(promos);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Promo item = context.Promos.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            PromoCreateViewModel promo = new PromoCreateViewModel()

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
            return View(promo);
        }

        [HttpPost]
        public IActionResult Edit(PromoCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                Promo promo = new Promo
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
                context.Promos.Update(promo);
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
            Promo item = context.Promos.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            PromoCreateViewModel promo = new PromoCreateViewModel()

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
            return View(promo);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Promo item = context.Promos.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            context.Promos.Remove(item);
            context.SaveChanges();
            return this.RedirectToAction("All");


        }


    }
}
