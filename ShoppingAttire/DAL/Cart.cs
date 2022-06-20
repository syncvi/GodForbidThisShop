using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using ShoppingAttire.Models;

namespace ShoppingAttire.DAL
{
    public class Cart
    {
        public List<Product> productList;
        private IProductDB DataBase;
        public Cart(IProductDB insertedDataBase)
        { 
            DataBase = insertedDataBase;
            productList = new List<Product>();
        }

        public List<Product> CartDisplayList() { return productList; }

        public string CartStateSave()
        {
            return JsonSerializer.Serialize(productList);
        }

        public void CartRemove(Product givenProduct)
        {
            productList.Remove(givenProduct);
        }

        public void CartStateLoad(string jsonProducts)
        {
            if (jsonProducts == null) return;
            else
            {
                productList = JsonSerializer.Deserialize<List<Product>>(jsonProducts);
                List<Product> bigList = DataBase.ProductsDisplay();
                for (int i = 0; i < productList.Count; i++)
                {
                    Product productOnIndex = productList[i];
                    if (!bigList.Contains(productOnIndex)) CartRemove(productOnIndex);
                }
            }
        }

        public void CartAdd(Product givenProduct)
        {
            productList.Add(DataBase.ProductGetDetails(givenProduct.Id));
        }

        public void CartClear()
        {
            productList = new List<Product>();
        }
    }
}
