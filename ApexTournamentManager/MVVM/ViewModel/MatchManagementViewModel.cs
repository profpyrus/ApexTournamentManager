using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class MatchManagementViewModel : ObservableObject
    {
        private Session _session;

        private readonly ObservableCollection<MatchViewModel> _matches;
        public IEnumerable<MatchViewModel> Matches => _matches;

        private MatchViewModel _selectedMatch;
        public MatchViewModel SelectedMatch
        {
            get
            {
                return _selectedMatch;
            }
            set
            {
                _selectedMatch = value;
            }
        }

        public string Placement
        {
            get
            {
                if (SelectedMatch != null && SelectedTeam != null)
                    return SelectedMatch.Placements[SelectedTeam.teamId].ToString();
                else
                    return "XX";
            }

            set
            {
                int newPlacement;
                if (SelectedMatch != null
                    && SelectedTeam != null
                    && int.TryParse(value, out newPlacement)
                    && newPlacement >= 0
                    && newPlacement <= 30)
                    SelectedMatch.Match.teamPlacements[SelectedTeam.teamId] = newPlacement;
            }
        }

        public RelayCommand AddMatchCommand { get; set; }
        public RelayCommand RemoveMatchCommand { get; set; }

        public MatchManagementViewModel(Session session)
        {
            _matches = new ObservableCollection<MatchViewModel>();
            _session = session;

            AddMatchCommand = new RelayCommand(o => { _session.AddMatch(Guid.NewGuid()); UpdateData(); });
            RemoveMatchCommand = new RelayCommand(o => { _session.RemoveMatch(SelectedMatch.Match); UpdateData(); });

            UpdateData();
        }

        private void UpdateData()
        {
            _matches.Clear();
            foreach (Match match in _session.matches)
            {
                _matches.Add(new MatchViewModel(match));
            }
        }
    }
}
