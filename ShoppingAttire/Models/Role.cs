using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAttire.Models
{
    public class Role
    {
        private const string V = "Field is required";
        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = V)]
        public int Id { get; set; }

        [Display(Name = "Role Name")]
        [Required(ErrorMessage = V)]
        public string RoleName { get; set; }
    }
}