using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ShoppingAttire.DAL;

namespace ShoppingAttire.Models
{
    public class Akimbo : PageModel
    {
        public IProductDB DataBase { get; set; }
        public ILogger _logger { get; set; }
        public string jsonCartDB { get; set; }
        public Cart cart { get; }

        public double sessionStart { get; set; }

        public double viewTime { get; set; }
       

        public Akimbo(IProductDB insertedDataBase, ILogger logger)
        {   
            DataBase = insertedDataBase;
            _logger = logger;
            cart = new Cart(DataBase);
        }

        public void CartCookieLoad()
        {
            jsonCartDB = HttpContext.Request.Cookies["cart"];

            if (jsonCartDB != null) cart.CartStateLoad(jsonCartDB);
        }

        public void CartCookieSave()
        {
            CookieOptions cookieOption = new CookieOptions();
            cookieOption.Expires = DateTime.Now.AddMinutes(3);

            jsonCartDB = cart.CartStateSave();
            Response.Cookies.Append("cart", jsonCartDB, cookieOption);
        }
        
        public string GetViewTime()
        {
            viewTime = 0;
            string sessionStart = HttpContext.Session.GetString("sessionTime");
            if (sessionStart != null)
            {
                this.sessionStart = double.Parse(sessionStart); //! ev parsing change
                viewTime = DateTime.Now.ToFileTime() - this.sessionStart;
            }
            this.sessionStart = DateTime.Now.ToFileTime();


            HttpContext.Session.SetString("sessionTime", this.sessionStart.ToString());
            HttpContext.Session.SetString("viewTime", viewTime.ToString());


            /*string proszeCie = String.Format("{0} s", TimeSpan.FromMilliseconds(viewTime).TotalSeconds.ToString());
            
            Console.WriteLine(proszeCie);
            


            string mod = proszeCie[0] + "." + proszeCie[1] + " s";
            Console.WriteLine(mod+"\n");

            return mod;*/
            
            var mod = Math.Round(TimeSpan.FromMilliseconds(viewTime).TotalSeconds /100)/100;

            return String.Format("{0} s", mod);
        }

        public static double TimeFormat(double milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds).TotalSeconds;
        }

    }
}
