using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingAttire.DAL;
using ShoppingAttire.Models;

namespace ShoppingAttire.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class DeleteUserModel : Akimbo
    {
        public DeleteUserModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        [FromQuery(Name = "givenId")]
        public int givenId { get; set; }
        public IActionResult OnGet()
        {
            DataBase.UserDelete(givenId);

            return RedirectToPage("./List");
        }
    }
}
