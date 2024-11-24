using MySql.Data.MySqlClient;
using System;

namespace StrategySync.Classes.DA
{
    public class StrategiesDA
    {
        private static string connectionString = "Server=strategysync.mysql.database.azure.com; Port=3306; Database=strategysync; Uid=sysadmin; Pwd=Password1!; SslMode=Required;";

        public static void CreateRecord(Strategy.Strategy strategy)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(
                    "INSERT INTO strategies(map_id, strategy_name, description, last_opened, is_checked, user_ids) VALUES(@MapId, @StrategyName, @Description, @LastOpened, @IsChecked, @UserIds)", connection))
                {
                    command.Parameters.AddWithValue("@MapId", strategy.MapID);
                    command.Parameters.AddWithValue("@StrategyName", strategy.Name);
                    command.Parameters.AddWithValue("@Description", strategy.Description);
                    command.Parameters.AddWithValue("@LastOpened", strategy.LastOpened);
                    command.Parameters.AddWithValue("@IsChecked", strategy.IsCheckedOut);
                    command.Parameters.AddWithValue("@UserIds", strategy.UserIds);

                    command.ExecuteNonQuery();
                }
            }
        }

         
        public static Strategy.Strategy ReadRecord(string strategyName)
        {
            Strategy.Strategy strategyInfo = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM strategies WHERE strategy_name = @StrategyName", connection))
                {
                    command.Parameters.AddWithValue("@StrategyName", strategyName);
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
                                UserIds = reader.GetString("user_ids")
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
                    @"UPDATE strategies SET map_id = @MapId, strategy_name = @StrategyName, description = @Description, last_opened = @LastOpened, is_checked = @IsChecked, user_ids = @UserIds WHERE strategy_id = @StrategyID", connection))
                {
                    command.Parameters.AddWithValue("@MapId", strategy.MapID);
                    command.Parameters.AddWithValue("@StrategyName", strategy.Name);
                    command.Parameters.AddWithValue("@Description", strategy.Description);
                    command.Parameters.AddWithValue("@LastOpened", strategy.LastOpened);
                    command.Parameters.AddWithValue("@IsChecked", strategy.IsCheckedOut);
                    command.Parameters.AddWithValue("@UserIds", strategy.UserIds);
                    command.Parameters.AddWithValue("@StrategyID", strategy.StrategyID);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
