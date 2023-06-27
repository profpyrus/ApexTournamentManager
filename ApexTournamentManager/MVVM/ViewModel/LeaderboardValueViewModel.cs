using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.ViewModel
{
	internal class LeaderboardValueViewModel
	{
        private readonly LeaderboardData _data;
        public LeaderboardData Data { get { return _data; } }
        public string Name { get; }

        public LeaderboardValueViewModel(List<RankData> data, string name)
        {
            Name = name;
            List<RankData> sortedData = data.OrderBy(o => o.Value).ToList();
            foreach (RankData dataPoint in sortedData)
            {
                int newRank = sortedData.IndexOf(dataPoint) + 1;
                if(newRank != 1 && dataPoint.Value == sortedData[sortedData.IndexOf(dataPoint) - 1].Value)
                    newRank = sortedData[sortedData.IndexOf(dataPoint) - 1].Rank;
                dataPoint.Rank = newRank;
			}
			
                
            _data = new LeaderboardData(sortedData);
		}
    }
}
