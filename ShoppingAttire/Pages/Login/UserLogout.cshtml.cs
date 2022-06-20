using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingAttire.Models;
using ShoppingAttire.DAL;

namespace ShoppingAttire.Pages.Login
{
    public class UserLogoutModel : Akimbo
    {
        public UserLogoutModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync("CookieAuthentication");
            return this.RedirectToPage("/MainFun/List");
        }
    }
}
