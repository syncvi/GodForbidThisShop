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
    public class DeleteModel : Akimbo
    {
        public DeleteModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        [FromQuery(Name = "givenId")]
        public int givenId { get; set; }

        public IActionResult OnGet()
        {
            DataBase.CategoryDelete(givenId);

            return RedirectToPage("./List");
        }
    }
}
