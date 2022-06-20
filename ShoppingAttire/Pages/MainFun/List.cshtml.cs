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
    public class ListModel : Akimbo
    {
        
        
        public IList<Product> productList = new List<Product>();

        public ListModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        public void OnGet() {
            productList.Clear();
            productList = DataBase.ProductsDisplay();
        }
    }
}
