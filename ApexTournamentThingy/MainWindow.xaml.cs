using System;
using System.ComponentModel;
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
            session = new Session(Guid.NewGuid());
            Team team = new Team(Guid.NewGuid(), "Team Bromance");
            team.players.Add(new Player(Guid.NewGuid(), "Profpyrus", 1, 2));
            team.players.Add(new Player(Guid.NewGuid(), "Cyrpax"));
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

            session.teams.Add(new Team(Guid.NewGuid(), "Looser's Club"));
            ((Team)session.teams[0]).players.Add(new Player(Guid.NewGuid(), "AnTique"));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine(JsonConvert.SerializeObject(session));
        }

        #region Add/Remove Team/Player
        private void AddTeam(object sender, RoutedEventArgs e)
        {
            int newTeamId = session.teams.Count + 1;
            Team newTeam = new Team(Guid.NewGuid(), "Team " + newTeamId);
            session.teams.Add(newTeam);
        }

        private void RemoveTeam(object sender, RoutedEventArgs e)
        {
            Team selectedItem = teamTabControl.SelectedItem as Team;
            session.teams.Remove(selectedItem);
        }
        private void AddPlayer(object sender, RoutedEventArgs e)
        {
            ((Team)session.teams.FindObjectById((Guid)((Button)sender).Tag)).players.Add(new Player(Guid.NewGuid(), "New Player"));
        }

        private void RemovePlayer(object sender, RoutedEventArgs e)
        {
            DependencyObject button = sender as DependencyObject;
            DependencyObject grid = VisualTreeHelper.GetParent(button);
            for(int i = 0; i < VisualTreeHelper.GetChildrenCount(grid); i++)
            {
                ListBox listbox = VisualTreeHelper.GetChild(grid, i) as ListBox;
                if(listbox != null)
                {
                    List<Player> playersToDelete = new List<Player>();
                    foreach (Player player in listbox.SelectedItems)
                    {
                        playersToDelete.Add(player);
                    }
                    foreach(Player player in playersToDelete)
                    {
                        session.RemovePlayerById(player.id);
                    }
                }
            }
        }
        #endregion

        #region Player Stat Modifications
        private void IncrementKills(object sender, RoutedEventArgs e)
        {
            session.GetPlayerById((Guid)((Button)sender).Tag).IncrementKills();
        }
        private void DecrementKills(object sender, RoutedEventArgs e)
        {
            session.GetPlayerById((Guid)((Button)sender).Tag).DecrementKills();
        }
        private void IncrementDeaths(object sender, RoutedEventArgs e)
        {
            session.GetPlayerById((Guid)((Button)sender).Tag).IncrementDeaths();
        }
        private void DecrementDeaths(object sender, RoutedEventArgs e)
        {
            session.GetPlayerById((Guid)((Button)sender).Tag).DecrementDeaths();
        }
        #endregion

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