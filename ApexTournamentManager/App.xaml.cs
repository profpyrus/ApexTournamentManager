﻿using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ApexTournamentManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Current.MainWindow = new MainWindow("Testsession", this);
            Current.MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
