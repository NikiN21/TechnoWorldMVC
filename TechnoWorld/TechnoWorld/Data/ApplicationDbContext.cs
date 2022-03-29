using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using TechnoWorld.Entities;
using TechnoWorld.Models;
using TechnoWorld.Models.Product;
using TechnoWorld.Models.Brand;

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
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }



        //public DbSet<TechnoWorld.Models.Product.ProductCreateVM> ProductCreateVM { get; set; }

        //        public DbSet<TechnoWorld.Models.Brand.CategoryChoiceVM> CategoryChoiceVM { get; set; }
        //public DbSet<TechnoWorld.Models.Brand.BrandChoiceVM> BrandChoiceVM { get; set; }

        //public DbSet<TechnoWorld.Models.Product.ProductAllVM> ProductAllVM { get; set; }


        //  public DbSet<TechnoWorld.Models.ProductCreateViewModel> ProductCreateViewModel { get; set; }

        //    public DbSet<TechnoWorld.Models.ClientBindingAllViewModel> ClientBindingAllViewModel { get; set; }
        //  public object Category { get; internal set; }

    }
}
