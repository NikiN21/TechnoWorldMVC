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
                string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = this.context.Users.SingleOrDefault(u => u.Id == userId);
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
                    CustomerId = userId


                };
                ev.Quantity -= bindingModel.ProductCount;
                this.context.Products.Update(ev);
                this.context.Orders.Add(orderForDb);
                this.context.SaveChanges();
            }
            return this.RedirectToAction("All", "Products");
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = context.Users.SingleOrDefault(u => u.Id == userId);

            List<OrderListingViewModel> orders = context
                 .Orders
                 .Select(x => new OrderListingViewModel
                 {
                     Id = x.Id,
                     ImageId =x.ImageUrl,
                     BrandId = x.BrandId,
                     Brand = x.BrandName.Name,
                     OrderedOn = x.OrderedOn.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
                     CustomerUsername = x.Customer.UserName,
                     ProductCount = x.ProductCount
                 }).ToList();

            return View(orders);
        }
        [Authorize]
        public IActionResult My(string searchString)
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.context.Users.SingleOrDefault(u => u.Id == currentUserId);
            if (user == null)
            {
                return null;
            }

            List<OrderListingViewModel> orders = this.context.Orders
                .Where(x => x.CustomerId == user.Id)
            .Select(x => new OrderListingViewModel
            {
                Id = x.Id,
                BrandId = x.BrandId,
                Brand= x.BrandName.Name,
                ImageUrl = x.ImageId,
                OrderedOn = x.OrderedOn.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
                CustomerId = x.CustomerId,
                CustomerUsername = x.Customer.UserName,
                ProductCount = x.ProductCount
            })
            .ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(o => o.Model.Contains(searchString)).ToList();
            }
            return this.View(orders);
        }

    }
}

