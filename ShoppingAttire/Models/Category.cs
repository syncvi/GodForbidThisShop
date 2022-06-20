using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAttire.Models;

namespace ShoppingAttire.Models
{
    public class Category
    {
        private const string V = "Field is required";

        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Short Name")]
        [Required(ErrorMessage = V)]
        [MaxLength(50)]
        public string ShortName { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = V)]
        [MaxLength(100)]
        public string LongName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = V)]
        [MaxLength(200)]
        public string Description { get; set; }

        public ICollection<CategoryProduct> Products { get; set; }
    }
}
