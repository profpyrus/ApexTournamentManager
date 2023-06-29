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
	internal class LeaderboardValueViewModel : ObservableObject
	{
        private readonly ObservableCollection<RankData> _data;
        public IEnumerable<RankData> Data { get { return _data; } }
        public string Name { get; }

        public LeaderboardValueViewModel(List<RankData> data, string name)
        {
            Name = name;
            _data = new ObservableCollection<RankData>();
            List<RankData> sortedData = data.OrderBy(o => o.ValueUnrounded).ToList();
            sortedData.Reverse();
            foreach (RankData dataPoint in sortedData)
            {
                int newRank = sortedData.IndexOf(dataPoint) + 1;
                if(newRank != 1 && dataPoint.ValueUnrounded == sortedData[sortedData.IndexOf(dataPoint) - 1].ValueUnrounded)
                    newRank = sortedData[sortedData.IndexOf(dataPoint) - 1]._rank;
                dataPoint._rank = newRank;
                _data.Add(dataPoint);
            }
        }
    }
}
