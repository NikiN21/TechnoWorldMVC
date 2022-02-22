using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTechnoWorld.Models
{
    public class AccessoryAllViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Picture")]
        public string Picture { get; set; }
    }
}
