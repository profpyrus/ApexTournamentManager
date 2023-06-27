using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.Model
{
    class PlayerData
    {
        public int kills { get; set; }
        public int deaths { get; set; }

        public PlayerData(int newKills = 0, int newDeaths = 0)
        {
            kills = newKills; deaths = newDeaths;
        }
    }
}
