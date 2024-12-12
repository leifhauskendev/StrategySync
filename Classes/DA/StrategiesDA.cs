using MySql.Data.MySqlClient;
using StrategySync.Classes.Strategy;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace StrategySync.Classes.DA
{
    public class StrategiesDA
    {
        private static string connectionString = "Server=strategysync.mysql.database.azure.com; Port=3306; Database=strategysync; Uid=sysadmin; Pwd=Password1!; SslMode=Required;";

        public static int CreateRecord(Strategy.Strategy strategy)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(
                    "INSERT INTO strategies(map_id, strategy_name, description, last_opened, is_checked, checked_out_to, user_ids, drawing) " +
                    "VALUES(@MapId, @StrategyName, @Description, @LastOpened, @IsChecked, @CheckedOutTo, @UserIds, @Drawing); SELECT LAST_INSERT_ID();", connection))
                {
                    command.Parameters.AddWithValue("@MapId", strategy.MapID);
                    command.Parameters.AddWithValue("@StrategyName", strategy.Name);
                    command.Parameters.AddWithValue("@Description", strategy.Description);
                    command.Parameters.AddWithValue("@LastOpened", strategy.LastOpened);
                    command.Parameters.AddWithValue("@IsChecked", strategy.IsCheckedOut);
                    command.Parameters.AddWithValue("@CheckedOutTo", strategy.CheckedOutTo);
                    command.Parameters.AddWithValue("@UserIds", strategy.UserIds);
                    command.Parameters.AddWithValue("@Drawing", strategy.Drawing);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }


        public static Strategy.Strategy ReadRecord(int strategyID)
        {
            Strategy.Strategy strategyInfo = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM strategies WHERE strategy_id = @StrategyID", connection))
                {
                    command.Parameters.AddWithValue("@StrategyID", strategyID);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            strategyInfo = new Strategy.Strategy
                            {
                                StrategyID = reader.GetInt32("strategy_id"),
                                MapID = reader.GetInt32("map_id"),
                                Name = reader.GetString("strategy_name"),
                                Description = reader.GetString("description"),
                                LastOpened = reader.GetDateTime("last_opened"),
                                IsCheckedOut = reader.GetBoolean("is_checked"),
                                CheckedOutTo = reader.GetString("checked_out_to"),
                                UserIds = reader.GetString("user_ids"),
                                Drawing = reader["drawing"] as byte[]
                            };
                        }
                    }
                }
            }

            return strategyInfo;
        }

        public static void EditRecord(Strategy.Strategy strategy)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(
                    @"UPDATE strategies SET map_id = @MapId, strategy_name = @StrategyName, description = @Description, last_opened = @LastOpened, is_checked = @IsChecked, checked_out_to = @CheckedOutTo, user_ids = @UserIds, drawing = @Drawing WHERE strategy_id = @StrategyID", connection))
                {
                    command.Parameters.AddWithValue("@MapId", strategy.MapID);
                    command.Parameters.AddWithValue("@StrategyName", strategy.Name);
                    command.Parameters.AddWithValue("@Description", strategy.Description);
                    command.Parameters.AddWithValue("@LastOpened", strategy.LastOpened);
                    command.Parameters.AddWithValue("@IsChecked", strategy.IsCheckedOut);
                    command.Parameters.AddWithValue("@CheckedOutTo", strategy.CheckedOutTo);
                    command.Parameters.AddWithValue("@UserIds", strategy.UserIds);
                    command.Parameters.AddWithValue("@Drawing", strategy.Drawing);
                    command.Parameters.AddWithValue("@StrategyID", strategy.StrategyID);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateCheckedOut(bool isCheckedOut, string checkedOutTo, int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(
                    @"UPDATE strategies SET is_checked = @IsChecked, checked_out_to = @CheckedOutTo WHERE strategy_id = @StrategyID", connection))
                {
                    command.Parameters.AddWithValue("@IsChecked", isCheckedOut);
                    command.Parameters.AddWithValue("@CheckedOutTo", checkedOutTo);
                    command.Parameters.AddWithValue("@StrategyID", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static ObservableCollection<StrategyListItem> GetStrategyListItemsByUser(string user)
        {
            var strategyList = new ObservableCollection<StrategyListItem>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM strategies WHERE user_ids LIKE @User", connection))
                {
                    command.Parameters.AddWithValue("@User", '%' + user + '%');
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            strategyList.Add(new StrategyListItem
                            {
                                StrategyID = reader.GetInt32("strategy_id"),
                                MapID = reader.GetInt32("map_id"),
                                Name = reader.GetString("strategy_name"),
                                Description = reader.GetString("description"),
                                LastOpened = reader.GetDateTime("last_opened"),
                                IsCheckedOut = reader.GetBoolean("is_checked"),
                                IsOwner = !(reader.GetString("user_ids").IndexOf(',') == -1) &&
                                            (Application.Current as App).User == reader.GetString("user_ids").Substring(0, reader.GetString("user_ids").IndexOf(',')) ||
                                            reader.GetString("user_ids").IndexOf(',') == -1
                                            ? Visibility.Visible : Visibility.Collapsed
                            });
                        }
                    }
                }
            }

            return strategyList;
        }

        public static bool ShareStrategy(int strategyID, string newUser)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string currentUserIds = null;
                using (MySqlCommand command = new MySqlCommand("SELECT user_ids FROM strategies WHERE strategy_id = @StrategyID", connection))
                {
                    command.Parameters.AddWithValue("@StrategyID", strategyID);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentUserIds = reader["user_ids"]?.ToString();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(currentUserIds) && currentUserIds.Split(',').Contains(newUser))
                {
                    return false;
                }

                string updatedUserIds = string.IsNullOrEmpty(currentUserIds) ? newUser : $"{currentUserIds},{newUser}";

                using (MySqlCommand command = new MySqlCommand("UPDATE strategies SET user_ids = @UserIds WHERE strategy_id = @StrategyID", connection))
                {
                    command.Parameters.AddWithValue("@UserIds", updatedUserIds);
                    command.Parameters.AddWithValue("@StrategyID", strategyID);

                    command.ExecuteNonQuery();
                }
            }
            return true;
        }

        public static bool DeleteRecord(int strategyID)
        {
            bool isDeleted = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("DELETE FROM strategies WHERE strategy_id = @StrategyID", connection))
                {
                    command.Parameters.AddWithValue("@StrategyID", strategyID);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        isDeleted = true;
                    }
                }
            }

            return isDeleted;
        }
    }
}