using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class PlayerViewModel : ObservableObject
    {
        private protected Player _player;

        public Player Player {
            get
            {
                return _player;
            }
        }

        public RelayCommand RemovePlayer { get; set; }

        public string Name
        {
            get
            {
                return _player.name;
            }
            set
            {
                _player.name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public Guid teamId => _player.id;

        public PlayerViewModel(Player player)
        {
            _player = player;
        }
    }
}
