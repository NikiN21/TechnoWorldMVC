using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechnoWorld.Models.Brand;

namespace TechnoWorld.Models.Product
{
    public class ProductCreateVM
    {
        public ProductCreateVM()
        {
            Categories = new List<CategoryChoiceVM>();
            Brands = new List<BrandChoiceVM>();
        }
        [Key]

        public int Id { get; set; }

        public string Model { get; set; }

        [Display(Name ="Category")]  
        public int CategoryId { get; set; }
        public virtual List<CategoryChoiceVM> Categories { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public virtual List<BrandChoiceVM> Brands { get; set; }

        
  
        public string Description { get; set; }
  
        public IFormFile Image { get; set; }
       
        public decimal Price { get; set; }
       
        public int Quantity { get; set; }
        
        public decimal Discount { get; set; }
       

    }
}
