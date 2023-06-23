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
        public int playerCount { get; set; }
        public ObservableCollection<basicData> teams { get; set; }

        public Session(Guid newId)
        {
            id = newId; teams = new ObservableCollection<basicData>();
        }

        public void RemovePlayerById(Guid playerId)
        {
            Player player = GetPlayerById(playerId);
            foreach(Team team in teams)
            {
                team.players.Remove(player);
            }
        }

        public void AddPlayerToTeam(int teamId, Player player)
        {
        }

        public Player GetPlayerById(Guid searchId)
        {
            foreach(Team team in teams)
            {
                foreach(Player player in team.players)
                {
                    if(player.id == searchId)
                    {
                        return player;
                    }
                }
            }
            return null;
        }
    }
    public class Team : basicData
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public int wins { get; set; }
        public ObservableCollection<basicData> players { get; set; }

        public Team(Guid newId, string newName, int newWins = 0)
        {
            id = newId; name = newName; wins = newWins; players = new ObservableCollection<basicData>();
        }

        /*public int GetTeamKills()
        {
            int kills = 0;
            foreach (Player player in players)
                kills += player.kills;
            return kills;
        }*/
    }

    public class Player : basicData, INotifyPropertyChanged
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }

        public Player(Guid newId, string newName, int newKills = 0, int newDeaths = 0)
        {
            id = newId; name = newName; kills = newKills; deaths = newDeaths;
        }

        public void DecrementKills()
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
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
