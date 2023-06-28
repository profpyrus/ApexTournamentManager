using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.Model
{
	internal class RankData
	{
        public int _rank;
        public string Rank
        {
            get
            {
                return "#" + _rank.ToString();
            }
        }
		public string Name { get; set; }
		public double ValueRounded { get { return Math.Round(ValueUnrounded * 10) / 10; } }
        public double ValueUnrounded { get; set; }

        public RankData(string name, double value)
        {
            Name = name;
            ValueUnrounded = value;
        }
    }
}
