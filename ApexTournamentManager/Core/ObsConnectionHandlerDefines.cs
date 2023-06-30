using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.Core
{
    static class ObsConnectionHandlerDefines
    {
        public static string sceneName = "ATM";
        public static string itemNamePrefix = "ATMr";
        public static string rankKey = "r";
        public static string nameKey = "n";
        public static string pointKey = "p";
        public static string rankedByKey = "b";

        public static string textSourceKind = "text_gdiplus_v2";

		public static string ipPrefix = "ws://";
        public static string defaultIp = "localhost";
        public static string defaultPort = "4455";
    }
}
