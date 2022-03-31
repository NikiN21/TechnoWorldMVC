using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoWorld.Models.Order
{
    public class OrderListingViewModel
    {
        public int Id { get; set; }
        public string OrderedOn { get; set; }
        public int ProductId { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string ProductUserId { get; set; }
        public int ProductCount { get; set; }
 
    }
}
