using ApexTournamentManager.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using Microsoft.Win32;
using System.ComponentModel;

namespace ApexTournamentManager
{
	/// <summary>
	/// Interaktionslogik für OpenOrNewDialog.xaml
	/// </summary>
	public partial class OpenOrNewDialog
	{
		private EventHandler FoundValidFile;

		private SaveAndLoadHandler snl;

		public event PropertyChangedEventHandler PropertyChanged;
		public string SessionName { get; set; }

		public OpenOrNewDialog(App app)
		{
			FoundValidFile += app.ValidPathFound;
			SessionName = "New Session";

			snl = new SaveAndLoadHandler();

			InitializeComponent();
		}


		private void Border_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				this.DragMove();
		}

		private void CreateNewSession_Click(object sender, RoutedEventArgs e)
		{
			FoundValidFile?.Invoke(snl.CreateSession(SessionName), EventArgs.Empty);
			this.Close();
		}

		private void OpenSession_Click(object sender, RoutedEventArgs e)
		{
			FoundValidFile?.Invoke("", EventArgs.Empty);
			this.Close();
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
