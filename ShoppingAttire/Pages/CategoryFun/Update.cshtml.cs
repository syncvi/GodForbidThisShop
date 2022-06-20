using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingAttire.DAL;
using ShoppingAttire.Models;

namespace ShoppingAttire.Pages.CategoryFun
{
    [Authorize(Roles = "Admin, Sales")]
    public class UpdateModel : Akimbo
    {
        public UpdateModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        [FromQuery(Name = "givenId")]
        public int givenId { get; set; }

        [BindProperty]
        public Category category { get; set; }

        public void OnGet()
        {
            category = DataBase.CategoryGetDetails(givenId);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid) DataBase.CategoryUpdate(category);

            return RedirectToPage("./List");
        }
    }
}
