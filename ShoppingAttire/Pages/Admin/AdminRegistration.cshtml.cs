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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ShoppingAttire.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminRegistrationModel : Akimbo
    {
        public AdminRegistrationModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        public SelectList roleSelection { get; set; }
        
        [BindProperty]
        public SiteUser regUser { get; set; }
        
        public Role regRole { get; set; }

        public void OnGet()
        {
            List<Role> roleList = DataBase.RolesDisplay();
            roleSelection= new SelectList(roleList, "Id", "RoleName");
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            
            String chosenRoles = Request.Form["LinkedList"];
            if (chosenRoles == null) return Page();

            String[] chosenIds = chosenRoles.Split(',');
            for (int i = 0; i < chosenIds.Length; i++)
            {
                regRole = DataBase.GetRole(Int32.Parse(chosenIds[i]));
            }
            regUser.Role = regRole.RoleName;

            DataBase.UserInsert(regUser);

            return RedirectToPage("/Admin/List");
        }
    }
}
