using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private Session _session { get; set; }

        public String SessionName { get { return _session.name; } }

        public RelayCommand TeamViewCommand { get; set; }
        public RelayCommand MatchViewCommand { get; set; }
        public RelayCommand PointsViewCommand { get; set; }
        public RelayCommand LeaderboardViewCommand { get; set; }


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

        public MainViewModel(string name)
        {
            _session = new Session(Guid.NewGuid(), name);

            TeamVM = new TeamManagementViewModel(_session);
            PointVM = new PointsManagementViewModel(_session);
            CurrentView = PointVM;

            TeamViewCommand = new RelayCommand(o => { CurrentView = TeamVM; });
            MatchViewCommand = new RelayCommand(o => { CurrentView = new MatchManagementViewModel(_session); });
            PointsViewCommand = new RelayCommand(o => { CurrentView = PointVM; });
            LeaderboardViewCommand = new RelayCommand(o => { CurrentView = new LeaderboardViewModel(_session); });
        }
    }
}
