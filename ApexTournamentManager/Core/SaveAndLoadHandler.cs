﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ApexTournamentManager.MVVM.Model;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.CodeDom;
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;

namespace ApexTournamentManager.Core
{
	static class SaveAndLoadHandler
	{
		static string filter = "Apex Legends Tournament Manager Session Files(*.atms)|*.atms";
		static string defaultName = "New Session";
		static string defaultExt = "atms";

		public static string CreateSession()
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.FileName = defaultName;

			if ((bool)GetFileDialog(dialog))
			{
				string sessionName = dialog.SafeFileName.Substring(0, dialog.SafeFileName.Length - 5);
				Session session = new Session(Guid.NewGuid(), sessionName, dialog.FileName);

				SaveSession(session);

				return dialog.FileName;
			}
			else return null;
		}

		public static void SaveSessionAs(Session session)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.FileName = session.name;

			if ((bool)GetFileDialog(dialog))
			{
				string sessionName = dialog.SafeFileName.Substring(0, dialog.SafeFileName.Length - 5);

				SaveSession(session);
			}
		}

		public static void SaveSession(Session session)
		{
			File.WriteAllText(session.path, SerializeSession(session));
		}

		public static string OpenSessionFile()
        {
			OpenFileDialog dialog = new OpenFileDialog();

			if ((bool)GetFileDialog(dialog))
			{
				return dialog.FileName;
			}
			else return "";
        }

		public static Session OpenSession()
		{
			string path = OpenSessionFile();
			if (!string.IsNullOrEmpty(path))
			{
				string ssession = File.ReadAllText(path);
				return DeserializeSession(ssession, path);
			} else { return null; }
		}

		public static Session OpenSession(string path)
		{
			string ssession = File.ReadAllText(path);
			return DeserializeSession(ssession, path);
		}

		public static bool? GetFileDialog(FileDialog dialog)
		{
			dialog.Filter = filter;
			dialog.DefaultExt = defaultExt;
			return dialog.ShowDialog();
		}

		public static string SerializeSession(Session session)
		{
			return JsonConvert.SerializeObject(session,
				Formatting.Indented, new JsonSerializerSettings
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				});
		}

		public static Session DeserializeSession(string json, string path)
		{
			JObject jsession = JObject.Parse(json);

			List<Team> teams = new List<Team>();

			foreach (JToken jteam in jsession["teams"])
			{
				List<Player> players = new List<Player>();

				foreach(JToken jplayer in jteam["players"])
				{
					string pid = jplayer["id"].Value<string>();
					string pname = jplayer["name"].Value<string>();
					int teamPlayerNumber = jplayer["teamPlayerNumber"].Value<int>();
					players.Add(new Player(Guid.Parse(pid), teamPlayerNumber, pname));
				}

				string tid = jteam["id"].Value<string>();
				string tname = jteam["name"].Value<string>();
				int teamNumber = jteam["teamNumber"].Value<int>();
				teams.Add(new Team(Guid.Parse(tid), teamNumber, tname, players));
			}


			List<Match> matches = new List<Match>();

			foreach (JToken jmatch in jsession["matches"])
			{
				string mid = jmatch["id"].Value<string>();
				int mnumber = jmatch["number"].Value<int>();
				Dictionary<Guid, int> teamPlacements = JsonConvert.DeserializeObject<Dictionary<Guid, int>>(jmatch["teamPlacements"].ToString());
				Dictionary<Guid, JToken> jplayerData = JsonConvert.DeserializeObject<Dictionary<Guid, JToken>>(jmatch["playerData"].ToString());
				Dictionary<Guid, PlayerData> playerData = new Dictionary<Guid, PlayerData>();

                foreach(KeyValuePair<Guid, JToken> pair in jplayerData)
					playerData[pair.Key] = new PlayerData(pair.Value.Value<int>("kills"), pair.Value.Value<int>("deaths"));
                matches.Add(new Match(mnumber, Guid.Parse(mid), teamPlacements, playerData));
			}


			List<Point> killPoints = new List<Point>();

			foreach(JToken jkillpoint in jsession["killPoints"])
			{
				killPoints.Add(new Point(jkillpoint["atLeast"].Value<int>(), jkillpoint["value"].Value<int>()));
			}


			List<Point> placementPoints = new List<Point>();

			foreach (JToken jplacementpoint in jsession["placementPoints"])
			{
				placementPoints.Add(new Point(jplacementpoint["atLeast"].Value<int>(), jplacementpoint["value"].Value<int>()));
			}


			string sname = jsession["name"].Value<string>();
			string sid = jsession["id"].Value<string>();

			return new Session(Guid.Parse(sid), sname, path, teams, matches, killPoints, placementPoints);
		}
	}
}
