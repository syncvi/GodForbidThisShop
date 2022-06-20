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
    public class ShortCutModel : Akimbo
    {
        public ShortCutModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        [FromQuery(Name = "givenId")]
        public int productId { get; set; }

        public IActionResult OnGet()
        {
            CartCookieLoad();
            cart.CartAdd(new Product { Id = productId });
            CartCookieSave();

            return RedirectToPage("/MainFun/List");
        }
    }
}
