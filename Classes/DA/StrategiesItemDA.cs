using MySql.Data.MySqlClient;
using StrategySync.Classes.Strategy;
using System;

namespace StrategySync.Classes.DA
{
    public class StrategiesItemDA
    {
        private static string connectionString = "Server=strategysync.mysql.database.azure.com; Port=3306; Database=strategysync; Uid=sysadmin; Pwd=Password1!; SslMode=Required;";

        public static void CreateRecord(StrategyItem strategyItem)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(
                    "INSERT INTO strategy_item(strategy_id, role_id, grenade_id, description, x_coordinate, y_coordinate) VALUES(@StrategyId, @RoleId, @GrenadeId, @Description, @XCoordinate, @YCoordinate)", connection))
                {
                    command.Parameters.AddWithValue("@StrategyId", strategyItem.StrategyID);
                    command.Parameters.AddWithValue("@RoleId", strategyItem.RoleID);
                    command.Parameters.AddWithValue("@GrenadeId", strategyItem.GrenadeID);
                    command.Parameters.AddWithValue("@Description", strategyItem.Description);
                    command.Parameters.AddWithValue("@XCoordinate", strategyItem.XCoordinate);
                    command.Parameters.AddWithValue("@YCoordinate", strategyItem.YCoordinate);

                    command.ExecuteNonQuery();
                }
            }
        }


        public static StrategyItem ReadRecord(int itemId)
        {
            StrategyItem strategyItem = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM strategy_item WHERE item_id = @ItemId", connection))
                {
                    command.Parameters.AddWithValue("@ItemId", itemId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            strategyItem = new StrategyItem
                            {
                                ItemID = reader.GetInt32("item_id"),
                                StrategyID = reader.GetInt32("strategy_id"),
                                RoleID = reader.GetInt32("role_id"),
                                GrenadeID = reader.GetInt32("grenade_id"),
                                Description = reader.GetString("description"),
                                XCoordinate = reader.GetFloat("x_coordinate"),
                                YCoordinate = reader.GetFloat("y_coordinate")
                            };
                        }
                    }
                }
            }

            return strategyItem;
        }

        public static void EditRecord(StrategyItem strategyItem)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(
                    "UPDATE strategy_item SET strategy_id = @StrategyId, role_id = @RoleId, grenade_id = @GrenadeId, description = @Description, x_coordinate = @XCoordinate, y_coordinate = @YCoordinate WHERE item_id = @ItemID", connection))
                {
                    command.Parameters.AddWithValue("@StrategyId", strategyItem.StrategyID);
                    command.Parameters.AddWithValue("@RoleId", strategyItem.RoleID);
                    command.Parameters.AddWithValue("@GrenadeId", strategyItem.GrenadeID);
                    command.Parameters.AddWithValue("@Description", strategyItem.Description);
                    command.Parameters.AddWithValue("@XCoordinate", strategyItem.XCoordinate);
                    command.Parameters.AddWithValue("@YCoordinate", strategyItem.YCoordinate);
                    command.Parameters.AddWithValue("@ItemID", strategyItem.ItemID);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
