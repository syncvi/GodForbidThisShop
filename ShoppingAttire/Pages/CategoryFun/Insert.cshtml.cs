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
    public class InsertModel : Akimbo
    {
        public InsertModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        [BindProperty]
        public Category newishCategory{ get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            DataBase.CategoryInsert(newishCategory);

            return RedirectToPage("./List");
        }
    }
}
