using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TechnoWorld.Data;
using TechnoWorld.Entities;
using TechnoWorld.Models.Order;

namespace TechnoWorld.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext context;

        public OrderController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpPost]

        public IActionResult Create(OrderCreateBindingModel bindingModel)
        {
            if (this.ModelState.IsValid)
            {
                string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = this.context.Users.SingleOrDefault(u => u.Id == currentUserId);
                var ev = this.context.Products.SingleOrDefault(e => e.Id == bindingModel.ProductId);
                if (user == null || ev == null || ev.Quantity < bindingModel.ProductCount)
                {

                    return this.RedirectToAction("All", "Products");
                }
                Order orderForDb = new Order
                {
                    OrderedOn = DateTime.UtcNow,
                    ProductId = bindingModel.ProductId,
                    ProductCount = bindingModel.ProductCount,
                    ProductUserId =currentUserId,
                   
                   
                };
                ev.Quantity -= bindingModel.ProductCount;
                this.context.Products.Update(ev);
                this.context.Orders.Add(orderForDb);
                this.context.SaveChanges();
            }
            return this.RedirectToAction("All", "Products");
        }
        
    }
}

