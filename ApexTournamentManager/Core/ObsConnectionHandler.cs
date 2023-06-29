using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexTournamentManager.MVVM.Model;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;

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
            List<SceneItemDetails> items = obs.GetSceneItemList("ATMs");
            foreach (SceneItemDetails item in items)
            {
                if (item.SourceKind == null && item.SourceName.Contains("ATMr"))
                {
                    InputSettings set = obs.GetInputSettings("ATMr1n");
                    set.Settings["text"] = "Und?";
                    obs.SetInputSettings(set);
                }
            }
        }
    }
}
