using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAttire.Models;
using ShoppingAttire.DAL;
using System.Text.Json;
using System.IO;

namespace ShoppingAttire.DAL
{
    public class BigLogger : ILogger
    {
        public void CreateLog(string logType, string details)
        {
            string log = LoggMe(logType, details);

            using (StreamWriter writer = new("wwwroot/logs/Logs.txt", append: true))
            {
                writer.WriteLine(log);
            }
        }

        public string ProductToString(Product givenProduct)
        {
            string details = "Id: " + givenProduct.Id + "\n" +
                        "Name: " + givenProduct.Name + "\n" +
                        "Price: " + givenProduct.Price + "\n" +
                        "Description: " + givenProduct.Description + "\n" +
                        "Producer's Id: " + givenProduct.ProducerId;

            return details;
        }

        private string LoggMe(string logType, string log)
        {
            Product loggerProduct = new();
            string productDetails = null;
            if (logType == "UpdateProduct" || logType == "InsertProduct" || logType == "DeleteProduct")
            {
                loggerProduct = JsonSerializer.Deserialize<Product>(log);
                productDetails = ProductToString(loggerProduct);
            }
            
            if (logType == "UpdateProduct")
            {
                log = "------------------------------------------------------\n"
                    + "A product with id=" + loggerProduct.Id + " has been modified." +
                    " Updated product details:\n" + productDetails + "\n"
                    + "------------------------------------------------------\n";
            }
            else if (logType == "InsertProduct")
            {
                log = "------------------------------------------------------\n"
                    + "A new product has been created. Product details:\n" + productDetails + "\n"
                    + "------------------------------------------------------\n";
            }
            else if (logType == "DeleteProduct")
            {
                log = "------------------------------------------------------\n"
                    + "A product with id=" + loggerProduct.Id + " has been deleted. \n"
                    + "------------------------------------------------------\n";
            }
            return log;
        }
        
    }
}
