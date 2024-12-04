using StrategySync.Classes.Strategy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StrategySync
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string User { get; set; } = "undefined";

        public Strategy CurrentStrategy;
    }
}
