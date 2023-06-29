using ApexTournamentManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OBSWebsocketDotNet;
using System.Windows.Media;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject
    {
		ObsConnectionHandler _obs;
        SolidColorBrush connectedColor;
		SolidColorBrush disconnectedColor;

		public SolidColorBrush ConnectionStatus { get; set; }

		public RelayCommand ConnectToOBS { get; set; }
		public RelayCommand TestvaluesToOBS { get; set; }
		public RelayCommand ClearvaluesToOBS { get; set; }
		public string IPText { get; set; }
		public string PortText { get; set; }

		public HomeViewModel(ObsConnectionHandler obs, Application app)
        {
            _obs = obs;
			connectedColor = (SolidColorBrush)app.Resources["Connected"];
			disconnectedColor = (SolidColorBrush)app.Resources["NotConnected"];
			ConnectionStatus = disconnectedColor;

			IPText = ObsConnectionHandlerDefines.defaultIp;
            PortText = ObsConnectionHandlerDefines.defaultPort;
            ConnectToOBS = new RelayCommand(o => { ConnectionStatus = _obs.Connect(IPText, PortText, "") ? connectedColor : disconnectedColor; OnPropertyChanged(nameof(ConnectionStatus)); });
            TestvaluesToOBS = new RelayCommand(o => { _obs.SendTestleaderboardToObs(); });
			ClearvaluesToOBS = new RelayCommand(o => { _obs.ClearLeaderboardToObs(); });
		}
    }
}
