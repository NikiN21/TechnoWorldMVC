using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoWorld.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderedOn { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string ProductUserId { get; set; }
        public virtual ProductUser ProductUser { get; set; }
    }
}
