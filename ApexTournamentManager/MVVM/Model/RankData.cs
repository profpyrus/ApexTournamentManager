using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.Model
{
	internal class RankData
	{
        public int Rank { get; set; }
		public string Name { get; set; }
		public int Value { get; set; }

        public RankData(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}
