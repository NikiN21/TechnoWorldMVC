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
        [Key]
        public int Id { get; set; }
        
        public string Model { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public string Description { get; set; }
       
        public string ImageId { get; set; }
        public virtual Image Image { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}







