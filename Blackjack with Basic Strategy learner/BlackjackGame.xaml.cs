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

namespace Blackjack_with_Basic_Strategy_learner
{
    /// <summary>
    /// Interaction logic for BlackjackGame.xaml
    /// </summary>
    public partial class BlackjackGame : Page
    {
        public Blackjack Game { get; set; }
        public Coins Coins { get; set; }

        public BlackjackGame()
        {
            InitializeComponent();

            Coins = new Coins();
            UpdateCoins();

            Game = new Blackjack(8);
        }

        private void UpdateCoins()
        {
            LabelBalance.Content = $"Balance: $ {Coins.Amount}";
        }

        private void BTNChip1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNChip5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNChip10_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNChip25_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNChip50_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNChip100_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNChip250_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNChip500_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNChip1000_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNChip5000_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNChip10000_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNHit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNStand_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNDouble_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNSplit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
