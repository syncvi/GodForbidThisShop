using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAttire.DAL;
using ShoppingAttire.Models;

namespace ShoppingAttire.Pages
{
    public class IndexModel : Akimbo
    {
        public IndexModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
            
        }

        public void OnGet()
        {

        }
    }
}
