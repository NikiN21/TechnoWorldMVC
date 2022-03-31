using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoWorld.Models.Order
{
    public class OrderListingViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string OrderedOn { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Brand")]
        //public string BrandName { get; set; }
        public string Model { get; set; }

        public string CustomerId { get; set; }
        [Display(Name = "Customer")]
        public string CustomerUsername { get; set; }
        public int ProductCount { get; set; }
    }
}
