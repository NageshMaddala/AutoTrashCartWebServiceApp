using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AutoTrashCartWebServiceApp.Models
{
    public class AutoTrashCartApiError
    {
        public string Message { get; set; }
        public string RequestMethod { get; set; }
        public string RequestUri { get; set; }
        public DateTime TimeUtc { get; set; }
    }

    public class SqlErrorLogging
    {
        public void InsertErrorLog(AutoTrashCartApiError apiError)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    sqlConnection.Open();
                    var cmd =
                        new SqlCommand("AutoTrashCartAPI_ErrorLogging", connection: sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                    cmd.Parameters.AddWithValue("@TimeUtc", apiError.TimeUtc);
                    cmd.Parameters.AddWithValue("@RequestUri", apiError.RequestUri);
                    cmd.Parameters.AddWithValue("@Message", apiError.Message);
                    cmd.Parameters.AddWithValue("@RequestMethod", apiError.RequestMethod);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}