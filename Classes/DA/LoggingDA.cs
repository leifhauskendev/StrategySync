using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategySync.Classes.DA
{
    public class LoggingDA
    {
        private static string connectionString = "Server=strategysync.mysql.database.azure.com; Port=3306; Database=strategysync; Uid=sysadmin; Pwd=Password1!; SslMode=Required;";

        public static void WriteLog(string operationType, string function, string user, int associatedId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO logged_items (operation_type, function_name, user, timestamp, associated_id) " +
                           "VALUES (@OperationType, @Function, @User, @Timestamp, @AssociatedId)", connection))
                    {
                        cmd.Parameters.AddWithValue("@OperationType", operationType);
                        cmd.Parameters.AddWithValue("@Function", function);
                        cmd.Parameters.AddWithValue("@User", user);
                        cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                        cmd.Parameters.AddWithValue("@AssociatedId", associatedId);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

}
