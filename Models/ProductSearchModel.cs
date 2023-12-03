using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace kit19Task.Models
{
    public class ProductSearchModel
    {
        private SqlDataReader reader;

        public ProductSearchModel()
        {
            // Parameterless constructor
        }

        public string ProductName { get; set; }
        public string Size { get; set; }
        public decimal? Price { get; set; }
        public DateTime? MfgDate { get; set; }
        public string Category { get; set; }
        public int ProductId { get; set; }

        
        
        



    }
}
