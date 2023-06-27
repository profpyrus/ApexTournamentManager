using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class TeamManagementViewModel : ObservableObject
    {
        private Session _session;

        private readonly ObservableCollection<TeamViewModel> _teams;

        public TeamViewModel SelectedTeam { get; set; }

        public IEnumerable<TeamViewModel> Teams => _teams;

        public RelayCommand AddTeamCommand { get; set; }
        public RelayCommand RemoveTeamCommand { get; set; }

        public TeamManagementViewModel(Session session)
        {
            _teams = new ObservableCollection<TeamViewModel>();
            _session = session;

            AddTeamCommand = new RelayCommand(o => { _session.AddTeam(System.Guid.NewGuid()); UpdateTeams(); });
            RemoveTeamCommand = new RelayCommand(o => { _session.RemoveTeam(SelectedTeam.teamId); UpdateTeams(); });

            this.PropertyChanged += (obj, args) => { UpdateTeams(); };

            UpdateTeams();
        }

        private void UpdateTeams()
        {
            _teams.Clear();
            foreach (Team team in _session.teams)
            {
                _teams.Add(new TeamViewModel(team));
            }
        }
    }
}
