using System;
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

namespace ApexTournamentManager.Core
{
	class SaveAndLoadHandler
	{
		public SaveAndLoadHandler()
		{
			
		}

		public string SerializeSession(Session session)
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(session,
				Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings
				{ 
					ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
				});
		}

		public Session DeserializeSession(string json)
		{
			Newtonsoft.Json.Linq.JObject jsession = Newtonsoft.Json.Linq.JObject.Parse(json);
			string test = jsession["name"].Value<string>();
			return null;
		}
	}
}
