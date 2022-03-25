using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoWorld.Models.Product
{
    public class ProductAllVM
    {
        public int Id { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

      
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Image Picture")]
        public string Image { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
    }
}
