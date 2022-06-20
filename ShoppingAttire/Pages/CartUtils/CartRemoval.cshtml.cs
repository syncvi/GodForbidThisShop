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
    public class CartRemovalModel : Akimbo
    {
        public CartRemovalModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        [FromQuery(Name = "givenId")]
        public int givenId { get; set; }

        public IActionResult OnGet()
        {
            CartCookieLoad();
            cart.CartDisplayList().RemoveAt(givenId);
            CartCookieSave();
            return RedirectToPage("Cart");
        }
    }
}
