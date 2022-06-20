using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using ShoppingAttire.Models;
using ShoppingAttire.DAL;
using Microsoft.Extensions.Configuration;

namespace ShoppingAttire.Pages.Login
{
    public class UserRegistrationModel : Akimbo
    {
        public UserRegistrationModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        [BindProperty]
        public SiteUser regUser { get; set; }

        
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            
            regUser.Role = "Customer";
            DataBase.UserInsert(regUser);

            return RedirectToPage("/MainFun/List");
        }
    }
}
