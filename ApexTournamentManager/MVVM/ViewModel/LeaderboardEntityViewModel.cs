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
	internal class LeaderboardEntityViewModel : ObservableObject
	{
		private readonly Match _match;
		private readonly ObservableCollection<LeaderboardValueViewModel> _values;
		public IEnumerable<LeaderboardValueViewModel> Values { get { return _values; } }
		public string Name { get; }

		public LeaderboardEntityViewModel(Match match, IEnumerable<Player> players)
		{
			_match = match;
			Name = "Players";
			_values = new ObservableCollection<LeaderboardValueViewModel>();

			List<RankData> data = new List<RankData>();
			foreach(Player player in players)
			{
				data.Add(new RankData(player.name, _match.playerData[player.id].kills));
			}

			_values.Add(new LeaderboardValueViewModel(data, "Kills"));
		}
		public LeaderboardEntityViewModel(Match match, IEnumerable<Team> teams)
		{
			_match = match;
			Name = "Teams";
			_values = new ObservableCollection<LeaderboardValueViewModel>();

			List<RankData> data = new List<RankData>();
			foreach (Team team in teams)
			{
				data.Add(new RankData(team.name, _match.teamPlacements[team.id]));
			}

			_values.Add(new LeaderboardValueViewModel(data, "Kills"));
		}
	}
}
