﻿using ApexTournamentManager.MVVM.Model;
using ApexTournamentManager.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApexTournamentManager
{
    partial class MainWindow : Window
    {
        public MainWindow(string sessionPath, Application app)
        {
            this.DataContext = new MainViewModel(sessionPath, this, app);
            InitializeComponent();
        }



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

		private void Button_MouseDown(object sender, MouseButtonEventArgs e)
		{

		}
	}
}
