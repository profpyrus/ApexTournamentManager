using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ApexTournamentThingy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Session session;

        public MainWindow()
        {
            InitializeComponent();
            session = new Session(1);
        }

        private void team1namebox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*Session data = new Session();
            data.id = 0;
            Team team = new Team();
            Player player = new Player();
            player.id = 1;
            player.name = "Profpyrus";
            player.kills = 2;
            player.deaths = 3;
            team.players.Add(player);
            team.id = 15;
            team.name = "Team Bromance";
            team.wins = 1;
            data.teams.Add(team);
            Trace.WriteLine(JsonConvert.SerializeObject(data));*/

            int playerCount = 0;
            teamTabControl.ApplyTemplate();
            foreach (TabItem tab in teamTabControl.Items)
            {
                Team team = new Team(tab.TabIndex, tab.GetTextboxValueByName("namebox"));
                for (int i = 1; i <= 3; i++)
                {
                    string name = tab.GetTextboxValueByName("player" + i + "box");
                    if (name != "")
                    {
                        team.players.Add(new Player(playerCount, name));
                        playerCount++;
                    }
                }
                session.teams.Add(team);
            }
            Trace.WriteLine(JsonConvert.SerializeObject(session));
        }
    }
}