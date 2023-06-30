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

		private ObsConnectionHandler _obs;
		public bool IsConnected { get; private set; }

		public LeaderboardViewModel(Session session, ObsConnectionHandler obs)
		{ 
			_session = session;
			IsConnected = obs.IsConnected;
			_matches = new ObservableCollection<LeaderboardMatchViewModel>();
			_matches.Add(new LeaderboardMatchViewModel(_session, "All Matches", this));
			foreach (Match match in _session.matches)
			{
				_matches.Add(new LeaderboardMatchViewModel(match, _session, "Match #" + match.number.ToString(), this));
			}
			_obs = obs;
		}

		public void sendDataToObs(object sender, EventArgs e)
        {
			_obs.SendLeaderboardToObs((LeaderboardValueViewModel)sender);
		}
	}
}
