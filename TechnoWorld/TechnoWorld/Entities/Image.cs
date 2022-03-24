//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Threading.Tasks;

//namespace TechnoWorld.Entities
//{
//    public class Image
//    {
//        public Image()
//        {
//            this.Id = Guid.NewGuid().ToString();
//        }
//        [Key]

//        public string Id { get; set; }
//        [Required]
//        [ForeignKey("Product")]

//        public int ProductId { get; set; }

//        public virtual Product Product { get; set; }

//        public string Extension { get; set; }
//    }
//}
//}
