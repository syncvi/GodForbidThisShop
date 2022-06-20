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
    public class ListModel : Akimbo
    {
        
        public IList<SiteUser> userList = new List<SiteUser>();

        public ListModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        public void OnGet()
        { 
            userList = DataBase.UsersDisplay();
        }
    }
}
