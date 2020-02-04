using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AutoTrashCartWebServiceApp.Models
{
    public class AutoTrashCartApiLogging
    {
        public void InsertLog(AutoTrashCartApiLog apiLog)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    sqlConnection.Open();
                    var cmd =
                        new SqlCommand("AutoTrashCartAPI_Logging", connection: sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                    cmd.Parameters.AddWithValue("@Host", apiLog.Host);
                    cmd.Parameters.AddWithValue("@Headers", apiLog.Headers);
                    cmd.Parameters.AddWithValue("@StatusCode", apiLog.StatusCode);
                    cmd.Parameters.AddWithValue("@RequestBody", apiLog.RequestBody);
                    cmd.Parameters.AddWithValue("@RequestedMethod", apiLog.RequestedMethod);
                    cmd.Parameters.AddWithValue("@UserHostAddress", apiLog.UserHostAddress);
                    cmd.Parameters.AddWithValue("@Useragent", apiLog.Useragent);
                    cmd.Parameters.AddWithValue("@AbsoluteUri", apiLog.AbsoluteUri);
                    cmd.Parameters.AddWithValue("@RequestType", apiLog.RequestType);
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