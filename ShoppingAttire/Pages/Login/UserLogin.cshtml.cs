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
    public class UserLoginModel : Akimbo
    {
        public UserLoginModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        public string Message { get; set; }

        [BindProperty]
        public SiteUser user { get; set; }
        public SiteUser newishUser { get; set; }

        private bool isValid(SiteUser user) { return DataBase.ValidateUser(user); }
        

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (isValid(user))
            {
                newishUser = DataBase.GetUser(user);
                var claims = new List<Claim>()
                {
                    
                    new Claim(ClaimTypes.Name, newishUser.UserName),
                    new Claim(ClaimTypes.Role, newishUser.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));

                if (returnUrl != null) return Redirect(returnUrl);
                else return RedirectToPage("/MainFun/List");

            }

            Message = "Incorrect username or password.";
            return Page();
        }
    }
}
