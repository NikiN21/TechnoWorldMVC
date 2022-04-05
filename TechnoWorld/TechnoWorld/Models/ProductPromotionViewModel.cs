using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoWorld.Models.Products
{
    public class ProductPromotionViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int BrandId { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public decimal Discount { get; set; }
        public string ImageUrl { get; set; }
    }
}
