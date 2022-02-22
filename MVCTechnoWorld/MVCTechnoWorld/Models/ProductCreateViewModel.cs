using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTechnoWorld.Models
{
    public class ProductCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Type")]
        public string Type { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Brand")]
        public string Brand { get; set; }
        [Required]

        [Display(Name = "Color")]
        public string Color { get; set; }
        [Required]
        [Range(50, 4000, ErrorMessage = "Price must be between 50 and 4000 ")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Picture")]
        public string Picture { get; set; }
    }
}

