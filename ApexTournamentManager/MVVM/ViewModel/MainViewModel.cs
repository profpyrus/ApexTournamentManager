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

        ObsConnectionHandler obs;

        public Session session
        {
            get { return _session; }
            set { _session = value; OnPropertyChanged(nameof(SessionName)); }
        }
        public String SessionName { get { return _session.name; } }

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand TeamViewCommand { get; set; }
        public RelayCommand MatchViewCommand { get; set; }
        public RelayCommand PointsViewCommand { get; set; }
        public RelayCommand LeaderboardViewCommand { get; set; }

		public RelayCommand CloseWindow { get; set; }
		public RelayCommand MinimizeWindow { get; set; }


        public HomeViewModel HomeVM { get; set; }
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

        public MainViewModel(string sessionPath, Window window, System.Windows.Application app)
        {
            SaveAndLoadHandler snl = new SaveAndLoadHandler();

			_window = window;
            _session = snl.OpenSession(sessionPath);

            obs = new ObsConnectionHandler();

            HomeVM = new HomeViewModel(obs,this, app, snl);
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });
            TeamViewCommand = new RelayCommand(o => { CurrentView = new TeamManagementViewModel(_session); });
            MatchViewCommand = new RelayCommand(o => { CurrentView = new MatchManagementViewModel(_session); });
            PointsViewCommand = new RelayCommand(o => { CurrentView = new PointsManagementViewModel(_session); });
            LeaderboardViewCommand = new RelayCommand(o => { CurrentView = new LeaderboardViewModel(_session, obs); });

            CloseWindow = new RelayCommand(o => { HomeVM.SaveSession.Execute(this); _window.Close(); });
			MinimizeWindow = new RelayCommand(o => { _window.WindowState = WindowState.Minimized; });
		}
    }
}
