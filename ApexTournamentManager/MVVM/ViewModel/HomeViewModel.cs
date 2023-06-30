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
		SaveAndLoadHandler _snl;
		MainViewModel _mvm;
        SolidColorBrush connectedColor;
		SolidColorBrush disconnectedColor;

		public SolidColorBrush ConnectionStatus { get; set; }

		public RelayCommand SaveSession { get; set; }
		public RelayCommand SaveSessionAs { get; set; }
		public RelayCommand OpenSession { get; set; }

		public RelayCommand ConnectToOBS { get; set; }
		public RelayCommand TestvaluesToOBS { get; set; }
		public RelayCommand ClearvaluesToOBS { get; set; }
		public string IPText { get; set; }
		public string PortText { get; set; }

		public HomeViewModel(ObsConnectionHandler obs, MainViewModel mvm, Application app, SaveAndLoadHandler snl)
        {
            _obs = obs;
			_mvm = mvm;
			_snl = snl;
			connectedColor = (SolidColorBrush)app.Resources["Connected"];
			disconnectedColor = (SolidColorBrush)app.Resources["NotConnected"];
			ConnectionStatus = disconnectedColor;

			IPText = ObsConnectionHandlerDefines.defaultIp;
            PortText = ObsConnectionHandlerDefines.defaultPort;

			SaveSession = new RelayCommand(o => { _snl.SaveSession(mvm.session); });
			SaveSessionAs = new RelayCommand(o => { _snl.SaveSessionAs(mvm.session); });
			OpenSession = new RelayCommand(o => { mvm.session = _snl.OpenSession(); });

			ConnectToOBS = new RelayCommand(o => { ConnectionStatus = _obs.Connect(IPText, PortText, "") ? connectedColor : disconnectedColor; OnPropertyChanged(nameof(ConnectionStatus)); });
            TestvaluesToOBS = new RelayCommand(o => { _obs.SendTestleaderboardToObs(); });
			ClearvaluesToOBS = new RelayCommand(o => { _obs.ClearLeaderboardToObs(); });
		}
    }
}
