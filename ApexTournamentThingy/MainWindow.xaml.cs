using System;
using System.Data;
using System.Globalization;
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
        private DataTable leaderboardDataTable;

        public MainWindow()
        {
            session = new Session(Guid.NewGuid());
            Team team = session.AddTeam(Guid.NewGuid(), "Team Bromance");
            session.AddPlayerToTeam(team, Guid.NewGuid(), "Profpyrus", 1, 2);
            session.AddPlayerToTeam(team, Guid.NewGuid(), "Cyrpax");
            InitializeComponent();
        }

        private void namebox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine(JsonConvert.SerializeObject(session));
        }

        #region Add/Remove Team/Player
        private void AddTeam(object sender, RoutedEventArgs e)
        {
            int newTeamNumber = session.teams.Count + 1;
            session.AddTeam(Guid.NewGuid(), "Team " + newTeamNumber);
        }

        private void RemoveTeam(object sender, RoutedEventArgs e)
        {
            Team selectedItem = teamTabControl.SelectedItem as Team;
            session.RemoveTeam(selectedItem);
        }
        private void AddPlayer(object sender, RoutedEventArgs e)
        {
            session.AddPlayerToTeam(((Guid)((Button)sender).Tag), Guid.NewGuid(), "New Player");
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
                        session.RemovePlayer(player);
                    }
                }
            }
        }
        #endregion

        /*#region Player Stat Modifications
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
        #endregion*/

        private void TextBox_KeyEnterUpdate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox tBox = (TextBox)sender;
                DependencyProperty prop = TextBox.TextProperty;

                BindingExpression binding = BindingOperations.GetBindingExpression(tBox, prop);
                if (binding != null) { binding.UpdateSource(); }
                foreach(Team team in session.teams)
                {
                    team.NotifyPropertyChanged("name");
                }
            }
        }

        private void RefreshLeaderboardsData(object sender, RoutedEventArgs e)
        {
            int matchmode = matchmodeBox.SelectedIndex;
            DataTable data = new DataTable();
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Placement";
            data.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Player";
            data.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Team";
            data.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Kills";
            data.Columns.Add(column);

            List<Player> players = session.GetAllPlayers();
            List<KeyValuePair<Guid, int>> killist = new List<KeyValuePair<Guid, int>>();
            foreach(Player player in players)
            {
                killist.Add(new KeyValuePair<Guid, int>(player.id, session.matches[0].playerData[player.id].kills));
            }

            killist.Sort();

            leaderboardDataTable = data;
        }
    }

    [ValueConversion(typeof(int), typeof(bool))]
    public class SelectionToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int selectionIndex = System.Convert.ToInt32(value as string);
            return (selectionIndex != 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, out resultDateTime))
            {
                return resultDateTime;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}