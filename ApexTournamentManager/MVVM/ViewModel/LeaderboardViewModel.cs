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
	internal class LeaderboardViewModel : ObservableObject
	{
		private Session _session;

		private ObservableCollection<LeaderboardMatchViewModel> _matches;
		public IEnumerable<LeaderboardMatchViewModel> Matches { get { return _matches; } }


		public LeaderboardViewModel(Session session)
		{ 
			_session = session;
			_matches = new ObservableCollection<LeaderboardMatchViewModel>();
			_matches.Add(new LeaderboardMatchViewModel(_session, "All Matches"));
			foreach (Match match in _session.matches)
			{
				_matches.Add(new LeaderboardMatchViewModel(match, _session, "Match #" + match.number.ToString()));
			}
		}
	}
}
