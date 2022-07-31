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
        Image _imageHiddenDealerCard = null;

        public BlackjackGame()
        {
            InitializeComponent();

            Coins = new Coins();
            UpdateCoins();

            Game = new Blackjack(8);
            UpdateBTNDeal();

            // disable action buttons
            ToggleActionButtons(false);
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
            // draw a card from the deck
            Card temp = Game.DrawCard();

            Game._playerCards.Add(temp);

            // create img for the card
            Image cardImg = new Image();
            cardImg.Width = 110;

            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(temp.Path, UriKind.Relative);
            src.EndInit();
            cardImg.Source = src;

            cardImg.Margin = new Thickness(_marginsPlayerCards[0], _marginsPlayerCards[1], _marginsPlayerCards[2], _marginsPlayerCards[3]);
            ContainerPlayerCards.Children.Add(cardImg);

            // update player card total
            Game.UpdateTotalsAndCheckForAce();
            UpdatePlayerTotalLabel();

            // prepare margins for next img
            _marginsPlayerCards[0] = -90;
            _marginsPlayerCards[1] -= 30;
        }

        private void DealerDrawCard(bool isHidden)
        {
            // draw a card from the decl
            Card temp = Game.DrawCard();

            // create img
            Image cardImg = new Image();
            cardImg.Width = 110;

            BitmapImage src = new BitmapImage();
            src.BeginInit();

            // check for if the card needs to be hidden
            if (isHidden)
            {
                src.UriSource = new Uri(Game._hiddenCardPath, UriKind.Relative);
                cardImg.Name = "ImageHiddenCard";
                Game._hiddenDealerCard = temp;
            } else
            {
                src.UriSource = new Uri(temp.Path, UriKind.Relative);

                Game._dealerCards.Add(temp);
            }
            src.EndInit();
            cardImg.Source = src;

            if (isHidden)
            {
                _imageHiddenDealerCard = cardImg;
            }

            ContainerDealerCards.Children.Add(cardImg);


            // update player card total
            Game.UpdateTotalsAndCheckForAce();
            UpdateDealerTotalLabel();
        }

        private void UpdatePlayerTotalLabel()
        {
            LabelPlayerTotal.Content = Game._playerTotal;
        }

        private void UpdateDealerTotalLabel()
        {
            LabelDealerTotal.Content = Game._dealerTotal;
        }

        // toggle methods for whether the buttons need to be disabled
        private void ToggleActionButtons(bool enabled)
        {
            BTNHit.IsEnabled = enabled;
            BTNStand.IsEnabled = enabled;
            BTNSplit.IsEnabled = enabled;
            BTNDouble.IsEnabled = enabled;
        }

        private void ToggleChipButtons(bool enabled)
        {
            BTNChip1.IsEnabled = enabled;
            BTNChip5.IsEnabled = enabled;
            BTNChip10.IsEnabled = enabled;
            BTNChip25.IsEnabled = enabled;
            BTNChip50.IsEnabled = enabled;
            BTNChip100.IsEnabled = enabled;
            BTNChip250.IsEnabled = enabled;
            BTNChip500.IsEnabled = enabled;
            BTNChip1000.IsEnabled = enabled;
            BTNChip5000.IsEnabled = enabled;
            BTNChip10000.IsEnabled = enabled;
        }

        private void ResetScreen()
        {
            // clear the card containers
            ContainerPlayerCards.Children.Clear();
            ContainerDealerCards.Children.Clear();

            // reset the labels
            LabelDealerTotal.Content = "0";
            LabelPlayerTotal.Content = "0";

            // toggle the buttons
            ToggleActionButtons(false);
            ToggleChipButtons(true);

            // reset playercard margins
            _marginsPlayerCards[0] = 0;
            _marginsPlayerCards[1] = 10;
            _marginsPlayerCards[2] = 10;
            _marginsPlayerCards[3] = 0;

            // reset the game
            Game.ResetGame();

            // reset the bet
            updateBet();

        }

        private void EndGame(bool blackjackPayout)
        {
            // turn the hidden dealer card
            Game.TurnHiddenCard();
            UpdateDealerTotalLabel();
            _imageHiddenDealerCard.Source = new BitmapImage(new Uri(Game._hiddenDealerCard.Path, UriKind.Relative));

            // dealer takes cards until he he has 17 or higher
            while (Game._dealerTotal < 17)
            {
                DealerDrawCard(false);
            }

            // get the result
            string result = Game.PlayerHasWon();

            if (blackjackPayout)
            {
                Coins.Amount += (Game._currentBet * 2.5);
                MessageBox.Show("blackjack");
            } else
            {
                switch (result)
                {
                    case "win":
                        Coins.Amount += (Game._currentBet*2);
                        break;
                    case "push":
                        Coins.Amount += Game._currentBet;
                        break;
                    default:
                        break;
                }
                
                MessageBox.Show(result);
            }

            Coins.SaveCoins();
            UpdateCoins();

            // clear cards
            ResetScreen();
        }

        // event methods
        private void BTNHit_Click(object sender, RoutedEventArgs e)
        {
            PlayerDrawCard();

            if(Game._playerTotal >= 21)
            {
                EndGame(false);
            }
        }

        private void BTNStand_Click(object sender, RoutedEventArgs e)
        {
            EndGame(false);
        }

        private void BTNDouble_Click(object sender, RoutedEventArgs e)
        {
            Game._currentBet *= 2;
            updateBet();
            PlayerDrawCard();
            EndGame(false);
        }

        private void BTNSplit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNDeal_Click(object sender, RoutedEventArgs e)
        {
            // disable the chip buttons and deal button
            ToggleChipButtons(false);
            BTNDeal.IsEnabled = false;

            Game._bettingAllowed = false;

            // take the bet from the balance
            Coins.Amount -= Game._currentBet;
            Coins.SaveCoins();
            UpdateCoins();
            

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
            // enable the action buttons
            ToggleActionButtons(true);

            // does the player have blackjack after dealing
            if(Game._playerTotal == 21)
            {
                EndGame(true);
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
