using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.Model
{
    class Player : basicData, INotifyPropertyChanged
    {
        public Guid id { get; set; }
        public int teamPlayerNumber { get; set; }
        public Team team { get; set; }
        public string name { get; set; }

        public Player(Guid newId, int newTeamPlayerNumber, string newName, Team newTeam = null)
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
}
