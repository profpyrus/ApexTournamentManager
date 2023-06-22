using System;
using System.Collections.ObjectModel;
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
        public Session session { get; set; }

        public MainWindow()
        {
            session = new Session(1);
            Team team = new Team(1, "Team Bromance");
            team.players.Add(new Player(0, "Profpyrus"));
            team.players.Add(new Player(1, "Cyrpax"));
            session.teams.Add(team);
            InitializeComponent();
        }

        private void namebox_TextChanged(object sender, TextChangedEventArgs e)
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

            session.teams.Add(new Team(5, "Looser's Club"));
            ((Team)session.teams[0]).players.Add(new Player(3, "AnTique"));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine(JsonConvert.SerializeObject(session));
        }

        private void AddTeam(object sender, RoutedEventArgs e)
        {
            int newTeamId = session.teams.Count + 1;
            Team newTeam = new Team(newTeamId, "Team " + newTeamId);
            session.teams.Add(newTeam);
        }

        private void RemoveTeam(object sender, RoutedEventArgs e)
        {
            Object selectedItem = teamTabControl.SelectedItem as TabItem;
            //Trace.WriteLine(selectedItem.Header);
        }
        private void AddPlayer(object sender, RoutedEventArgs e)
        {
            int teamId = Convert.ToInt32(((Button)sender).Tag.ToString());
            ((Team)session.teams[teamId-1]).players.Add(new Player(1, "New Player"));
        }

        private void RemovePlayer(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_KeyEnterUpdate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox tBox = (TextBox)sender;
                DependencyProperty prop = TextBox.TextProperty;

                BindingExpression binding = BindingOperations.GetBindingExpression(tBox, prop);
                if (binding != null) { binding.UpdateSource(); }
            }
        }
    }
}