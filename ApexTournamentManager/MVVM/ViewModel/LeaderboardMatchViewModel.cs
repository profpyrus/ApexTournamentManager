using ApexTournamentManager.Core;
using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ApexTournamentManager.MVVM.ViewModel
{
	internal class LeaderboardMatchViewModel : ObservableObject
	{
        private readonly Session _session;
        private readonly Match _match;
        private ObservableCollection<LeaderboardEntityViewModel> _entities;
        public IEnumerable<LeaderboardEntityViewModel> Entities { get{ return _entities; } }
        public string Name { get; }

        public LeaderboardMatchViewModel(Match match, Session session, string name, LeaderboardViewModel vm)
        {
            Name = name;
            _session = session;
            _match = match;
            _entities = new ObservableCollection<LeaderboardEntityViewModel>();
            _entities.Add(new LeaderboardEntityViewModel(_session, _match, _session.GetAllPlayers(), vm));
            List<Team> teams = new List<Team>();
            foreach (basicData team in _session.teams)
            {
                teams.Add(team as Team);
            }

            _entities.Add(new LeaderboardEntityViewModel(_session, _match, teams, vm));
		}

        public LeaderboardMatchViewModel(Session session, string name, LeaderboardViewModel vm)
        {
            _session = session;
            Name = name;
            _entities = new ObservableCollection<LeaderboardEntityViewModel>();
            _entities.Add(new LeaderboardEntityViewModel(_session, _session.GetAllPlayers(), vm));
            List<Team> teams = new List<Team>();
            foreach (basicData team in _session.teams)
            {
                teams.Add(team as Team);
            }

            _entities.Add(new LeaderboardEntityViewModel(_session, teams, vm));
        }
	}
}
