using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApexTournamentManager.MVVM.Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class MatchViewModel
    {
        private protected Match _match;

        private protected ObservableCollection<TeamMatchViewModel> _teams;
        private protected Dictionary<Guid, PlayerData> _playerData => _match.playerData;

        public String MatchNumber
        {
            get { return _match.number.ToString(); }
        }

		public IEnumerable<TeamMatchViewModel> Teams
        {
            get { return _teams; }
        }

        public Match Match
        {
            get
            {
                return _match;
            }
        }

        public MatchViewModel(Match match, IEnumerable<basicData> teams)
        {
            _match = match;
            _teams = new ObservableCollection<TeamMatchViewModel>();
            foreach(Team team in teams)
            {
                _teams.Add(new TeamMatchViewModel(team, _match));
            }
        }
    }
}
