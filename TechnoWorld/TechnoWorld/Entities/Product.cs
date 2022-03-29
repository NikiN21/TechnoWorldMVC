using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoWorld.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Required]
        
        public string Model { get; set; }
        [Required]
        
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        [Required]
        public string Description { get; set; }
       
        public string ImageId { get; set; }
        public virtual Image Image { get; set; }

        [Required]
        
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }


    }
}







