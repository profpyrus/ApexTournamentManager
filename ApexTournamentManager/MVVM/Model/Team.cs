﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.Model
{
    class Team : basicData, INotifyPropertyChanged
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public int teamNumber { get; set; }
        public ObservableCollection<basicData> players { get; set; }

        public Team(Guid newId, int newTeamNumber, string newName)
        {
            id = newId; teamNumber = newTeamNumber; name = newName; players = new ObservableCollection<basicData>();
            AddPlayer(Guid.NewGuid());
        }

		public Team(Guid newId, int newTeamNumber, string newName, List<Player> newPlayers)
		{
			id = newId; teamNumber = newTeamNumber; name = newName; players = new ObservableCollection<basicData>(newPlayers);
            foreach (Player player in players)
                player.team = this;
		}

		public void SetTeamNumber(int newTeamNumber)
        {
            teamNumber = newTeamNumber;
            NotifyPropertyChanged("teamNumber");
        }

        public Player AddPlayer(Guid playerId, string playerName = "New Player")
        {
            Player newPlayer = new Player(playerId, this.players.Count, playerName, this);
            players.Add(newPlayer);
            RefreshPlayerNumbers();
            return newPlayer;
        }

        public void RemovePlayer(Player player)
        {
            players.Remove(player);
            RefreshPlayerNumbers();
        }

        public void RefreshPlayerNumbers()
        {
            foreach (Player player in players)
            {
                player.SetTeamPlayerNumber(players.IndexOf(player) + 1);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
