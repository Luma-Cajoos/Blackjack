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

        private void updateBet()
        {
            LabelBet.Content = $"Bet: $ {Game._currentBet}";
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

        private void BTNDeal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNChip1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(1, Coins.Amount);
            updateBet();
        }

        private void BTNChip1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(1);
            updateBet();
        }

        private void BTNChip5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(5, Coins.Amount);
            updateBet();
        }

        private void BTNChip5_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(5);
            updateBet();
        }

        private void BTNChip10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(10, Coins.Amount);
            updateBet();
        }

        private void BTNChip10_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(10);
            updateBet();
        }

        private void BTNChip25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(25, Coins.Amount);
            updateBet();
        }

        private void BTNChip25_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(25);
            updateBet();
        }

        private void BTNChip50_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(50, Coins.Amount);
            updateBet();
        }

        private void BTNChip50_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(50);
            updateBet();
        }

        private void BTNChip100_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(100, Coins.Amount);
            updateBet();
        }

        private void BTNChip100_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(100);
            updateBet();
        }

        private void BTNChip250_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(250, Coins.Amount);
            updateBet();
        }

        private void BTNChip250_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(250);
            updateBet();
        }

        private void BTNChip500_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(500, Coins.Amount);
            updateBet();
        }

        private void BTNChip500_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(500);
            updateBet();
        }

        private void BTNChip1000_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(1000, Coins.Amount);
            updateBet();
        }

        private void BTNChip1000_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(1000);
            updateBet();
        }

        private void BTNChip5000_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(5000, Coins.Amount);
            updateBet();
        }

        private void BTNChip5000_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(5000);
            updateBet();
        }

        private void BTNChip10000_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(10000, Coins.Amount);
            updateBet();
        }

        private void BTNChip10000_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(10000);
            updateBet();
        }
    }
}
