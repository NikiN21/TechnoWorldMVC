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
        public int ProductId { get; set; }
       
        public virtual Product Product { get; set; }
        public string CustomerId { get; set; }
        public virtual ProductUser Customer { get; set; }
        public int ProductCount { get; set; }

    }
}
