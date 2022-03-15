using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TechnoWorld.Domain;
using TechnoWorld.Entities;
using TechnoWorld.Models;

namespace TechnoWorld.Data
{
    public class ApplicationDbContext : IdentityDbContext <ProductUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<Promo> Promos { get; set; }
        public DbSet<TechnoWorld.Models.ProductCreateViewModel> ProductCreateViewModel { get; set; }
        public DbSet<TechnoWorld.Models.AccessoryCreateViewModel> AccessoryCreateViewModel { get; set; }
        public DbSet<TechnoWorld.Models.PromoCreateViewModel> PromoCreateViewModel { get; set; }
        public DbSet<TechnoWorld.Models.ClientBindingAllViewModel> ClientBindingAllViewModel { get; set; }
    }
}
