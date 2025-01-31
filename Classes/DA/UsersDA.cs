﻿using System;
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

            LoggingDA.WriteLog("Create", "CreateUser", user.Username, -1);
        }


        public static User ReadRecord(string username)
        {
            User userInfo = new User();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM Users WHERE username=@username", connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userInfo.Username = reader.GetString("Username");
                            userInfo.Salt = (byte[])reader.GetValue(reader.GetOrdinal("Salt"));
                            userInfo.EncryptedPassword = (byte[])reader.GetValue(reader.GetOrdinal("Password"));
                            userInfo.Salt = (byte[])reader.GetValue(reader.GetOrdinal("Salt"));
                            userInfo.AESKey = (byte[])reader.GetValue(reader.GetOrdinal("AES_Key"));
                            userInfo.AESIV = (byte[])reader.GetValue(reader.GetOrdinal("AES_IV"));
                        }
                    }
                }
            }

            LoggingDA.WriteLog("Read", "ReadUser", username, -1);

            return userInfo;
        }

        public static void UpdatePassword(User user)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("UPDATE users SET password = @password, aes_key = @aesKey, aes_iv = @aesIV WHERE username = @username", connection))
                {
                    command.Parameters.AddWithValue("@username", user.Username);
                    command.Parameters.AddWithValue("@password", user.EncryptedPassword);
                    command.Parameters.AddWithValue("@aesKey", user.AESKey);
                    command.Parameters.AddWithValue("@aesIV", user.AESIV);

                    command.ExecuteNonQuery();
                }
            }

            LoggingDA.WriteLog("Update", "UpdatePassword", user.Username, -1);
        }


        public static string GetUsernameByEmail(string email)
        {
            string username = null;
            string query = "SELECT username FROM users WHERE email=@Email";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open(); 
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            username = reader.GetString("username");
                        }
                    }
                }
            }

            LoggingDA.WriteLog("Read", "GetUsernameByEmail", username, -1);

            return username;
        }
    }
}