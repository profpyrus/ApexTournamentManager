using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.Model
{
    class PlayerData
    {
        private int _kills;
        private int _deaths;
        public int kills
        {
            get
            {
                return _kills;
            }
            set
            {
                int newKills = _kills + value;
                if(newKills < 0)
                    newKills = 0;
                _kills = newKills;
            }
        }
		public int deaths
		{
			get
			{
				return _deaths;
			}
			set
			{
				int newDeaths = _deaths + value;
				if (newDeaths < 0)
					newDeaths = 0;
				_deaths = newDeaths;
			}
		}

		public PlayerData(int newKills = 0, int newDeaths = 0)
        {
            kills = newKills; deaths = newDeaths;
        }
    }
}
