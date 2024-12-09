using StrategySync.Classes.DA;
using StrategySync.Classes.Strategy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StrategySync.BL
{
    public class StrategyBL
    { 
        public static App app = (App)Application.Current;
        public static bool SaveNewStrategy (Strategy strategy)
        {
            try
            {
                strategy.UserIds = app.User;
                strategy.LastOpened = DateTime.Now;
                if (strategy.Description == null)
                {
                    strategy.Description = string.Empty;
                }
                var newId = StrategiesDA.CreateRecord(strategy);

                foreach (StrategyItem item in strategy.StrategyItems)
                {
                    item.StrategyID = newId;
                    if (item.Description == null) 
                    {
                        item.Description = string.Empty;
                    }
                    StrategiesItemDA.CreateRecord(item);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool SaveExistingStrategy (Strategy strategy, ObservableCollection<StrategyItem> deletedItems)
        {
            try
            {
                strategy.LastOpened = DateTime.Now;
                StrategiesDA.EditRecord(strategy);

                foreach (StrategyItem item in strategy.StrategyItems)
                {
                    item.StrategyID = strategy.StrategyID;
                    if (item.Description == null)
                    {
                        item.Description = string.Empty;
                    }
                    if (StrategiesItemDA.ReadRecord(item.ItemID) == null)
                    {
                        StrategiesItemDA.CreateRecord(item);
                    }
                    else
                    {
                        StrategiesItemDA.EditRecord(item);
                    }
                }

                foreach (StrategyItem item in deletedItems)
                {
                    StrategiesItemDA.DeleteRecordById(item.ItemID);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
