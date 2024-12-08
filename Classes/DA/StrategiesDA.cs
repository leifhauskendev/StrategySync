using MySql.Data.MySqlClient;
using StrategySync.Classes.Strategy;
using System;
using System.Collections.ObjectModel;

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
                    "INSERT INTO strategies(map_id, strategy_name, description, last_opened, is_checked, user_ids, drawing) " +
                    "VALUES(@MapId, @StrategyName, @Description, @LastOpened, @IsChecked, @UserIds, @Drawing); SELECT LAST_INSERT_ID();", connection))
                {
                    command.Parameters.AddWithValue("@MapId", strategy.MapID);
                    command.Parameters.AddWithValue("@StrategyName", strategy.Name);
                    command.Parameters.AddWithValue("@Description", strategy.Description);
                    command.Parameters.AddWithValue("@LastOpened", strategy.LastOpened);
                    command.Parameters.AddWithValue("@IsChecked", strategy.IsCheckedOut);
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
                    @"UPDATE strategies SET map_id = @MapId, strategy_name = @StrategyName, description = @Description, last_opened = @LastOpened, is_checked = @IsChecked, user_ids = @UserIds, drawing = @Drawing WHERE strategy_id = @StrategyID", connection))
                {
                    command.Parameters.AddWithValue("@MapId", strategy.MapID);
                    command.Parameters.AddWithValue("@StrategyName", strategy.Name);
                    command.Parameters.AddWithValue("@Description", strategy.Description);
                    command.Parameters.AddWithValue("@LastOpened", strategy.LastOpened);
                    command.Parameters.AddWithValue("@IsChecked", strategy.IsCheckedOut);
                    command.Parameters.AddWithValue("@UserIds", strategy.UserIds);
                    command.Parameters.AddWithValue("@Drawing", strategy.Drawing);
                    command.Parameters.AddWithValue("@StrategyID", strategy.StrategyID);

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
                        if (reader.Read())
                        {
                            strategyList.Add(new StrategyListItem
                            {
                                StrategyID = reader.GetInt32("strategy_id"),
                                MapID = reader.GetInt32("map_id"),
                                Name = reader.GetString("strategy_name"),
                                Description = reader.GetString("description"),
                                LastOpened = reader.GetDateTime("last_opened"),
                                IsCheckedOut = reader.GetBoolean("is_checked")
                            });
                        }
                    }
                }
            }

            return strategyList;
        }
    }
}
