﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoWorld.Domain
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
        public string Brand { get; set; }
        //public virtual Brand Brand { get; set; }
        [Required]
        public string Description { get; set; }
        public string Picture { get; set; }
        [Required]
        [Range(50, 4000)]
        public int Price { get; set; }

        public int Quantity { get; set; }
        public int Discount { get; set; }

    }
}







