using kit19Task.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace kit19Task.DAL
{
    public class ProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ProductSearchModel> SearchProducts(ProductSearchModel productSearchModel)
        {
            List<ProductSearchModel> products = new List<ProductSearchModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SearchProducts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    // Add parameters to the SqlCommand
                    command.Parameters.AddWithValue("@ProductName", productSearchModel.ProductName);
                    command.Parameters.AddWithValue("@Size", productSearchModel.Size);
                    command.Parameters.AddWithValue("@Price",SqlDbType.Decimal).Value= productSearchModel.Price;
                    command.Parameters.Add("@MfgDate", SqlDbType.Date).Value = productSearchModel.MfgDate?.ToString("yyyy-MM-dd");
                    command.Parameters.AddWithValue("@Category", productSearchModel.Category);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        reader.Close();
                        Generate_TXT(command);

                        ProductSearchModel product = new ProductSearchModel();
                        products = ConvertDataTableToList(dataTable);

                    }
                }
            }

            return products;
        }
        public static List<ProductSearchModel> ConvertDataTableToList(DataTable dataTable)
        {
            List<ProductSearchModel> productList = new List<ProductSearchModel>();

            foreach (DataRow row in dataTable.Rows)
            {
                ProductSearchModel product = new ProductSearchModel
                {
                    ProductName = row["ProductName"].ToString(),
                    Size = row["Size"].ToString(),
                    Price = row["Price"] as decimal?,
                    MfgDate = row["MfgDate"] as DateTime?,
                    Category = row["Category"].ToString(),
                    ProductId = Convert.ToInt32(row["ProductId"])
                };

                productList.Add(product);
            }

            return productList;
        }
        public static void Generate_TXT(SqlCommand cmd)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\log.txt";
            StringBuilder Query = new StringBuilder();
            Query.AppendLine("Exec " + cmd.CommandText);
            foreach (SqlParameter p in cmd.Parameters)
            {
                Query = Query.AppendLine(p.ParameterName + " = " + (p.Value == null ? "NULL," : "'" + p.Value.ToString() + "',"));
            }
            Query.Length = Query.Length - 3;
            System.IO.File.WriteAllText(desktop, Query.ToString());
        }
    }
}
