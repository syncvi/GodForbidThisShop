using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingAttire.DAL;
using ShoppingAttire.Models;

namespace ShoppingAttire.Pages.CartUtils
{
    public class CartCleanerModel : Akimbo
    {
        public CartCleanerModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        public IActionResult OnGet()
        {
            CartCookieLoad();
            cart.CartClear();
            CartCookieSave();

            return RedirectToPage("Cart");
        }
    }
}
