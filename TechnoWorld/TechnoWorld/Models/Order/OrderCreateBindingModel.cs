using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TechnoWorld.Models.Order
{
    public class OrderCreateBindingModel
    {
        
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Quantity")]
        public int ProductCount { get; set; }
        public string BrandName { get; set; }



    }
}
