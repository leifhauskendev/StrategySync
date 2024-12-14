using MySql.Data.MySqlClient;
using StrategySync.BL;
using StrategySync.Classes.Strategy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

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
                    "INSERT INTO strategies_item(strategy_id, role_id, grenade_id, description, x_coordinate, y_coordinate, link, mediaImage) VALUES(@StrategyId, @RoleId, @GrenadeId, @Description, @XCoordinate, @YCoordinate, @Link, @MediaImage); SELECT LAST_INSERT_ID();", connection))
                {
                    command.Parameters.AddWithValue("@StrategyId", strategyItem.StrategyID);
                    command.Parameters.AddWithValue("@RoleId", 0);
                    command.Parameters.AddWithValue("@GrenadeId", strategyItem.ItemType);
                    command.Parameters.AddWithValue("@Description", strategyItem.Description);
                    command.Parameters.AddWithValue("@XCoordinate", strategyItem.XCoordinate);
                    command.Parameters.AddWithValue("@YCoordinate", strategyItem.YCoordinate);
                    command.Parameters.AddWithValue("@Link", strategyItem.Link ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@MediaImage", strategyItem.MediaImage ?? (object)DBNull.Value);

                    var returnedId = Convert.ToInt32(command.ExecuteScalar());

                    LoggingDA.WriteLog("Create", "CreateStrategyItem", (Application.Current as App).User, returnedId);
                }
            }
        }


        public static StrategyItem ReadRecord(int itemId)
        {
            StrategyItem strategyItem = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM strategies_item WHERE item_id = @ItemId", connection))
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
                                ItemType = reader.GetInt32("grenade_id"),
                                Description = reader.GetString("description"),
                                XCoordinate = reader.GetFloat("x_coordinate"),
                                YCoordinate = reader.GetFloat("y_coordinate"),
                                Link = reader.IsDBNull(reader.GetOrdinal("link")) ? null : reader.GetString("link"),
                                MediaImage = reader.IsDBNull(reader.GetOrdinal("mediaImage")) ? null : (byte[])reader["mediaImage"]
                            };
                        }
                    }
                }
            }

            LoggingDA.WriteLog("Read", "ReadStrategyItem", (Application.Current as App).User, itemId);

            return strategyItem;
        }

        public static ObservableCollection<StrategyItem> ReadRecordsByStrategyId(int strategyId)
        {
            ObservableCollection<StrategyItem> strategyItems = new ObservableCollection<StrategyItem>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM strategies_item WHERE strategy_id = @StrategyId", connection))
                {
                    command.Parameters.AddWithValue("@StrategyId", strategyId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StrategyItem strategyItem = new StrategyItem
                            {
                                ItemID = reader.GetInt32("item_id"),
                                StrategyID = reader.GetInt32("strategy_id"),
                                // RoleID = reader.GetInt32("role_id"),
                                ItemType = reader.GetInt32("grenade_id"),
                                Description = reader.GetString("description"),
                                XCoordinate = reader.GetFloat("x_coordinate"),
                                YCoordinate = reader.GetFloat("y_coordinate"),
                                Link = reader.IsDBNull(reader.GetOrdinal("link")) ? null : reader.GetString("link"),
                                MediaImage = reader.IsDBNull(reader.GetOrdinal("mediaImage")) ? null : (byte[])reader["mediaImage"]
                            };
                            strategyItems.Add(strategyItem);
                        }
                    }
                }
            }

            LoggingDA.WriteLog("Read", "ReadStrategyItemsByStrategyId", (Application.Current as App).User, strategyId);

            return strategyItems;
        }


        public static void EditRecord(StrategyItem strategyItem)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(
                    "UPDATE strategies_item SET strategy_id = @StrategyId, role_id = @RoleId, grenade_id = @GrenadeId, description = @Description, x_coordinate = @XCoordinate, y_coordinate = @YCoordinate, link = @Link, mediaImage = @MediaImage WHERE item_id = @ItemID", connection))
                {
                    command.Parameters.AddWithValue("@StrategyId", strategyItem.StrategyID);
                    command.Parameters.AddWithValue("@RoleId", 0);
                    command.Parameters.AddWithValue("@GrenadeId", strategyItem.ItemType);
                    command.Parameters.AddWithValue("@Description", strategyItem.Description);
                    command.Parameters.AddWithValue("@XCoordinate", strategyItem.XCoordinate);
                    command.Parameters.AddWithValue("@YCoordinate", strategyItem.YCoordinate);
                    command.Parameters.AddWithValue("@Link", strategyItem.Link ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@MediaImage", strategyItem.MediaImage ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ItemID", strategyItem.ItemID);

                    command.ExecuteNonQuery();
                }
            }

            LoggingDA.WriteLog("Update", "UpdateStrategyItem", (Application.Current as App).User, strategyItem.ItemID);
        }

        public static void DeleteRecordById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(
                    "DELETE FROM strategies_item WHERE item_id = @ItemId", connection))
                {
                    command.Parameters.AddWithValue("@ItemId", id);
                    command.ExecuteNonQuery();
                }
            }

            LoggingDA.WriteLog("Delete", "DeleteStrategyItem", (Application.Current as App).User, id);
        }

    }
}
