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
using Org.BouncyCastle.Utilities.IO;
using System.Windows.Input;
using System.Windows.Forms;

namespace StrategySync
{
    public class TestingMethods
    {
        //Strategy Screen Tests
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

        //Login Screen Tests

        public static bool GetUsernameByEmailTest()
        {
            string email = "trevor.schlechter@trojans.dsu.edu";
            string username = "Trevor";

            string result = UserDA.GetUsernameByEmail(email);

            if (string.Equals(result, username))
                return true;
            else
                return false;

        }

        public static bool CreateReadRecordTest()
        {
            User test = new User();
            test.Username = "test";
            test.EncryptedPassword = null;
            test.Salt = null;
            test.Email = "Test@email.com";
            test.AESKey = null;
            test.AESIV = null;
            test.UsersFriends = "Trevor";

            User result = UserDA.ReadRecord(test.Username);
            if (result.Username != test.Username)
                return false;
            else
                return true;
        }

        //Strategy Items Tests

        public static bool CreateItemTest()
        {
            Strategy teststrat = new Strategy();
            teststrat.StrategyID = 25;
            teststrat.StrategyItems = null;

            StrategyItem test = new StrategyItem();
            test.ItemID = 100;
            test.StrategyID = 25;
            test.ItemType = 3;
            test.Description = "test";
            test.XCoordinate = 5;
            test.YCoordinate = 5;

            int newId = StrategiesItemDA.CreateRecord(test);
            StrategyItem result = StrategiesItemDA.ReadRecord(newId);

            if (newId != result.ItemID)
                return false;
            else
                return true;
        }

        public static bool DeleteItemTest()
        {
            StrategyItem test = new StrategyItem();
            test.ItemType = 3;
            test.Description = "test";
            test.XCoordinate = 5;
            test.YCoordinate = 5;

            StrategiesItemDA.DeleteRecordById(test.ItemID);
            var result = StrategiesItemDA.ReadRecord(test.ItemID);

            if (result != null)
                return false;
            else
                return true;
        }
    }
}