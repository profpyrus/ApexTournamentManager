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
		private readonly Session _session;
		private readonly Match _match;
		private readonly ObservableCollection<LeaderboardValueViewModel> _values;
        public IEnumerable<LeaderboardValueViewModel> Values { get { return _values; } }
		public string Name { get; }

        public LeaderboardEntityViewModel(Session session, Match match, IEnumerable<Player> players)
		{
			_session = session;
			_match = match;
			Name = "Players";
			_values = new ObservableCollection<LeaderboardValueViewModel>();


			List<RankData> killData = new List<RankData>();
			List<RankData> deathData = new List<RankData>();
			List<RankData> kdData = new List<RankData>();
			List<RankData> pointsData = new List<RankData>();

			foreach (Player player in players)
			{
                if (!_match.playerData.ContainsKey(player.id))
                {
					_match.playerData[player.id] = new PlayerData();
                }
				int kills = _match.playerData[player.id].kills;
				killData.Add(new RankData(player.name, kills));

				deathData.Add(new RankData(player.name, _match.playerData[player.id].deaths));

				if (_match.playerData[player.id].deaths == 0)
					kdData.Add(new RankData(player.name, (double)_match.playerData[player.id].kills));
				else
					kdData.Add(new RankData(player.name, (double)_match.playerData[player.id].kills / (double)_match.playerData[player.id].deaths));

				int points = 0;
				int placement = 0;
				_match.teamPlacements.TryGetValue(player.team.id, out placement);
				foreach(Point point in _session.killPoints.Reverse())
                {
					if(kills >= point.atLeast)
                    {
						points += point.value;
						break;
                    }
                }
				foreach (Point point in _session.placementPoints.Reverse())
				{
					if (placement >= point.atLeast)
					{
						points += point.value;
						break;
					}
				}
				pointsData.Add(new RankData(player.name, points));
			}

			_values.Add(new LeaderboardValueViewModel(killData, "Kills"));
			_values.Add(new LeaderboardValueViewModel(deathData, "Deaths"));
			_values.Add(new LeaderboardValueViewModel(kdData, "K/D"));
			_values.Add(new LeaderboardValueViewModel(pointsData, "Points"));
		}

		public LeaderboardEntityViewModel(Session session, Match match, IEnumerable<Team> teams)
		{
			_session = session;
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

		public LeaderboardEntityViewModel(Session session, IEnumerable<Player> players)
		{
			_session = session;
			IEnumerable<Match> matches = _session.matches;
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

			List<RankData> killsTotalData = new List<RankData>();
			List<RankData> killsAvrgData = new List<RankData>();
			List<RankData> deathsTotalData = new List<RankData>();
			List<RankData> deathsAvrgData = new List<RankData>();
			List<RankData> kdData = new List<RankData>();
			List<RankData> pointsTotalData = new List<RankData>();
			List<RankData> pointsAvrgData = new List<RankData>();

			foreach (Player player in players)
			{
				int killsTotal = 0;
				int deathsTotal = 0;
				int pointsTotal = 0;
				List<int> allKills = new List<int>();
				List<int> allDeaths = new List<int>();
				List<int> allPoints = new List<int>();
				foreach (Match match in matches)
				{
					if (!_match.playerData.ContainsKey(player.id))
					{
						_match.playerData[player.id] = new PlayerData();
					}
					int kills = _match.playerData[player.id].kills;
					killsTotal += kills;
					allKills.Add(kills);

					int deaths = _match.playerData[player.id].deaths;
					deathsTotal += deaths;
					allDeaths.Add(deaths);
					
					int points = 0;
					int placement = 0;
					_match.teamPlacements.TryGetValue(player.team.id, out placement);
					foreach (Point point in _session.killPoints.Reverse())
					{
						if (kills >= point.atLeast)
						{
							points += point.value;
							break;
						}
					}
					foreach (Point point in _session.placementPoints.Reverse())
					{
						if (placement >= point.atLeast)
						{
							points += point.value;
							break;
						}
					}
				}
				if (deathsTotal == 0)
					kdData.Add(new RankData(player.name, killsTotal));
				else
					kdData.Add(new RankData(player.name, (double)killsTotal / (double)deathsTotal));

				killsTotalData.Add(new RankData(player.name, killsTotal));
				killsAvrgData.Add(new RankData(player.name, allKills.Average()));
				deathsTotalData.Add(new RankData(player.name, deathsTotal));
				deathsAvrgData.Add(new RankData(player.name, allDeaths.Average()));
				pointsTotalData.Add(new RankData(player.name, pointsTotal));
				pointsAvrgData.Add(new RankData(player.name, pointsAvrg));
			}

			_values.Add(new LeaderboardValueViewModel(killsTotalData, "Total kills"));
			_values.Add(new LeaderboardValueViewModel(killData, "Kills"));
			_values.Add(new LeaderboardValueViewModel(deathData, "Deaths"));
			_values.Add(new LeaderboardValueViewModel(kdData, "K/D"));
			_values.Add(new LeaderboardValueViewModel(pointsData, "Points"));
		}

		public LeaderboardEntityViewModel(Session session, IEnumerable<Team> teams)
		{
			_session = session;
			IEnumerable<Match> matches = _session.matches;
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
