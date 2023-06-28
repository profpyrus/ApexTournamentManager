using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

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

			List<RankData> placementsData = new List<RankData>();
			List<RankData> killsData = new List<RankData>();
			List<RankData> pointsData = new List<RankData>();

			foreach (Team team in teams)
			{
				int placement = _match.teamPlacements[team.id];
				int kills = 0;
				int points = 0;
				foreach (Player player in team.players)
				{
					int playerKills = _match.playerData[player.id].kills;
					kills += playerKills;

					foreach (Point point in _session.killPoints.Reverse())
					{
						if (playerKills >= point.atLeast)
						{
							points += point.value;
							break;
						}
					}
				}

				placementsData.Add(new RankData(team.name, placement));
				killsData.Add(new RankData(team.name, kills));
				pointsData.Add(new RankData(team.name, points));
			}

			_values.Add(new LeaderboardValueViewModel(placementsData, "Placements"));
			_values.Add(new LeaderboardValueViewModel(killsData, "Kills"));
			_values.Add(new LeaderboardValueViewModel(pointsData, "Points"));
		}

		public LeaderboardEntityViewModel(Session session, IEnumerable<Player> players)
		{
			_session = session;
			IEnumerable<Match> matches = _session.matches;
			Name = "Players";
			_values = new ObservableCollection<LeaderboardValueViewModel>();

			List<RankData> killsTotalData = new List<RankData>();
			List<RankData> killsAvrgData = new List<RankData>();
			List<RankData> deathsTotalData = new List<RankData>();
			List<RankData> deathsAvrgData = new List<RankData>();
			List<RankData> kdData = new List<RankData>();
			List<RankData> pointsTotalData = new List<RankData>();
			List<RankData> pointsAvrgData = new List<RankData>();

			foreach (Player player in players)
			{
				List<int> allKills = new List<int>();
				List<int> allDeaths = new List<int>();
				List<int> allPoints = new List<int>();
				foreach (Match match in matches)
				{
					if (!match.playerData.ContainsKey(player.id))
					{
						match.playerData[player.id] = new PlayerData();
					}
					int kills = match.playerData[player.id].kills;
					allKills.Add(kills);

					int deaths = match.playerData[player.id].deaths;
					allDeaths.Add(deaths);
					
					int points = 0;
					int placement = 0;
					match.teamPlacements.TryGetValue(player.team.id, out placement);
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
					allPoints.Add(points);
				}
				if (allDeaths.Sum() == 0)
					kdData.Add(new RankData(player.name, allKills.Sum()));
				else
					kdData.Add(new RankData(player.name, (double)allKills.Sum() / (double)allDeaths.Sum()));

				killsTotalData.Add(new RankData(player.name, allKills.Sum()));
				killsAvrgData.Add(new RankData(player.name, allKills.Average()));
				deathsTotalData.Add(new RankData(player.name, allDeaths.Sum()));
				deathsAvrgData.Add(new RankData(player.name, allDeaths.Average()));
				pointsTotalData.Add(new RankData(player.name, allPoints.Sum()));
				pointsAvrgData.Add(new RankData(player.name, allPoints.Average()));
			}

			_values.Add(new LeaderboardValueViewModel(pointsTotalData, "Points (Total)"));
			_values.Add(new LeaderboardValueViewModel(pointsAvrgData, "Points (Average)"));
			_values.Add(new LeaderboardValueViewModel(killsTotalData, "Kills (Total)"));
			_values.Add(new LeaderboardValueViewModel(killsAvrgData, "Kills (Average)"));
			_values.Add(new LeaderboardValueViewModel(deathsTotalData, "Deaths (Total)"));
			_values.Add(new LeaderboardValueViewModel(deathsAvrgData, "Deaths (Average)"));
			_values.Add(new LeaderboardValueViewModel(kdData, "K/D"));
		}

		public LeaderboardEntityViewModel(Session session, IEnumerable<Team> teams)
		{
			_session = session;
			IEnumerable<Match> matches = _session.matches;
			Name = "Teams";
			_values = new ObservableCollection<LeaderboardValueViewModel>();

			List<RankData> placementsTotalData = new List<RankData>();
			List<RankData> placementsAvrgData = new List<RankData>();
			List<RankData> killsTotalData = new List<RankData>();
			List<RankData> killsAvrgData = new List<RankData>();
			List<RankData> pointsTotalData = new List<RankData>();
			List<RankData> pointsAvrgData = new List<RankData>();

			foreach (Team team in teams)
			{
				List<int> allPlacements = new List<int>();
				List<int> allKills = new List<int>();
				List<int> allPoints = new List<int>();
				foreach (Match match in matches)
				{
					int kills = 0;
					int points = 0;

					foreach (Player player in team.players)
					{
						if (!match.playerData.ContainsKey(player.id))
						{
							match.playerData[player.id] = new PlayerData();
						}
						int playerKills = match.playerData[player.id].kills;
						kills += playerKills;

						foreach (Point point in _session.killPoints.Reverse())
						{
							if (playerKills >= point.atLeast)
							{
								points += point.value;
								break;
							}
						}
					}
					if (!match.teamPlacements.ContainsKey(team.id))
					{
						match.teamPlacements[team.id] = 0;
					}
					int placement = match.teamPlacements[team.id];
					if (placement != 0)
						allPlacements.Add(placement);

					foreach (Point point in _session.placementPoints.Reverse())
					{
						if (placement >= point.atLeast)
						{
							points += point.value;
							break;
						}
					}

					allKills.Add(kills);
					allPoints.Add(points);
				}
				if(allPlacements.Count != 0)
				{
					placementsTotalData.Add(new RankData(team.name, allPlacements.Sum()));
					placementsAvrgData.Add(new RankData(team.name, allPlacements.Average()));
				}
				killsTotalData.Add(new RankData(team.name, allKills.Sum()));
				killsAvrgData.Add(new RankData(team.name, allKills.Average()));
				pointsTotalData.Add(new RankData(team.name, allPoints.Sum()));
				pointsAvrgData.Add(new RankData(team.name, allPoints.Average()));
			}

			_values.Add(new LeaderboardValueViewModel(pointsTotalData, "Points (Total)"));
			_values.Add(new LeaderboardValueViewModel(pointsAvrgData, "Points (Average)"));
			_values.Add(new LeaderboardValueViewModel(killsTotalData, "Kills (Total)"));
			_values.Add(new LeaderboardValueViewModel(killsAvrgData, "Kills (Average)"));
			if(placementsTotalData.Count != 0)
			{
				_values.Add(new LeaderboardValueViewModel(placementsTotalData, "Placements (Total)"));
				_values.Add(new LeaderboardValueViewModel(placementsAvrgData, "Placements (Average)"));
			}
		}
	}
}
