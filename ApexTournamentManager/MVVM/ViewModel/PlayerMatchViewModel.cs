using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class PlayerMatchViewModel : ObservableObject
    {
        private Match _match;
        private Player _player;

        public RelayCommand IncrementKills { get; set; }
		public RelayCommand DecrementKills { get; set; }
		public RelayCommand IncrementDeaths { get; set; }
		public RelayCommand DecrementDeaths { get; set; }

		public string Kills
        {
            get
            {
				PlayerData data;
				if (!_match.playerData.TryGetValue(_player.id, out data))
				{
					data = new PlayerData();
					_match.playerData[_player.id] = data;
				}
				return _match.playerData[_player.id].kills.ToString();
            }
        }

		public string Deaths
		{
			get
			{
				PlayerData data;
				if (!_match.playerData.TryGetValue(_player.id, out data))
				{
					data = new PlayerData();
					_match.playerData[_player.id] = data;
				}
				return _match.playerData[_player.id].deaths.ToString();
			}
		}

		public string Name
        {
            get
            {
                return _player.name;
            }
        }

        public PlayerMatchViewModel(Player player, Match match)
        {
            _player = player;
            _match = match;

			IncrementKills = new RelayCommand(o => { _match.playerData[_player.id].kills = 1; OnPropertyChanged(nameof(Kills)); });
			DecrementKills = new RelayCommand(o => { _match.playerData[_player.id].kills = -1; OnPropertyChanged(nameof(Kills)); });
			IncrementDeaths = new RelayCommand(o => { _match.playerData[_player.id].deaths = 1; OnPropertyChanged(nameof(Deaths)); });
			DecrementDeaths = new RelayCommand(o => { _match.playerData[_player.id].deaths = -1; OnPropertyChanged(nameof(Deaths)); });
		}
    }
}
