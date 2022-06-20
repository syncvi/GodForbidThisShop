using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAttire.Models
{
    public class Producer
    {
        private const string V = "Field is required";
        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = V)]
        public int Id { get; set; }

        [Display(Name = "Producer")]
        [Required(ErrorMessage = V)]
        public string ProducerName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}