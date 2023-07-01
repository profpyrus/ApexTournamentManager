using ApexTournamentManager.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.Model
{
    class Session
    {
        public string name { get; set; }
        public Guid id { get; set; }
        public string path { get; set; }
        public ObservableCollection<basicData> teams { get; set; }
        public ObservableCollection<Match> matches { get; set; }
        public ObservableCollection<Point> killPoints { get; set; }
        public ObservableCollection<Point> placementPoints { get; set; }

        public Session(Guid newId, string newName, string newPath)
        {
            name = newName; id = newId; path = newPath;
            teams = new ObservableCollection<basicData>(); matches = new ObservableCollection<Match>();
            killPoints = new ObservableCollection<Point>(); placementPoints = new ObservableCollection<Point>();
            DefaultPoints();
            AddTeam(Guid.NewGuid());
            matches.Add(new Match(matches.Count + 1, Guid.NewGuid(), teams));
        }

		public Session(Guid newId, string newName, string newPath, IEnumerable<Team> newTeams, IEnumerable<Match> newMatches, IEnumerable<Point> newKillPoints, IEnumerable<Point> newPlacementPoints)
		{
			name = newName; id = newId; path = newPath;
			teams = new ObservableCollection<basicData>(newTeams); matches = new ObservableCollection<Match>(newMatches);
			killPoints = new ObservableCollection<Point>(newKillPoints); placementPoints = new ObservableCollection<Point>(newPlacementPoints);
		}

		private void DefaultPoints()
        {
            DefaultPlacementPoints();
            DefaultKillPoints();
        }

        public void DefaultPlacementPoints()
        {
            placementPoints.Clear();
            placementPoints.Add(new Point(1, 10));
            placementPoints.Add(new Point(2, 5));
            placementPoints.Add(new Point(3, 3));
            placementPoints.Add(new Point(6, 1));
            placementPoints.Add(new Point(11, 0));
        }

        public void DefaultKillPoints()
        {
            killPoints.Clear();
            for(int i = 1; i <= 15; i++)
            {
                killPoints.Add(new Point(i, i));
            }
        }

        public void RemovePlayer(Guid playerId)
        {
            Player player = GetPlayerById(playerId);
            foreach (Team team in teams)
            {
                team.players.Remove(player);
            }
            RefreshAllPlayerNumbers();
        }

        public void RemovePlayer(Player player)
        {
            foreach (Team team in teams)
            {
                team.players.Remove(player);
            }
            RefreshAllPlayerNumbers();
        }

        public Player AddPlayerToTeam(Team team, Guid playerId, string playerName = "New Player")
        {
            return team.AddPlayer(playerId, playerName);
        }

        public Player AddPlayerToTeam(Guid teamId, Guid playerId, string playerName = "New Player")
        {
            Team team = GetTeamById(teamId);
            return team.AddPlayer(playerId, playerName);
        }

        public Team AddTeam(Guid teamId, string teamName = null)
        {
            if (teamName == null)
                teamName = "Team " + (teams.Count + 1);
            int newTeamNumber = teams.Count + 1;
            Team newTeam = new Team(teamId, newTeamNumber, teamName);
            teams.Add(newTeam);
            return newTeam;
        }

        public void RemoveTeam(Team team)
        {
            teams.Remove(team);
            RefreshTeamNumbers();
        }

        public void RemoveTeam(Guid teamId)
        {
            Team team = GetTeamById(teamId);
            teams.Remove(team);
            RefreshTeamNumbers();
        }

        public void AddMatch(Guid matchId)
        {
            matches.Add(new Match(matches.Count+1, matchId, teams));
        }

        public void RemoveMatch(Match match)
        {
            matches.Remove(match);
            foreach(Match _match in matches)
                _match.number = matches.IndexOf(_match) + 1;
        }

        public void RefreshAllPlayerNumbers()
        {
            foreach (Team team in teams)
            {
                team.RefreshPlayerNumbers();
            }
        }

        public void RefreshTeamNumbers()
        {
            foreach (Team team in teams)
            {
                team.SetTeamNumber(teams.IndexOf(team) + 1);
            }
        }

        public Player GetPlayerById(Guid searchId)
        {
            foreach (Team team in teams)
            {
                return (Player)team.players.FindObjectById(searchId);
            }
            return null;
        }

        public Team GetTeamById(Guid searchId)
        {
            return (Team)teams.FindObjectById(searchId);
        }

        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();
            foreach (Team team in teams)
            {
                foreach (Player player in team.players)
                {
                    players.Add(player);
                }
            }
            return players;
        }
    }
}
