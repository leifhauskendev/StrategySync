using StrategySync.Classes.Strategy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using StrategySync.BL;
using System.Windows.Ink;
using System.Collections.ObjectModel;
using StrategySync.Classes.DA;
using System.Windows;
using Microsoft.Win32;

namespace StrategySync
{
    public class TestingMethods
    {

        public static bool CreateStratTest()
        {
            Strategy TestStrat = new Strategy();
            TestStrat.MapID = 1;
            TestStrat.Name = "test";
            TestStrat.Description = "this is a test";
            TestStrat.LastOpened = DateTime.Now;
            TestStrat.IsCheckedOut = false;
            TestStrat.UserIds = "1234";
            TestStrat.Drawing = null;
            TestStrat.StrategyItems = null;
            TestStrat.CheckedOutTo = "1234";

            int newId = StrategiesDA.CreateRecord(TestStrat);
            Strategy ReturnedStrat = StrategiesDA.ReadRecord(newId);

            if (newId != ReturnedStrat.StrategyID)
                return false;
            else
                return true;
        }

        public static bool DeleteStratTest()
        {
            Strategy TestStrat = new Strategy();

            bool result = StrategiesDA.DeleteRecord(TestStrat.StrategyID);

            return result;
        }

        public static bool EditRecordTest()
        {
            Strategy TEST = new Strategy();
            TEST.MapID = 1;
            TEST.Name = "TEST";
            TEST.Description = "this is a test";
            TEST.LastOpened = DateTime.Now;
            TEST.IsCheckedOut = false;
            TEST.UserIds = "trevor";
            TEST.Drawing = null;
            TEST.StrategyItems = null;
            TEST.CheckedOutTo = null;

            Strategy stategyInfo = StrategiesDA.ReadRecord(TEST.StrategyID);
            if (stategyInfo.StrategyID != 1)
                return false;
            else
                return true;
        }

        public static bool ShareRecordTest()
        {
            Strategy TEST = new Strategy();
            
            string newUser = "trevor";

            bool result = StrategiesDA.ShareStrategy(TEST.StrategyID, newUser);
            return result;
        }

        public static bool GetUsernameByEmailTest()
        {
            string email = "trevor.schlechter@trojans.dsu.edu";

            string result = UserDA.GetUsernameByEmail(email);

            Console.WriteLine(result);

            if (result == "trevor")
                return true;
            else
                return false;

        }

    }
}