using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private Window _window;
        private Session _session { get; set; }

        public String SessionName { get { return _session.name; } }

        public RelayCommand TeamViewCommand { get; set; }
        public RelayCommand MatchViewCommand { get; set; }
        public RelayCommand PointsViewCommand { get; set; }
        public RelayCommand LeaderboardViewCommand { get; set; }

		public RelayCommand CloseWindow { get; set; }
		public RelayCommand MinimizeWindow { get; set; }


		public TeamManagementViewModel TeamVM { get; set; }
        public PointsManagementViewModel PointVM { get; set; }


		private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(string name, Window window)
        {
            string test = Newtonsoft.Json.JsonConvert.SerializeObject(new Session(Guid.NewGuid(), "SerializedSession"), Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore});


			_window = window;
            _session = new Session(Guid.NewGuid(), name);

            TeamVM = new TeamManagementViewModel(_session);
            PointVM = new PointsManagementViewModel(_session);
            CurrentView = PointVM;

            TeamViewCommand = new RelayCommand(o => { CurrentView = TeamVM; });
            MatchViewCommand = new RelayCommand(o => { CurrentView = new MatchManagementViewModel(_session); });
            PointsViewCommand = new RelayCommand(o => { CurrentView = PointVM; });
            LeaderboardViewCommand = new RelayCommand(o => { CurrentView = new LeaderboardViewModel(_session); });

            CloseWindow = new RelayCommand(o => { _window.Close(); });
			MinimizeWindow = new RelayCommand(o => { _window.WindowState = WindowState.Minimized; });
		}
    }
}
