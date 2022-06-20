using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingAttire.DAL;
using ShoppingAttire.Models;

namespace ShoppingAttire.Pages
{
    public class DetailsModel : Akimbo
    {
        public DetailsModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        [BindProperty]
        public Product Product { get; set; }
        
        public Producer Producer { get; set; }
        

        public string DisplayAssignedCategories()
        {
            string result = "";
            //List<Category> chosenCategories = new();
            List<Category> categoryList = DataBase.CategoriesDisplay();
            foreach (Category category in categoryList)
            {
                if (DataBase.ProductHasCategory(Product.Id, category.Id))
                {
                    result += category.ShortName + ", ";
                }
            }
            return result;
        }



        public void OnGet(int givenId)
        {
            Product = DataBase.ProductGetDetails(givenId);
            Producer = DataBase.ProducerGetDetails(Product.ProducerId);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            return RedirectToPage("./List");
        }
    }
}
