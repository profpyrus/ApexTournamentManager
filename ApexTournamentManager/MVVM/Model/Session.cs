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
        public Guid id { get; set; }
        public ObservableCollection<basicData> teams { get; set; }

        public ObservableCollection<Match> matches { get; set; }

        public Session(Guid newId)
        {
            id = newId; teams = new ObservableCollection<basicData>(); matches = new ObservableCollection<Match>();
            AddPlayerToTeam(AddTeam(Guid.NewGuid()), Guid.NewGuid());
            matches.Add(new Match(matches.Count + 1, Guid.NewGuid(), teams.ToList()));
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

        public Player AddPlayerToTeam(Team team, Guid playerId, string playerName = "New Player", int playerKills = 0, int playerDeaths = 0)
        {
            return team.AddPlayer(playerId, playerName, playerKills, playerDeaths);
        }

        public Player AddPlayerToTeam(Guid teamId, Guid playerId, string playerName = "New Player", int playerKills = 0, int playerDeaths = 0)
        {
            Team team = GetTeamById(teamId);
            return team.AddPlayer(playerId, playerName, playerKills, playerDeaths);
        }

        public Team AddTeam(Guid teamId, string teamName = null, int teamWins = 0)
        {
            if (teamName == null)
                teamName = "Team " + (teams.Count + 1);
            int newTeamNumber = teams.Count + 1;
            Team newTeam = new Team(teamId, newTeamNumber, teamName, teamWins);
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
