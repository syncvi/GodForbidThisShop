using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ShoppingAttire.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace ShoppingAttire.DAL
{
    public class ProductSqlDB : IProductDB
    {
        public IConfiguration config { get; set; }

        SqlConnection connection { get; set; }

        public ProductSqlDB(IConfiguration insertedConfig)
        {
            this.config = insertedConfig;
            String connectionString = insertedConfig.GetConnectionString("WorkingDB");
            connection = new SqlConnection(connectionString);
        }

        protected string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }
        /*It formats the string as two uppercase hexadecimal characters.
        In more depth, the argument "X2" is a "format string" that tells the ToString() method how it should format the string.
        In this case, "X2" indicates the string should be formatted in Hexadecimal.
        byte.ToString() without any arguments returns the number in its natural decimal representation, with no padding.*/

        /*---------------------------------------------------------------------USERS------------------------------------------------------------------------*/

        public bool ValidateUser(SiteUser user) //needs work
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[User] WHERE [UserName]=@username AND [password]=@password", connection);
            bool validationStatus = false;

            connection.Open();

            SqlParameter atUsername = new SqlParameter("@username", System.Data.SqlDbType.NVarChar, 50)
            {
                Value = user.UserName
            };
            SqlParameter atPassword = new SqlParameter("@password", System.Data.SqlDbType.NVarChar, 150)
            {
                Value = HashPassword(user.Password)
            };
            /*SqlParameter atRole = new SqlParameter("@role", System.Data.SqlDbType.NVarChar, 20)
            {
                Value = user.Role
            };*/
            Console.WriteLine(user.Password);
            Console.WriteLine(HashPassword(user.Password));

            cmd.Parameters.Add(atUsername);
            cmd.Parameters.Add(atPassword);
            //cmd.Parameters.Add(atRole);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) validationStatus = true;


            connection.Close();
            reader.Close();

            return validationStatus;
        }

        public List<SiteUser> UsersDisplay()
        {
            List<SiteUser> userList = new List<SiteUser>();
            SqlCommand query = new("SELECT * FROM [dbo].[User]", connection);

            connection.Open();
            SqlDataReader reader = query.ExecuteReader();

            while (reader.Read())
            {
                int dbId = reader.GetInt32(0);
                string dbUserName = reader.GetString(1);
                string dbPassword = reader.GetString(2);
                string dbRole = reader.GetString(3);

                if (dbRole != "Admin")
                {
                    SiteUser newishUser = new SiteUser { UserId = dbId, UserName = dbUserName, Password = dbPassword, Role = dbRole };
                    userList.Add(newishUser);
                }
                
            }

            reader.Close();
            connection.Close();

            return userList;
        }

        public SiteUser GetUser(SiteUser givenUser)
        {

            SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[User]", connection);

            connection.Open();


            SqlDataReader reader = query.ExecuteReader();

            
            while (reader.Read())
            {
                int dbId = reader.GetInt32(0);
                string dbUserName = reader.GetString(1);
                string dbPassword = reader.GetString(2);
                string dbRole = reader.GetString(3);

                if (dbUserName == givenUser.UserName)
                {
                    givenUser.UserId = dbId;
                    givenUser.Password = dbPassword;
                    givenUser.Role = dbRole;
                }
            }

            reader.Close();
            connection.Close();

            return givenUser;

        }

        //insert user into database
        public int UserInsert(SiteUser givenUser)
        {
            SqlCommand query = new SqlCommand("INSERT INTO [dbo].[User] ([UserName], [Password], [Role]) VALUES (@username, @password, @role)", connection);

            connection.Open();

            SqlParameter atUsername = new SqlParameter("@username", System.Data.SqlDbType.NVarChar, 50)
            {
                Value = givenUser.UserName
            };
            SqlParameter atPassword = new SqlParameter("@password", System.Data.SqlDbType.NVarChar, 150)
            {
                Value = HashPassword(givenUser.Password)
            };
            SqlParameter atRole = new SqlParameter("@role", System.Data.SqlDbType.NVarChar, 20)
            {
                Value = givenUser.Role
            };

            query.Parameters.Add(atUsername);
            query.Parameters.Add(atPassword);
            query.Parameters.Add(atRole);

            int inserted = query.ExecuteNonQuery();

            connection.Close();

            return inserted;

        }
        public int UserUpdate(SiteUser givenUser)
        {
            SqlCommand cmd = new SqlCommand("updateCategory", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            connection.Open();

            SqlParameter atUserId = new SqlParameter("@UserId", System.Data.SqlDbType.Int, 0)
            {
                Value = givenUser.UserId
            };
            SqlParameter atUserName = new SqlParameter("@UserName", System.Data.SqlDbType.NChar, 50)
            {
                Value = givenUser.UserName
            };
            SqlParameter atPassword = new SqlParameter("@Password", System.Data.SqlDbType.NChar, 150)
            {
                Value = givenUser.Password
            };
            SqlParameter atRole = new SqlParameter("@Role", SqlDbType.VarChar, 20)
            {
                Value = givenUser.Role
            };

            cmd.Parameters.Add(atRole);
            cmd.Parameters.Add(atUserId);
            cmd.Parameters.Add(atUserName);
            cmd.Parameters.Add(atPassword);

            cmd.Prepare();
            int updated = cmd.ExecuteNonQuery();
            connection.Close();

            return updated;
        }        

        public int UserDelete(int givenId)
        {
            SqlCommand query = new("deleteUser", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            connection.Open();

            SqlParameter atId = new SqlParameter("@Id", SqlDbType.Int)
            {
                Value = givenId
            };
            query.Parameters.Add(atId);


            int deleted = query.ExecuteNonQuery();
            connection.Close();

            return deleted;
        }

        /*---------------------------------------------------------------------ROLES------------------------------------------------------------------------*/

        public List<Role> RolesDisplay()
        {
            List<Role> roleList = new List<Role>();
            SqlCommand query = new("SELECT * FROM Role", connection);

            connection.Open();
            SqlDataReader reader = query.ExecuteReader();

            while (reader.Read())
            {
                int dbId = reader.GetInt32(0);
                string dbName = reader.GetString(1);

                Role newishRole = new Role { Id = dbId, RoleName = dbName};
                roleList.Add(newishRole);
            }

            reader.Close();
            connection.Close();

            return roleList;
        }
        
        public Role GetRole(int givenId)
        {
            Role newishRole = new();
            SqlCommand query = new("SELECT * FROM Role WHERE Id=" + givenId, connection);

            connection.Open();

            SqlDataReader reader = query.ExecuteReader();

            if (reader.Read())
            {
                int dbId = reader.GetInt32(0);
                string dbName = reader.GetString(1);

                newishRole = new Role { Id = dbId, RoleName = dbName };
            }

            reader.Close();
            connection.Close();

            return newishRole;

        }
        /*---------------------------------------------------------------------PRODUCER------------------------------------------------------------------------*/
        public List<Producer> ProducerDisplay()
        {
            List<Producer> producerList = new List<Producer>();
            SqlCommand query = new("SELECT * FROM Producer", connection);

            connection.Open();
            SqlDataReader reader = query.ExecuteReader();

            while (reader.Read())
            {
                int dbId = reader.GetInt32(0);
                string dbName = reader.GetString(1);

                Producer newishProducer = new Producer { Id = dbId, ProducerName = dbName };
                producerList.Add(newishProducer);
            }

            reader.Close();
            connection.Close();

            return producerList;
        }

        public Producer ProducerGetDetails(int givenId)
        {
            Producer newishProducer = new();
            SqlCommand query = new("SELECT * FROM Producer WHERE Id=" + givenId, connection);

            connection.Open();

            SqlDataReader reader = query.ExecuteReader();

            if (reader.Read())
            {
                int dbId = reader.GetInt32(0);
                string dbName = reader.GetString(1);

                newishProducer = new Producer { Id = dbId, ProducerName = dbName };
            }

            reader.Close();
            connection.Close();

            return newishProducer;

        }

        /*---------------------------------------------------------------------PRODUCTS------------------------------------------------------------------------*/

        public List<Product> ProductsDisplay()
        {
            List<Product> productList = new List<Product>();
            SqlCommand query = new("SELECT * FROM Product", connection);

            connection.Open();
            SqlDataReader reader = query.ExecuteReader();

            while (reader.Read())
            {
                int dbId = reader.GetInt32(0);
                string dbName = reader.GetString(1);
                decimal dbPrice = reader.GetDecimal(2);
                string dbDesc = reader.GetString(3);
                int dbProdId = reader.GetInt32(4);

                Product product = new Product { Id = dbId, Name = dbName, Price = dbPrice, Description = dbDesc, ProducerId = dbProdId };
                productList.Add(product);
            }

            reader.Close();
            connection.Close();

            return productList;
        }

        public Product ProductGetDetails(int givenId)
        {
            Product product = new();
            SqlCommand query = new("SELECT * FROM Product WHERE Id=" + givenId, connection);

            connection.Open();

            SqlDataReader reader = query.ExecuteReader();

            if(reader.Read())
            {
                int dbId = reader.GetInt32(0);
                string dbName = reader.GetString(1);
                decimal dbPrice = reader.GetDecimal(2);
                string dbDesc = reader.GetString(3);
                int dbProdId = reader.GetInt32(4);

                product = new Product { Id = dbId, Name = dbName, Price = dbPrice, Description = dbDesc, ProducerId = dbProdId };
            }

            reader.Close();
            connection.Close();

            return product;

        }

        public bool ProductHasCategory(int productId, int categoryId)
        {
            bool hasCategory = false;
            SqlCommand query = new("SELECT * FROM CategoryProduct WHERE ProductId=" + productId + " AND CategoryId=" + categoryId, connection);

            connection.Open();

            
            SqlParameter atProdId = new SqlParameter("@ProductId", System.Data.SqlDbType.Int, 0)
            {
                Value = productId
            };

            SqlParameter atCatId = new SqlParameter("@CategoryId", System.Data.SqlDbType.Int, 0)
            {
                Value = categoryId
            };
            
            query.Parameters.Add(atProdId);
            query.Parameters.Add(atCatId);

            SqlDataReader reader = query.ExecuteReader();
            if (reader.Read())
            {
                hasCategory = true;
            }

            
            connection.Close();
            reader.Close();
            return hasCategory;
        }
        
        
        public int GetLastProductId()
        {
            int lastId = 0;
            SqlCommand query = new("SELECT TOP 1 Id FROM Product ORDER BY Id DESC", connection);

            connection.Open();

            SqlDataReader reader = query.ExecuteReader();

            if (reader.Read())
            {
                lastId = reader.GetInt32(0);
            }

            reader.Close();
            connection.Close();

            return lastId;
        }

        public int ProductInsert(Product givenProduct)
        {

            SqlCommand query = new("insertProduct", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();

            SqlParameter atName = new SqlParameter("@Name", SqlDbType.VarChar, 50)
            {
                Value = givenProduct.Name
            };
            
            SqlParameter atPrice = new SqlParameter("@Price", SqlDbType.Money, 0)
            {
                Value = givenProduct.Price
            };
            
            SqlParameter atDesc = new SqlParameter("@Description", SqlDbType.VarChar, 200)
            {
                Value = givenProduct.Description
            };
            SqlParameter atProdId = new SqlParameter("@ProducerId", SqlDbType.Int, 0)
            {
                Value = givenProduct.ProducerId
            };

            query.Parameters.Add(atName);
            query.Parameters.Add(atPrice);
            query.Parameters.Add(atDesc);
            query.Parameters.Add(atProdId);

            int inserted = query.ExecuteNonQuery();

            connection.Close();

            return inserted;

        }

        public int ProductUpdate(Product givenProduct)
        {
            
            SqlCommand query = new("updateProduct", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();

            SqlParameter atId = new SqlParameter("@Id", SqlDbType.Int)
            {
                Value = givenProduct.Id
            };

            SqlParameter atName = new SqlParameter("@Name", SqlDbType.VarChar, 50)
            {
                Value = givenProduct.Name
            };

            SqlParameter atPrice = new SqlParameter("@Price", SqlDbType.Money)
            {
                Value = givenProduct.Price
            };

            SqlParameter atDesc = new SqlParameter("@Description", SqlDbType.VarChar, 200)
            {
                Value = givenProduct.Description
            };
            SqlParameter atProdId = new SqlParameter("@ProducerId", SqlDbType.Int, 0)
            {
                Value = givenProduct.ProducerId
            };

            query.Parameters.Add(atId);
            query.Parameters.Add(atName);
            query.Parameters.Add(atPrice);
            query.Parameters.Add(atDesc);
            query.Parameters.Add(atProdId);

            int updated = query.ExecuteNonQuery();
            connection.Close();

            return updated;
        }

        public int ProductDelete(int givenId)
        {
            SqlCommand query = new("deleteProduct", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            connection.Open();

            SqlParameter atId = new SqlParameter("@Id", SqlDbType.Int)
            {
                Value = givenId
            };
            query.Parameters.Add(atId);

            
            int deleted = query.ExecuteNonQuery();
            connection.Close();

            return deleted;
        }

        /*---------------------------------------------------------------------CATEGORIES------------------------------------------------------------------------*/
        public List<Category> CategoriesDisplay()
        {
            List<Category> categoryList = new List<Category>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Category", connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int cid = reader.GetInt32(0);
                string shortName = reader.GetString(1);
                string longName = reader.GetString(2);

                Category category = new Category { Id = cid, ShortName = shortName, LongName = longName };
                categoryList.Add(category);
            }
            connection.Close();
            reader.Close();

            return categoryList;
        }

        public Category CategoryGetDetails(int givenId)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Category WHERE id=@id", connection);
            connection.Open();

            SqlParameter atId = new SqlParameter("@Id", System.Data.SqlDbType.Int, 0)
            {
                Value = givenId
            };
            cmd.Parameters.Add(atId);
            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;


            int dbCid = reader.GetInt32(0);
            string dbShortName = reader.GetString(1);
            string dbLongName = reader.GetString(2);
            string dbDesc = reader.GetString(3);

            connection.Close();
            reader.Close();

            return new Category { Id = dbCid, ShortName = dbShortName, LongName = dbLongName, Description = dbDesc };
        }

        public int CategoryInsert(Category givenCategory)
        {
            SqlCommand cmd = new SqlCommand("insertCategory", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            connection.Open();

            SqlParameter atShorty = new SqlParameter("@ShortName", System.Data.SqlDbType.NChar, 50)
            {
                Value = givenCategory.ShortName
            };
            SqlParameter atFully = new SqlParameter("@LongName", System.Data.SqlDbType.NChar, 100)
            {
                Value = givenCategory.LongName
            };
            SqlParameter atDesc = new SqlParameter("@Description", SqlDbType.VarChar, 200)
            {
                Value = givenCategory.Description
            };
            cmd.Parameters.Add(atDesc);
            cmd.Parameters.Add(atShorty);
            cmd.Parameters.Add(atFully);

            cmd.Prepare();
            int inserted = cmd.ExecuteNonQuery();
            connection.Close();

            return inserted;
        }

        public int CategoryDelete(int givenId)
        {
            SqlCommand cmd = new SqlCommand("deleteCategory", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            connection.Open();

            SqlParameter atId = new SqlParameter("@Id", System.Data.SqlDbType.Int, 0)
            {
                Value = givenId
            };
            cmd.Parameters.Add(atId);

            cmd.Prepare();
            int deleted = cmd.ExecuteNonQuery();
            connection.Close();

            return deleted;
        }

        public int CategoryUpdate(Category givenCategory)
        {
            SqlCommand cmd = new SqlCommand("updateCategory", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            connection.Open();

            SqlParameter atId = new SqlParameter("@Id", System.Data.SqlDbType.Int, 0)
            {
                Value = givenCategory.Id
            };
            SqlParameter atShorty = new SqlParameter("@ShortName", System.Data.SqlDbType.NChar, 50)
            {
                Value = givenCategory.ShortName
            };
            SqlParameter atFully = new SqlParameter("@LongName", System.Data.SqlDbType.NChar, 100)
            {
                Value = givenCategory.LongName
            };
            SqlParameter atDesc = new SqlParameter("@Description", SqlDbType.VarChar, 200)
            {
                Value = givenCategory.Description
            };

            cmd.Parameters.Add(atDesc);
            cmd.Parameters.Add(atId);
            cmd.Parameters.Add(atShorty);
            cmd.Parameters.Add(atFully);

            cmd.Prepare();
            int updated = cmd.ExecuteNonQuery();
            connection.Close();

            return updated;
        }

        /*---------------------------------------------------------------------LINKS------------------------------------------------------------------------*/
        public int EstablishLink(int productId, int categoryId)
        {
            try 
            {
                
                SqlCommand cmd = new SqlCommand("establishLink", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                connection.Open();

                SqlParameter atProdId = new SqlParameter("@ProductId", System.Data.SqlDbType.Int, 0)
                {
                    Value = productId
                };

                SqlParameter atCatId = new SqlParameter("@CategoryId", System.Data.SqlDbType.Int, 0)
                {
                    Value = categoryId
                };

                cmd.Parameters.Add(atProdId);
                cmd.Parameters.Add(atCatId);

                cmd.Prepare();
                int established = cmd.ExecuteNonQuery();
                cmd.Dispose();
                connection.Close();

                return established;
            }
            catch (Exception ex)
            {
                ;
            }

            return 0;
        }

        public int PurgeLink(int productId, int categoryId)
        {
            SqlCommand cmd = new SqlCommand("purgeLink", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            connection.Open();

            SqlParameter atProdId = new SqlParameter("@ProductId", System.Data.SqlDbType.Int, 0)
            {
                Value = productId
            };

            SqlParameter atCatId = new SqlParameter("@CategoryId", System.Data.SqlDbType.Int, 0)
            {
                Value = categoryId
            };

            cmd.Parameters.Add(atProdId);
            cmd.Parameters.Add(atCatId);

            cmd.Prepare();
            int purged = cmd.ExecuteNonQuery();
            connection.Close();

            return purged;

        }

        public int WipeProductLinks(int productId)
        {
            SqlCommand cmd = new SqlCommand("wipeProductLinks", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            connection.Open();

            SqlParameter atProdId = new SqlParameter("@ProductId", System.Data.SqlDbType.Int, 0)
            {
                Value = productId
            };

            cmd.Parameters.Add(atProdId);

            cmd.Prepare();
            int wiped = cmd.ExecuteNonQuery();
            connection.Close();

            return wiped;
        }

    }

}
