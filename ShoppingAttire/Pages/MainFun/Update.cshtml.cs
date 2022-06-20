using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingAttire.DAL;
using ShoppingAttire.Models;

namespace ShoppingAttire.Pages
{
    [Authorize(Roles = "Admin, Sales")]
    public class UpdateModel : Akimbo
    {
        public UpdateModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        [BindProperty]
        public Product Product { get; set; }

        public SelectList categorySelection { get; set; }
        public SelectList producerSelection { get; set; }
        public Producer newishProducer { get; set; }

        public void OnGet(int givenId)
        {
            List<Category> categoryList = DataBase.CategoriesDisplay();
            List<Producer> producerList = DataBase.ProducerDisplay();
            categorySelection = new SelectList(categoryList, "Id", "ShortName");
            producerSelection = new SelectList(producerList, "Id", "ProducerName");
            Product = DataBase.ProductGetDetails(givenId);
            newishProducer = DataBase.ProducerGetDetails(Product.ProducerId);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            
            int updated = Product.Id;
            String chosenCategories = Request.Form["LinkedList"];
            if (chosenCategories == null) return Page();

            DataBase.WipeProductLinks(updated);
            String[] chosenIds = chosenCategories.Split(',');
            for (int i = 0; i < chosenIds.Length; i++)
            {
                DataBase.EstablishLink(updated, Int32.Parse(chosenIds[i]));
            }

            String chosenProducer = Request.Form["Linkage"];
            if (chosenProducer == null) return Page();
            String[] chosenProds = chosenProducer.Split(',');

            for (int i = 0; i < chosenProds.Length; i++)
            {
                newishProducer = DataBase.ProducerGetDetails(Int32.Parse(chosenProds[i]));
            }
            Product.ProducerId = newishProducer.Id;
            
            DataBase.ProductUpdate(Product);
            _logger.CreateLog("UpdateProduct", JsonSerializer.Serialize(Product));
            TempData["Notification"] = "Product updated successfully";
            Console.WriteLine(TempData["Notification"]);
            return RedirectToPage("./List");
        }
    }
}
