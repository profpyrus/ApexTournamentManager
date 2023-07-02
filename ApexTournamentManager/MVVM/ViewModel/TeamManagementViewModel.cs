using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class TeamManagementViewModel : ObservableObject
    {
        private Session _session;

        private readonly ObservableCollection<TeamViewModel> _teams;

        private TeamViewModel _selectedTeam;
        public TeamViewModel SelectedTeam
        {
            get { return _selectedTeam; }
            set { if(value != null) _selectedTeam = value; }
        }
        public int TeamCount { get { return _teams.Count; } }

        public IEnumerable<TeamViewModel> Teams => _teams;

        public RelayCommand AddTeamCommand { get; set; }
        public RelayCommand RemoveTeamCommand { get; set; }

        public TeamManagementViewModel(Session session)
        {
            _teams = new ObservableCollection<TeamViewModel>();
            _session = session;

            AddTeamCommand = new RelayCommand(o => { _session.AddTeam(System.Guid.NewGuid()); UpdateTeams(); });
            RemoveTeamCommand = new RelayCommand(o => { _session.RemoveTeam(SelectedTeam.teamId); UpdateTeams(_teams.IndexOf(SelectedTeam)); });

            UpdateTeams(0);
        }

        private void UpdateTeams()
        {
            _teams.Clear();
            foreach (Team team in _session.teams)
            {
                _teams.Add(new TeamViewModel(team));
            }
            _selectedTeam = _teams.Last();
			OnPropertyChanged(nameof(SelectedTeam));
			OnPropertyChanged(nameof(TeamCount));
		}

		private void UpdateTeams(int index)
		{
			_teams.Clear();
			foreach (Team team in _session.teams)
			{
				_teams.Add(new TeamViewModel(team));
			}
			_selectedTeam = _teams.ElementAtOrLast(index);
			OnPropertyChanged(nameof(SelectedTeam));
			OnPropertyChanged(nameof(TeamCount));
		}
	}
}
