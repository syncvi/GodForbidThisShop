using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingAttire.DAL;
using ShoppingAttire.Models;
using System.Text.Json;

namespace ShoppingAttire.Pages
{
    [Authorize(Roles = "Admin, Sales")]
    public class InsertModel : Akimbo
    {
        public InsertModel(IProductDB insertedDataBase, ILogger logger) : base(insertedDataBase, logger)
        {
        }

        [BindProperty]
        public Product Product { get; set; }
        public Producer newishProducer { get; set; }

        
        public SelectList categorySelection { get; set; }
        public SelectList producerSelection { get; set; }




        public void OnGet()
        {
            List<Category> categoryList = DataBase.CategoriesDisplay();
            List<Producer> producerList = DataBase.ProducerDisplay();
            categorySelection = new SelectList(categoryList, "Id", "ShortName");
            producerSelection = new SelectList(producerList, "Id", "ProducerName");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            
            String chosenProducer = Request.Form["Linkage"];
            if (chosenProducer == null) return Page();
            String[] chosenProds = chosenProducer.Split(',');

            for (int i = 0; i < chosenProds.Length; i++)
            {
                newishProducer = DataBase.ProducerGetDetails(Int32.Parse(chosenProds[i]));
            }
            Product.ProducerId = newishProducer.Id;


            DataBase.ProductInsert(Product);
            
            
            
            int inserted = DataBase.GetLastProductId();
            String chosenCategories = Request.Form["LinkedList"];
            if (chosenCategories == null) return Page();

            String[] chosenIds = chosenCategories.Split(',');
            for(int i = 0; i<chosenIds.Length; i++)
            {
                DataBase.EstablishLink(inserted, Int32.Parse(chosenIds[i]));
            }

            _logger.CreateLog("InsertProduct", JsonSerializer.Serialize(Product));
            TempData["Notification"] = "Product created successfully";
            Console.WriteLine(TempData["Notification"]);
            return RedirectToPage("./List");
        }
    }
}
