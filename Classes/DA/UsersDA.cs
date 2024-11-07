using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace StrategySync.Classes.DA
{
    public class UserDA
    {
        private static string connectionString = "Server=strategysync.mysql.database.azure.com; Port=3306; Database=strategysync; Uid=sysadmin; Pwd=Password1!; SslMode=Required;";

        public static void CreateRecord(User user)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(
                    "INSERT INTO users (username, password, salt, email, aes_key, aes_iv, users_friends) VALUES (@username, @password, @salt, @Email, @AesKey, @AesIV, @UsersFriends)",
                    connection))
                {
                    command.Parameters.AddWithValue("@username", user.Username);
                    command.Parameters.AddWithValue("@password", user.EncryptedPassword);
                    command.Parameters.AddWithValue("@salt", user.Salt);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@AesKey", user.AESKey);
                    command.Parameters.AddWithValue("@AesIV", user.AESIV);
                    command.Parameters.AddWithValue("@UsersFriends", user.UsersFriends);

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