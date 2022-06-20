using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAttire.Models;

namespace ShoppingAttire.Models
{
    public class CategoryProduct
    {
        /*[NotMapped]
        [Key]
        public int ID { get; set; }*/
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
