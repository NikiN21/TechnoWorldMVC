using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoWorld.Data;
using TechnoWorld.Domain;
using TechnoWorld.Entities;

namespace TechnoWorld.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;


            await RoleSeeder(services);
            await SeedAdministrator(services);

            var data = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories(data);

           // var dataBrand = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //SeedBrands(dataBrand);


            return app;
        }
        private static void SeedCategories(ApplicationDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }
            data.Categories.AddRange(new[]
            {
                new Category {Name="Laptop"},
                new Category {Name="Monitor"},
                new Category {Name="Accessory"},

            });
            data.SaveChanges();
        }
        //private static void SeedBrands(ApplicationDbContext data)
        //{
        //    if (data.Brands.Any())
        //    {
        //        return;
        //    }
        //    data.Brands.AddRange(new[]
        //    {
        //        new Brand {Name="Lenovo"},
        //        new Brand {Name="Samsung"},
        //        new Brand {Name="HP"},
        //        new Brand {Name="DELL"},
        //        new Brand {Name="Acer"},
        //        new Brand {Name="Huawei"},

        //    });
        //    data.SaveChanges();
        //}



        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Administrator", "Client" };

            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }


        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ProductUser>>();

            if (await userManager.FindByNameAsync("admin") == null)
            {
                ProductUser user = new ProductUser();
                user.FirstName = "admin";
                user.LastName = "admin";
                user.PhoneNumber = "123456789";
                user.UserName = "admin";
                user.Email = "admin@admin.com";

                var result = await userManager.CreateAsync
                (user, "123!@#qweQWE");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

    }
}
