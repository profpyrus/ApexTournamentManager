using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class TeamViewModel : ObservableObject
    {
        private protected Team _team;
        private protected PlayerViewModel _selectedPlayer;

        public PlayerViewModel SelectedPlayer {
            get
            {
                return _selectedPlayer;
            }
            set
            {
                if (value != null)
                    _selectedPlayer = value;
            }
        }
        public int PlayerCount { get { return _players.Count; } }

        public RelayCommand AddPlayerCommand { get; set; }
        public RelayCommand RemovePlayerCommand { get; set; }

        public string Name
        {
            get
            {
                return _team.name;
            }
            set
            {
                _team.name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public Guid teamId => _team.id;
        private protected ObservableCollection<PlayerViewModel> _players;

        public IEnumerable<PlayerViewModel> Players
        {
            get
            {
                return _players;
            }
        }

        public TeamViewModel(Team team)
        {
            _players = new ObservableCollection<PlayerViewModel>();
            _team = team;

            AddPlayerCommand = new RelayCommand(o => { team.AddPlayer(Guid.NewGuid()); UpdatePlayers(); });
            RemovePlayerCommand = new RelayCommand(o => { team.RemovePlayer(_selectedPlayer.Player); UpdatePlayers(_players.IndexOf(_selectedPlayer)); });

            UpdatePlayers(0);
        }

        void UpdatePlayers()
        {
            _players.Clear();
            foreach (Player player in _team.players)
            {
                _players.Add(new PlayerViewModel(player));
			}
			_selectedPlayer = _players.Last();
			OnPropertyChanged(nameof(SelectedPlayer));
			OnPropertyChanged(nameof(PlayerCount));
		}

		void UpdatePlayers(int index)
		{
			_players.Clear();
			foreach (Player player in _team.players)
			{
				_players.Add(new PlayerViewModel(player));
			}
			_selectedPlayer = _players.ElementAtOrLast(index);
			OnPropertyChanged(nameof(SelectedPlayer));
			OnPropertyChanged(nameof(PlayerCount));
		}

		void TeamVM_PropertyChanged(object obj, PropertyChangedEventArgs e)
        {
            UpdatePlayers();
        }
    }
}
