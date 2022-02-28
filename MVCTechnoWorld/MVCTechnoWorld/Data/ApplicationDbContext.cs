using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MVCTechnoWorld.Domain;
using MVCTechnoWorld.Models;

namespace MVCTechnoWorld.Data
{
    public class ApplicationDbContext : IdentityDbContext <ProductsUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<MVCTechnoWorld.Models.ProductCreateViewModel> ProductCreateViewModel { get; set; }
        public DbSet<MVCTechnoWorld.Models.AccessoryCreateViewModel> AccessoryCreateViewModel { get; set; }
    }
}
