using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAttire.Models
{
    public class Product
    {
        private const string V = "Field is required";

        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name ="Name")]
        [Required(ErrorMessage = V)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = V)]
        [Range(0.01, Double.MaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = V)]
        [MaxLength(200)]
        public string Description { get; set; }

        [Display(Name = "ProducerID")]
        [Required(ErrorMessage = V)]
        public int ProducerId { get; set; }

        public Producer Producer { get; set; }

        [Display(Name = "Categories")]
        public ICollection<CategoryProduct> Categories { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!(obj is Product)) return false;

            Product p = (Product)obj;
            return p.Id.Equals(Id);
        }

    }
}
