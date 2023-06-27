using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApexTournamentManager.MVVM.Model;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class MatchViewModel
    {
        private protected Match _match;

        private protected Dictionary<Guid, int> _teamPlacements => _match.teamPlacements;
        private protected Dictionary<Guid, PlayerData> _playerData => _match.playerData;

        public Dictionary<Guid, int> Placements
        {
            get { return _teamPlacements; }
            set { _match.teamPlacements = value; }
        }

        public Match Match
        {
            get
            {
                return _match;
            }
        }

        public string MatchNumber
        {
            get
            {
                return _match.number.ToString();
            }
        }

        public MatchViewModel(Match match)
        {
            _match = match;
        }
    }
}
