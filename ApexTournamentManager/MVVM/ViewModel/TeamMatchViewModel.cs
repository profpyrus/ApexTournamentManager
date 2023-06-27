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
    class TeamMatchViewModel : ObservableObject
    {
        private readonly Match _match;

        private readonly Team _team;

        private ObservableCollection<PlayerMatchViewModel> _players;

        public string Name
        {
            get { return _team.name; }
        }

		public IEnumerable<PlayerMatchViewModel> Players
		{
			get { return _players; }
		}

		public string Placement
        {
            get
            {
                int placement;
                if (!_match.teamPlacements.TryGetValue(_team.id, out placement))
                {
                    placement = 0;
                    _match.teamPlacements[_team.id] = placement;
                }
				return placement.ToString();
			}
            set
            {
                int newPlacement;
                if(int.TryParse(value, out newPlacement)
                    && newPlacement >= 0
                    && newPlacement <= 30)
                    _match.teamPlacements[_team.id] = newPlacement;
            }
        }

        public TeamMatchViewModel(Team team, Match match)
        {
            _team = team;
            _match = match;
            _players = new ObservableCollection<PlayerMatchViewModel>();
            foreach(Player player in _team.players)
            {
                _players.Add(new PlayerMatchViewModel(player, match));
            }
        }
    }
}
