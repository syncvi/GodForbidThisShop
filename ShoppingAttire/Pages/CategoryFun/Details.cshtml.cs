using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingAttire.DAL;
using ShoppingAttire.Models;

namespace ShoppingAttire.Pages.CategoryFun
{
    public class DetailsModel : Akimbo
    {
        public DetailsModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        [FromQuery(Name = "givenId")]
        public int givenId { get; set; }

        [BindProperty]
        public Category category { get; set; }

        public String DisplayAssignedProducts()
        {
            String result = "";
            //List<Category> chosenCategories = new();
            List<Product> productList = DataBase.ProductsDisplay();
            foreach (Product product in productList)
            {
                if (DataBase.ProductHasCategory(product.Id, category.Id))
                {
                    result += product.Name + ", ";
                }
            }
            return result;
        }

        public IActionResult OnGet()
        {
            category = DataBase.CategoryGetDetails(givenId);

            if (category == null) return RedirectToPage("List");

            return Page();
        }
    }
}
