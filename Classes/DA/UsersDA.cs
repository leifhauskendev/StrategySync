using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace StrategySync.Classes.DA
{
    public class UserDA
    {
        private static string connectionString = "Server=strategysync.mysql.database.azure.com; Port=3306; Database=strategysync; Uid=sysadmin@strategysync; Pwd=Password1!; SslMode=Required;";

        public static void CreateRecord(User user, string hashedPassword)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("INSERT INTO Users (User, Password, Salt) VALUES (@user, @password, @salt)", connection))
                {
                    command.Parameters.AddWithValue("@user", user.Username);
                    command.Parameters.AddWithValue("@password", hashedPassword);
                    command.Parameters.AddWithValue("@salt", user.Salt);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static User ReadRecord(string username)
        {
            User userInfo = new User();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM Users WHERE User=@user", connection))
                {
                    command.Parameters.AddWithValue("@user", username);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userInfo.Username = reader.GetString("User");
                            userInfo.Salt = (byte[])reader.GetValue(reader.GetOrdinal("Salt"));
                        }
                    }
                }
            }

            return userInfo;
        }
    }
}