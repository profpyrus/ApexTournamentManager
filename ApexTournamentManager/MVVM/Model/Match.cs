using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.MVVM.Model
{
    class Match
    {
        public Guid id { get; set; }
        public int number { get; set; }
        public Dictionary<Guid, int> teamPlacements { get; set; }
        public Dictionary<Guid, PlayerData> playerData { get; set; }

        public Match(int matchNumber, Guid matchId, List<basicData> teams)
        {
            teamPlacements = new Dictionary<Guid, int>();
            playerData = new Dictionary<Guid, PlayerData>();

            number = matchNumber; id = matchId;
            foreach (Team team in teams)
            {
                teamPlacements.Add(team.id, 0);
                foreach (Player player in team.players)
                {
                    playerData.Add(player.id, new PlayerData());
                }
            }
        }
    }
}
