using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using ApexTournamentManager.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static ApexTournamentManager.Core.SaveAndLoadHandler;

namespace ApexTournamentManager
{
    partial class MainWindow : Window
    {
        MainViewModel mvm;

        public MainWindow(string sessionPath, Application app)
        {
            this.DataContext = mvm = new MainViewModel(sessionPath, this, app);
            InitializeComponent();
            Closing += OnWindowClosing;
        }



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

		public void OnWindowClosing(object sender, CancelEventArgs e)
		{
            MessageBoxResult result = MessageBox.Show("Do you want to save the session?", "Save Session", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    SaveSession(mvm.session);
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
		}
	}
}
