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

        private double[] _marginsPlayerCards = { 0, 10, 10, 0 };

        public BlackjackGame()
        {
            InitializeComponent();

            Coins = new Coins();
            UpdateCoins();

            Game = new Blackjack(8);
            UpdateBTNDeal();
        }

        private void UpdateCoins()
        {
            LabelBalance.Content = $"Balance: $ {Coins.Amount}";
        }

        private void updateBet()
        {
            LabelBet.Content = $"Bet: $ {Game._currentBet}";
        }

        private void UpdateBTNDeal()
        {
            BTNDeal.IsEnabled = !(Game._currentBet == 0);
        }

        private void PlayerDrawCard()
        {
            // shared code
            Card temp = Game.DrawCard();

            Game._playerTotal += temp.Value;

            Image cardImg = new Image();
            cardImg.Width = 110;

            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(temp.Path, UriKind.Relative);
            src.EndInit();
            cardImg.Source = src;

            cardImg.Margin = new Thickness(_marginsPlayerCards[0], _marginsPlayerCards[1], _marginsPlayerCards[2], _marginsPlayerCards[3]);
            ContainerPlayerCards.Children.Add(cardImg);
            UpdatePlayerTotalLabel();

            // prepare margins for next img
            _marginsPlayerCards[0] = -90;
            _marginsPlayerCards[1] -= 30;
        }

        private void DealerDrawCard(bool isHidden)
        {
            // shared code
            Card temp = Game.DrawCard();

            Image cardImg = new Image();
            cardImg.Width = 110;

            BitmapImage src = new BitmapImage();
            src.BeginInit();
            if (isHidden)
            {
                src.UriSource = new Uri(Game._hiddenCardPath, UriKind.Relative);
                Game._hiddenDealerCard = temp;
            } else
            {
                src.UriSource = new Uri(temp.Path, UriKind.Relative);

                Game._dealerTotal += temp.Value;
            }
            src.EndInit();
            cardImg.Source = src;

            UpdateDealerTotalLabel();
            ContainerDealerCards.Children.Add(cardImg);
        }

        private void UpdatePlayerTotalLabel()
        {
            LabelPlayerTotal.Content = Game._playerTotal;
        }

        private void UpdateDealerTotalLabel()
        {
            LabelDealerTotal.Content = Game._dealerTotal;
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
            // here we start the game
            // deal cards to player and dealer

            // deal 4 cards in this order: player, dealer, player and a back card to dealer
            for (int i = 0; i < 4; i++)
            {
                if( i == 0 || i == 2)
                {
                    PlayerDrawCard();
                } else
                {
                    DealerDrawCard(i == 3);
                }
            }
        }

        private void BTNChip1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(1, Coins.Amount);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(1);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(5, Coins.Amount);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip5_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(5);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(10, Coins.Amount);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip10_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(10);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(25, Coins.Amount);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip25_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(25);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip50_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(50, Coins.Amount);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip50_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(50);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip100_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(100, Coins.Amount);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip100_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(100);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip250_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(250, Coins.Amount);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip250_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(250);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip500_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(500, Coins.Amount);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip500_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(500);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip1000_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(1000, Coins.Amount);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip1000_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(1000);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip5000_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(5000, Coins.Amount);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip5000_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(5000);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip10000_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.IncreaseBet(10000, Coins.Amount);
            updateBet();
            UpdateBTNDeal();
        }

        private void BTNChip10000_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Game.DecreaseBet(10000);
            updateBet();
            UpdateBTNDeal();
        }
    }
}
