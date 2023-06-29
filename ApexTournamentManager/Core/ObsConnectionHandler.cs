using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexTournamentManager.MVVM.Model;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using defines = ApexTournamentManager.Core.ObsConnectionHandlerDefines;

namespace ApexTournamentManager.Core
{
    class ObsConnectionHandler
    {
        OBSWebsocket obs;

        public ObsConnectionHandler()
        {
            obs = new OBSWebsocket();
        }

        public bool Connect()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                obs.ConnectAsync("ws://localhost:4455", "");
            });
            int tries = 0;
            while(!obs.IsConnected && tries < 5)
            {
                tries += 1;
                System.Threading.Thread.Sleep(1000);
            }
            return obs.IsConnected;
        }

        public void SendLeaderboardToObs(IEnumerable<RankData> data)
        {
            InputSettings set = obs.GetInputSettings("ATMr1n");
            set.Settings["text"] = data.First().Name;
            obs.SetInputSettings(set);
            set = obs.GetInputSettings("ATMr1r");
            set.Settings["text"] = "#" + data.First().Rank;
            obs.SetInputSettings(set);
        }
    }
}
