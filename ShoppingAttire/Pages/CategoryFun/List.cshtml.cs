using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingAttire.DAL;
using ShoppingAttire.Models;

namespace ShoppingAttire.Pages.CategoryFun
{
    public class ListModel : Akimbo
    {
        
        public List<Category> categoryList;

        public ListModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        public void OnGet()
        {
            categoryList = DataBase.CategoriesDisplay();
        }
    }
}
