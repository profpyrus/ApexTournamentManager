﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ApexTournamentManager.MVVM.Model;
using ApexTournamentManager.MVVM.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using defines = ApexTournamentManager.Core.ObsConnectionHandlerDefines;

namespace ApexTournamentManager.Core
{
    class ObsConnectionHandler : ObservableObject
    {
        OBSWebsocket obs;
        public bool IsConnected { get { return obs.IsConnected; } }

        public ObsConnectionHandler()
        {
            obs = new OBSWebsocket();
        }

        public bool Connect(string ip, string port, string password)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                obs.ConnectAsync(defines.ipPrefix + ip + ":" + port, password);
            });
            int tries = 0;
            while(!obs.IsConnected && tries < 5)
            {
                tries += 1;
                System.Threading.Thread.Sleep(1000);
            }
            return obs.IsConnected;
        }

        public void SendTestleaderboardToObs()
        {
            List<RankData> ranks = new List<RankData>();
            for(int i = 1; i <= defines.maximumRankCount; i++)
            {
                ranks.Add(new RankData(i + ". PLACE", i));
			}
			SendLeaderboardToObs(new LeaderboardValueViewModel(ranks, "TESTRANKING", true));
        }

        public void ClearLeaderboardToObs()
        {
			for (int i = 1; i <= defines.maximumRankCount; i++)
			{
				ClearObsRank(i);
			}
            ClearObsTextSource(defines.itemNamePrefix + defines.rankedByKey);
		}

        public void SendLeaderboardToObs(LeaderboardValueViewModel vm)
        {
            List<RankData> data = vm.Data.ToList();
            int i = 0;
			WriteToObsTextSource(vm.Name, defines.itemNamePrefix + defines.rankedByKey);
			foreach (RankData rank in data)
            {
                i += 1;
                int index = data.IndexOf(rank) + 1;
                WriteToObsRank(index, rank.Rank, rank.Name, rank.ValueRounded);
            }
            for(; i <= defines.maximumRankCount;)
            {
                i += 1;
                ClearObsRank(i);
            }
        }

        public void WriteToObsRank(int index, string rank, string name, double points)
        {
            string itemPrefix = defines.itemNamePrefix + index.ToString();
            WriteToObsTextSource("#" + rank, itemPrefix + defines.rankKey);
            WriteToObsTextSource(name, itemPrefix + defines.nameKey);
            WriteToObsTextSource(points.ToString(), itemPrefix + defines.pointKey);
        }

        public void ClearObsRank(int ind)
        {
            string itemPrefix = defines.itemNamePrefix + ind;
            WriteToObsTextSource("", itemPrefix + defines.rankKey);
            WriteToObsTextSource("", itemPrefix + defines.nameKey);
            WriteToObsTextSource("", itemPrefix + defines.pointKey);
        }

        public void WriteToObsTextSource(string text, string name)
        {
            try
            {
                InputSettings set = obs.GetInputSettings(name);
                set.Settings["text"] = text;
                obs.SetInputSettings(set);
            }
            catch (OBSWebsocketDotNet.ErrorResponseException)
            {
            }
        }

		public void ClearObsTextSource(string name)
		{
			try
			{
				InputSettings set = obs.GetInputSettings(name);
				set.Settings["text"] = "";
				obs.SetInputSettings(set);
			}
			catch (OBSWebsocketDotNet.ErrorResponseException) { }
		}

        public void CreateAllRankTextsources(string template, string scene)
        {
            try
			{
				InputSettings set = obs.GetInputSettings(template);
                JObject defaultSet = obs.GetInputDefaultSettings(defines.textSourceKind);
                string name = "";
				int i = defines.maximumRankCount;
                try
				{
                    name = defines.itemNamePrefix + defines.rankedByKey;
                    set.Settings["InputName"] = name;
					obs.CreateInput(scene, name, defines.textSourceKind, set.Settings, true);
				}
                catch (OBSWebsocketDotNet.ErrorResponseException) { }
				while (i > 0)
				{
                    string itemNamePrefix = defines.itemNamePrefix + i.ToString();
					name = itemNamePrefix + defines.nameKey;
					set.Settings["InputName"] = name;
					try
					{
						obs.CreateInput(scene, name, defines.textSourceKind, set.Settings, true);
					}
                    catch (OBSWebsocketDotNet.ErrorResponseException) { }

					name = itemNamePrefix + defines.rankKey;
					set.Settings["InputName"] = name;
					try
					{
						obs.CreateInput(scene, name, defines.textSourceKind, set.Settings, true);
					}
					catch (OBSWebsocketDotNet.ErrorResponseException) { }

					name = itemNamePrefix + defines.pointKey;
					set.Settings["InputName"] = name;
					try
					{
						obs.CreateInput(scene, name, defines.textSourceKind, set.Settings, true);
					}
					catch (OBSWebsocketDotNet.ErrorResponseException) { }

                    i -= 1;
				}
                SendTestleaderboardToObs();
			}
            catch (OBSWebsocketDotNet.ErrorResponseException) { }
        }
	}
}
