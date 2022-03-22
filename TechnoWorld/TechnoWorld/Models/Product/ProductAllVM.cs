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

        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Product Picture")]
        public string Picture { get; set; }

        [Display(Name = "Price")]
        public int Price { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Discount")]
        public int Discount { get; set; }
    }
}
