using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoWorld.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime OrderedOn { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("Products")]
        public virtual Product Product { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public string CustomerId { get; set; }
        public virtual ProductUser Customer { get; set; }
        public int Count { get; set; }
        public string ImageId { get; set; }
        public virtual Image Image { get; set; }
        public string ImageUrl { get; set; }
        public string Model { get; set; }
    }
}
