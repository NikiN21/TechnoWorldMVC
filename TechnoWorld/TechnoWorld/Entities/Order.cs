using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoWorld.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime OrderedOn { get; set; }
        public string BrandId { get; set; }
        public virtual Brand BrandName { get; set; }
     //   public string BrandName { get; set; }
       // public virtual Brand Brand { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string CustomerId { get; set; }
        public virtual ProductUser Customer { get; set; }
        public int ProductCount { get; set; }
        public string ImageId { get; set; }
        public string ImageUrl { get; set; }
        public virtual Image Image { get; set; }

    }
}
