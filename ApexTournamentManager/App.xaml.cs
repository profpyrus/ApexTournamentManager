using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;

namespace ApexTournamentManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected OpenOrNewDialog dialog;

        protected override void OnStartup(StartupEventArgs e)
        {
            dialog = new OpenOrNewDialog(this);
			Current.MainWindow = dialog;
			Current.MainWindow.Show();

			base.OnStartup(e);
        }

        public void ValidPathFound(object sender, EventArgs e)
		{
            string path = sender as string;
            if (File.Exists(path))
			{
				Current.MainWindow = new MainWindow(sender as string, this);
                dialog.Close();
				Current.MainWindow.Show();
			}
		}
	}
}
