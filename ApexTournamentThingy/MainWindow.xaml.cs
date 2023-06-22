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

namespace ApexTournamentThingy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void team1namebox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Trace.WriteLine("Text changed");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (TabItem tab in teamTabControl.Items)
            {
                Grid item = LogicalTreeHelper.GetChildren(tab).OfType<Grid>().FirstOrDefault();
                List<TextBox> team1stuff = item.Children.OfType<TextBox>().ToList();
                foreach (TextBox box in team1stuff)
                {
                    Trace.WriteLine(box.Text);
                }
            }
            Trace.WriteLine("Test console output");
        }
    }
}