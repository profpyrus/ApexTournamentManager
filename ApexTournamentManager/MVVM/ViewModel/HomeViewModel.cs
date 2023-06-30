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
		public bool IsConnected { get; set; }

		public RelayCommand SaveSession { get; set; }
		public RelayCommand SaveSessionAs { get; set; }
		public RelayCommand OpenSession { get; set; }

		public RelayCommand ConnectToOBS { get; set; }
		public RelayCommand TestvaluesToOBS { get; set; }
		public RelayCommand ClearvaluesToOBS { get; set; }
		public RelayCommand CreateSources { get; set; }
		public string IPText { get; set; }
		public string PortText { get; set; }
		public string TemplateName { get; set; }
		public string SceneName { get; set; }

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
			TemplateName = "RANKTEMPLATE";
			SceneName = "Scene";

			SaveSession = new RelayCommand(o => { _snl.SaveSession(_mvm.session); });
			SaveSessionAs = new RelayCommand(o => { _snl.SaveSessionAs(_mvm.session); });
			OpenSession = new RelayCommand(o => { _mvm.session = _snl.OpenSession(); });

			ConnectToOBS = new RelayCommand(o => { ConnectionStatus = (IsConnected = _obs.Connect(IPText, PortText, "")) ? connectedColor : disconnectedColor; OnPropertyChanged(nameof(ConnectionStatus)); OnPropertyChanged(nameof(IsConnected)); });
            TestvaluesToOBS = new RelayCommand(o => { _obs.SendTestleaderboardToObs(); });
			ClearvaluesToOBS = new RelayCommand(o => { _obs.ClearLeaderboardToObs(); });
			CreateSources = new RelayCommand(o => { _obs.CreateAllRankTextsources(TemplateName, SceneName); });
		}
    }
}
