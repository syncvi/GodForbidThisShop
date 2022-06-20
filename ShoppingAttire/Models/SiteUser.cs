using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingAttire.Models
{
    public class SiteUser
    {
        private const string V = "Field is required";
        [Display(Name = "ID")]
        
        public int UserId { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = V)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = V)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{4,15}$", ErrorMessage = "Password must be constructed of uppercase and lowercase letters + numbers")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Role")]
        [DefaultValue("Customer")]
        public string Role { get; set; }

        

        [NotMapped]
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}
