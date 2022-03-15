using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoWorld.Data;
using TechnoWorld.Models;

namespace TechnoWorld.Controllers
{
    public class ClientController: Controller
    {
        private readonly ApplicationDbContext context;
        public ClientController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult All()
        {
            List<ClientBindingAllViewModel> users = context.Users
                .Select(
                clients => new ClientBindingAllViewModel
                {
                    Id = clients.Id,
                    UserName = clients.UserName,
                    FirstName = clients.FirstName,
                    LastName = clients.LastName,
                    Email = clients.Email,
                    PhoneNumber = clients.PhoneNumber
                }).ToList();
            return View(User);
        }
    }
}
