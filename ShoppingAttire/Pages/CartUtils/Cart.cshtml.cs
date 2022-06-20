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
    public class CartModel : Akimbo
    {
        public CartModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        public List<Product> cartProducts { get; set; }

        public void OnGet()
        {
            
            CartCookieLoad();
            cartProducts = cart.CartDisplayList();
            CartCookieSave();
        }
    }
}
