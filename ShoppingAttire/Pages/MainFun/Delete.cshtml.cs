using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingAttire.DAL;
using ShoppingAttire.Models;

namespace ShoppingAttire.Pages
{
    [Authorize(Roles = "Admin, Sales")]
    public class DeleteModel : Akimbo
    {
        public DeleteModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        Product deletedProduct { get; set; }
        

        public IActionResult OnGet(int givenId)
        {
            deletedProduct = DataBase.ProductGetDetails(givenId);
            DataBase.ProductDelete(givenId);

            _logger.CreateLog("DeleteProduct", JsonSerializer.Serialize(deletedProduct));
            TempData["Notification"] = "Product deleted successfully";
            Console.WriteLine(TempData["Notification"]);
            return RedirectToPage("./List");
        }
    }
}
