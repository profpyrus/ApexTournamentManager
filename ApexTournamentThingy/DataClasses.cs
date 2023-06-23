using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentThingy
{
    public class Session
    {
        public Guid id { get; set; }
        public ObservableCollection<basicData> teams { get; set; }

        public ObservableCollection<Match> matches { get; set; }

        public Session(Guid newId)
        {
            id = newId; teams = new ObservableCollection<basicData>(); matches = new ObservableCollection<Match>();
            matches.Add(new Match(matches.Count + 1, new Guid(), teams.ToList()));
        }

        public void RemovePlayer(Guid playerId)
        {
            Player player = GetPlayerById(playerId);
            foreach(Team team in teams)
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

        public Player AddPlayerToTeam(Team team, Guid playerId, string playerName, int playerKills = 0, int playerDeaths = 0)
        {
            return team.AddPlayer(playerId, playerName, playerKills, playerDeaths);
        }

        public Player AddPlayerToTeam(Guid teamId, Guid playerId, string playerName, int playerKills = 0, int playerDeaths = 0)
        {
            Team team = GetTeamById(teamId);
            return team.AddPlayer(playerId, playerName, playerKills, playerDeaths);
        }

        public Team AddTeam(Guid teamId, string teamName, int teamWins = 0)
        {
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

        public void RefreshAllPlayerNumbers()
        {
            foreach(Team team in teams)
            {
                team.RefreshPlayerNumbers();
            }
        }

        public void RefreshTeamNumbers()
        {
            foreach(Team team in teams)
            {
                team.SetTeamNumber(teams.IndexOf(team) + 1);
            }
        }

        public Player GetPlayerById(Guid searchId)
        {
            foreach(Team team in teams)
            {
                return (Player)team.players.FindObjectById(searchId);
            }
            return null;
        }

        public Team GetTeamById(Guid searchId)
        {
            return (Team)teams.FindObjectById(searchId);
        }
    }

    public class Team : basicData, INotifyPropertyChanged
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public int teamNumber { get; set; }
        public ObservableCollection<basicData> players { get; set; }

        public Team(Guid newId, int newTeamNumber, string newName, int newWins = 0)
        {
            id = newId; teamNumber = newTeamNumber; name = newName; players = new ObservableCollection<basicData>();
        }

        public void SetTeamNumber(int newTeamNumber)
        {
            teamNumber = newTeamNumber;
            NotifyPropertyChanged("teamNumber");
        }

        public Player AddPlayer(Guid playerId, string playerName, int playerKills = 0, int playerDeaths = 0)
        {
            Player newPlayer = new Player(playerId, this.players.Count, this, playerName);
            players.Add(newPlayer);
            RefreshPlayerNumbers();
            return newPlayer;
        }

        public void RefreshPlayerNumbers()
        {
            foreach(Player player in players)
            {
                player.SetTeamPlayerNumber(players.IndexOf(player) + 1);
            }
        }

        /*public int GetTeamKills()
        {
            int kills = 0;
            foreach (Player player in players)
                kills += player.kills;
            return kills;
        }*/

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Player : basicData, INotifyPropertyChanged
    {
        public Guid id { get; set; }
        public int teamPlayerNumber { get; set; }
        public Team team { get; set; }
        public string name { get; set; }

        public Player(Guid newId, int newTeamPlayerNumber, Team newTeam, string newName)
        {
            id = newId; teamPlayerNumber = newTeamPlayerNumber; team = newTeam; name = newName;
        }

        public void SetTeamPlayerNumber(int newTeamPlayerNumber)
        {
            teamPlayerNumber = newTeamPlayerNumber;
            NotifyPropertyChanged("teamPlayerNumber");
        }

        /*public void DecrementKills()
        {
            int newVal = kills-1;
            if (newVal < 0)
                newVal = kills;
            kills = newVal;
            NotifyPropertyChanged("kills");
        }
        public void IncrementKills()
        {
            kills += 1;
            NotifyPropertyChanged("kills");
        }

        public void DecrementDeaths()
        {
            int newVal = deaths-1;
            if (newVal < 0)
                newVal = deaths;
            deaths = newVal;
            NotifyPropertyChanged("deaths");
        }
        public void IncrementDeaths()
        {
            deaths += 1;
            NotifyPropertyChanged("deaths");
        }*/

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Match
    {
        public Guid id { get; set; }
        public int number { get; set; }
        public Dictionary<Guid, int> teamPlacements { get; set; }
        public Dictionary<Guid, PlayerData> playerData { get; set; }

        public Match(int matchNumber, Guid matchId, List<basicData> teams)
        {
            number = matchNumber; id = matchId;
            foreach(Team team in teams)
            {
                teamPlacements.Add(team.id, 0);
                foreach(Player player in team.players)
                {
                    playerData.Add(player.id, new PlayerData());
                }
            }
        }
    }

    public class PlayerData
    {
        public int kills { get; set; }
        public int deaths { get; set; }

        public PlayerData(int newKills = 0, int newDeaths = 0)
        {
            kills = newKills; deaths = newDeaths;
        }
    }

    public interface basicData
    {
        public Guid id { get; set; }
        public string name { get; set; }
    }

    public static class ListSearchHelper
    {
        public static basicData FindObjectById(this ObservableCollection<basicData> coll, Guid id)
        {
            foreach(basicData obj in coll)
            {
                if(obj.id == id)
                {
                    return obj;
                }
            }
            return null;
        }

        public static basicData FindObjectByName(this ObservableCollection<basicData> coll, string name)
        {
            foreach (basicData obj in coll)
            {
                if (obj.name == name)
                {
                    return obj;
                }
            }
            return null;
        }
    }
}
