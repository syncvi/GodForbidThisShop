using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAttire.Models;

namespace ShoppingAttire.DAL
{
    public interface IProductDB
    {
        public bool ValidateUser(SiteUser user);
        public List<SiteUser> UsersDisplay();
        public SiteUser GetUser(SiteUser user);
        public int UserInsert(SiteUser user);
        public int UserDelete(int givenId);

        public List<Producer> ProducerDisplay();
        public Producer ProducerGetDetails(int givenId);



        public List<Role> RolesDisplay();
        public Role GetRole(int givenId);


        public int GetLastProductId();
        public bool ProductHasCategory(int productId, int categoryId);
        public List<Product> ProductsDisplay();
        public Product ProductGetDetails(int givenId);
        public int ProductInsert(Product givenProduct);
        public int ProductUpdate(Product givenProduct);
        public int ProductDelete(int givenId);
        

        public List<Category> CategoriesDisplay();
        public Category CategoryGetDetails(int givenId);
        public int CategoryInsert(Category givenCategory);
        public int CategoryUpdate(Category givenCategory);
        public int CategoryDelete(int givenId);

        
        public int EstablishLink(int productId, int categoryId);
        public int PurgeLink(int productId, int categoryId);
        public int WipeProductLinks(int productId);
    }
}
