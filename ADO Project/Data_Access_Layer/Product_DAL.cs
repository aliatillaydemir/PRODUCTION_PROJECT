using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ADO_Project.Models;
using WebGrease.Css.Ast.Selectors;

namespace ADO_Project.Data_Access_Layer
{
    public class Product_DAL //product Data Access Layer(DAL). Its for read data from our Database.
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString(); //its coming from Web.config.
        
        //Get All Products
        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();
            
            using(SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetAllProducts";
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataProducts = new DataTable();

                conn.Open();
                adapter.Fill(dataProducts);
                conn.Close();

                foreach(DataRow dataRow in dataProducts.Rows)  //reading data process... 
                {
                    productList.Add(new Product 
                    {
                        ProductID = Convert.ToInt32(dataRow["ProductID"]),
                        ProductName = dataRow["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dataRow["Price"]),
                        Quantity = Convert.ToInt32(dataRow["Quantity"]),
                        Remarks = dataRow["Remarks"].ToString()
                    });
                }

            }
            return productList;
        }

        
        //Add Data(Product) to Database
        public bool InsertProduct(Product product)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertProducts", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //we need 2 method and database query for update process...  get data by id  - and - update data according to id. 
       
        //Get Products by ProductID
        public List<Product> GetProductByID(int ProductId)
        {
            List<Product> productList = new List<Product>();

            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetProductById";
                cmd.Parameters.AddWithValue("@ProductID", ProductId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataProducts = new DataTable();

                conn.Open();
                adapter.Fill(dataProducts);
                conn.Close();

                foreach (DataRow dataRow in dataProducts.Rows)  //reading data process... 
                {
                    productList.Add(new Product
                    {
                        ProductID = Convert.ToInt32(dataRow["ProductID"]),
                        ProductName = dataRow["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dataRow["Price"]),
                        Quantity = Convert.ToInt32(dataRow["Quantity"]),
                        Remarks = dataRow["Remarks"].ToString()
                    });
                }

            }
            return productList;
        }


        //Update Products 
        public bool UpdateProduct(Product product)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateProducts", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProductID", product.ProductID);  //primary key
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }

            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                // Log the reason for failure if needed
                return false;
            }
        }


        //Delete Product
        public string DeleteProduct(int productId)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("SP_DELETEPRODUCT", connection);
                command.CommandType= CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PRODUCTID", productId);
                command.Parameters.Add("@OUTPUTMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@OUTPUTMESSAGE"].Value.ToString();
                connection.Close();
            }

                return result;
        }


    }
}