using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.Model
{
	internal class LeaderboardData
	{
		public IEnumerable<RankData> Ranks;

		public LeaderboardData(IEnumerable<RankData> ranks)
		{
			Ranks = ranks;	
		}
	}
}
