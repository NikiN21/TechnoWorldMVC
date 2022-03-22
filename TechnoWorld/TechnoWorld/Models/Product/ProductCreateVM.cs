using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechnoWorld.Models.Category;

namespace TechnoWorld.Models.Product
{
    public class ProductCreateVM
    {
        public ProductCreateVM()
        {
            Categories = new List<CategoryChoiceVM>();
        }
        [Key]

        public int Id { get; set; }

       
        [Display(Name ="Category")]
        
        public int CategoryId { get; set; }
        
        public string Brand { get; set; }
        
        public string Model { get; set; }
  
        public string Description { get; set; }
  
        public string Picture { get; set; }
       
        public int Price { get; set; }
       
        public int Quantity { get; set; }
        
        public int Discount { get; set; }
        public virtual List<CategoryChoiceVM> Categories { get; set; }

    }
}
