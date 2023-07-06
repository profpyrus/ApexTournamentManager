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
		MainViewModel _mvm;
        SolidColorBrush connectedColor;
		SolidColorBrush disconnectedColor;

		public SolidColorBrush ConnectionStatus { get; set; }
		public bool IsConnected { get; set; }

		public RelayCommand CreateSession { get; set; }
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

		public HomeViewModel(ObsConnectionHandler obs, MainViewModel mvm, Application app)
        {
            _obs = obs;
			_mvm = mvm;
			connectedColor = (SolidColorBrush)app.Resources["Connected"];
			disconnectedColor = (SolidColorBrush)app.Resources["NotConnected"];
			ConnectionStatus = disconnectedColor;

			IPText = ObsConnectionHandlerDefines.defaultIp;
            PortText = ObsConnectionHandlerDefines.defaultPort;
			TemplateName = "RANKTEMPLATE";
			SceneName = "Scene";

			CreateSession = new RelayCommand(o => { var session = SaveAndLoadHandler.OpenSession(SaveAndLoadHandler.CreateSession()); if (session != null) _mvm.session = session; });
			SaveSession = new RelayCommand(o => { SaveAndLoadHandler.SaveSession(_mvm.session); });
			SaveSessionAs = new RelayCommand(o => { SaveAndLoadHandler.SaveSessionAs(_mvm.session); });
			OpenSession = new RelayCommand(o => { var session = SaveAndLoadHandler.OpenSession(); if (session != null) _mvm.session = session; });

			ConnectToOBS = new RelayCommand(o => { ConnectionStatus = (IsConnected = _obs.Connect(IPText, PortText, "")) ? connectedColor : disconnectedColor; OnPropertyChanged(nameof(ConnectionStatus)); OnPropertyChanged(nameof(IsConnected)); });
            TestvaluesToOBS = new RelayCommand(o => { _obs.SendTestleaderboardToObs(); });
			ClearvaluesToOBS = new RelayCommand(o => { _obs.ClearLeaderboardToObs(); });
			CreateSources = new RelayCommand(o => { _obs.CreateAllRankTextsources(TemplateName, SceneName); });
		}
    }
}
