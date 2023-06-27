using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.ViewModel
{
    class TeamViewModel : ObservableObject
    {
        private protected Team _team;

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
        public IEnumerable<basicData> players => _team.players;

        public TeamViewModel(Team team)
        {
            _team = team;
        }
    }
}
