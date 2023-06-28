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
                if (!_match.playerData.ContainsKey(player.id))
                {
					_match.playerData[player.id] = new PlayerData();
                }
				data.Add(new RankData(player.name, _match.playerData[player.id].kills));
			}

			_values.Add(new LeaderboardValueViewModel(data, "Kills"));
			data.Clear();

			foreach (Player player in players)
			{
				if (!_match.playerData.ContainsKey(player.id))
				{
					_match.playerData[player.id] = new PlayerData();
				}
				data.Add(new RankData(player.name, _match.playerData[player.id].deaths));
			}

			_values.Add(new LeaderboardValueViewModel(data, "Deaths"));
			data.Clear();

			foreach (Player player in players)
			{
				if (!_match.playerData.ContainsKey(player.id))
				{
					_match.playerData[player.id] = new PlayerData();
				}
				if(_match.playerData[player.id].deaths == 0)
					data.Add(new RankData(player.name, (double)_match.playerData[player.id].kills));
				else
					data.Add(new RankData(player.name, (double)_match.playerData[player.id].kills / (double)_match.playerData[player.id].deaths));
			}
			_values.Add(new LeaderboardValueViewModel(data, "K/D"));
			data.Clear();
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

			_values.Add(new LeaderboardValueViewModel(data, "Placements"));
		}

		public LeaderboardEntityViewModel(IEnumerable<Match> matches, IEnumerable<Player> players)
		{
			Name = "Players";
			_values = new ObservableCollection<LeaderboardValueViewModel>();

			List<RankData> data = new List<RankData>();
			foreach (Player player in players)
			{
				int killsTotal = 0;
				foreach(Match match in matches)
				{
					if (!match.playerData.ContainsKey(player.id))
					{
						match.playerData[player.id] = new PlayerData();
					}
					killsTotal += match.playerData[player.id].kills;
				}
				data.Add(new RankData(player.name, killsTotal));
			}
			_values.Add(new LeaderboardValueViewModel(data, "Kills Total"));
			data.Clear();

			foreach (Player player in players)
			{
				List<int> killsTotal = new List<int>();
				foreach (Match match in matches)
				{
					if (!match.playerData.ContainsKey(player.id))
					{
						match.playerData[player.id] = new PlayerData();
					}
					killsTotal.Add(match.playerData[player.id].kills);
				}
				data.Add(new RankData(player.name, killsTotal.Average()));
			}
			_values.Add(new LeaderboardValueViewModel(data, "Kills Average"));
			data.Clear();

			foreach (Player player in players)
			{
				int deathsTotal = 0;
				foreach (Match match in matches)
				{
					if (!match.playerData.ContainsKey(player.id))
					{
						match.playerData[player.id] = new PlayerData();
					}
					deathsTotal += match.playerData[player.id].deaths;
				}
				data.Add(new RankData(player.name, deathsTotal));
			}
			_values.Add(new LeaderboardValueViewModel(data, "Deaths Total"));
			data.Clear();

			foreach (Player player in players)
			{
				List<int> deathsTotal = new List<int>();
				foreach (Match match in matches)
				{
					if (!match.playerData.ContainsKey(player.id))
					{
						match.playerData[player.id] = new PlayerData();
					}
					deathsTotal.Add(match.playerData[player.id].deaths);
				}
				data.Add(new RankData(player.name, deathsTotal.Average()));
			}
			_values.Add(new LeaderboardValueViewModel(data, "Deaths Average"));
			data.Clear();
		}
		public LeaderboardEntityViewModel(IEnumerable<Match> matches, IEnumerable<Team> teams)
		{
			Name = "Teams";
			_values = new ObservableCollection<LeaderboardValueViewModel>();

			List<RankData> data = new List<RankData>();
			foreach (Team team in teams)
			{
				List<int> placements = new List<int>();
				foreach (Match match in matches)
				{
					if (!match.teamPlacements.ContainsKey(team.id))
					{
						match.teamPlacements[team.id] = 0;
					}
					placements.Add(match.teamPlacements[team.id]);
				}
				data.Add(new RankData(team.name, placements.Average()));
			}

			_values.Add(new LeaderboardValueViewModel(data, "Placements Average"));
			data.Clear();
		}
	}
}
