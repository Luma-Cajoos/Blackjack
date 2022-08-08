using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
        private double[] _marginsPlayerSplitCards = { 0, 10, 10, 0 };
        Image _imageHiddenDealerCard = null;

        // split vars
        StackPanel SplitDeckPanel = null;
        Label SplitDeckLabel = null;

        public BlackjackGame(int decksInPlay)
        {
            InitializeComponent();

            Coins = new Coins();
            UpdateCoins();

            Game = new Blackjack(decksInPlay);
            UpdateBTNDeal();

            // disable action buttons
            ToggleActionButtons(false);

            // update cards left label
            UpdateLabelCardsLeft();
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

        private void UpdateLabelCardsLeft()
        {
            LabelCardsLeft.Text = $"Cards left in the shoe: {Game._cards.Length - Game.CardsDrawn}";
        }

        private void PlayerDrawCard(bool toSplitDeck)
        {
            // draw a card from the deck
            Card temp = Game.DrawCard();

            // create img for the card
            Image cardImg = new Image();
            cardImg.Width = 110;

            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(temp.Path, UriKind.Relative);
            src.EndInit();
            cardImg.Source = src;

            // split deck or not
            if (toSplitDeck)
            {
                cardImg.Margin = new Thickness(_marginsPlayerSplitCards[0], _marginsPlayerSplitCards[1], _marginsPlayerSplitCards[2], _marginsPlayerSplitCards[3]);
                SplitDeckPanel.Children.Add(cardImg);

                Game._playerCardsSplit.Add(temp);
                // update player card total
                Game.UpdateTotalsAndCheckForAce();
                UpdatePlayerTotalSplitLabel();

                // prepare margins for next img
                _marginsPlayerSplitCards[0] = -90;
                _marginsPlayerSplitCards[1] -= 30;
            } else
            {
                cardImg.Margin = new Thickness(_marginsPlayerCards[0], _marginsPlayerCards[1], _marginsPlayerCards[2], _marginsPlayerCards[3]);
                ContainerPlayerCards.Children.Add(cardImg);

                Game._playerCards.Add(temp);
                // update player card total
                Game.UpdateTotalsAndCheckForAce();
                UpdatePlayerTotalLabel();

                // prepare margins for next img
                _marginsPlayerCards[0] = -90;
                _marginsPlayerCards[1] -= 30;

                // update the cards left label
                UpdateLabelCardsLeft();
            }
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

            // update the cards left label
            UpdateLabelCardsLeft();
        }

        private void UpdatePlayerTotalLabel()
        {
            LabelPlayerTotal.Content = Game._playerTotal;
        }
        private void UpdatePlayerTotalSplitLabel()
        {
            SplitDeckLabel.Content = Game._playerTotalSplit;
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
            ContainerSplitDeck.Children.Clear(); 

            // reset split container
            ColDefSplit.Width = new GridLength(0);

            // change margin on first label
            RectFirstDeck.Margin = new Thickness(270, 0, 270, 0);

            // reset the labels
            LabelDealerTotal.Content = "0";
            LabelPlayerTotal.Content = "0";

            // toggle the buttons
            ToggleChipButtons(true);

            // reset playercard margins
            _marginsPlayerCards[0] = 0;
            _marginsPlayerCards[1] = 10;
            _marginsPlayerCards[2] = 10;
            _marginsPlayerCards[3] = 0;
            _marginsPlayerSplitCards[0] = 0;
            _marginsPlayerSplitCards[1] = 10;
            _marginsPlayerSplitCards[2] = 10;
            _marginsPlayerSplitCards[3] = 0;

            // reset the game
            Game.ResetGame();

            // reset the bet
            updateBet();
        }

        private async void EndGame(bool blackjackPayout)
        {
            // turn the hidden dealer card
            Game.TurnHiddenCard();
            UpdateDealerTotalLabel();
            _imageHiddenDealerCard.Source = new BitmapImage(new Uri(Game._hiddenDealerCard.Path, UriKind.Relative));
            await Task.Delay(500);

            // dealer takes cards until he he has 17 or higher
            while (Game._dealerTotal < 17)
            {
                DealerDrawCard(false);
                await Task.Delay(500);
            }

            if (Game._playerUsedSplit)
            {
                string resultSplit = Game.PlayerSplitHasWon();
                string result = Game.PlayerHasWon();

                // result split deck
                switch (resultSplit)
                {
                    case "win":
                        Coins.Amount += ((Game._currentBet/2) * 2);
                        break;
                    case "push":
                        Coins.Amount += (Game._currentBet/2);
                        break;
                    default:
                        break;
                }

                // result normal deck
                switch (result)
                {
                    case "win":
                        Coins.Amount += (Game._currentBet * 2);
                        break;
                    case "push":
                        Coins.Amount += Game._currentBet;
                        break;
                    default:
                        break;
                }

                LabelResult.Text = $"Your last round was a {result} in the left deck and a {resultSplit} in the right deck";
            } else if(!Game.PlayerChoseInsurance)
            {
                // get the result
                string result = Game.PlayerHasWon();

                if (blackjackPayout)
                {
                    double payout = Game._currentBet * 2.5;
                    Coins.Amount += payout;
                    MessageBox.Show("blackjack");
                }
                else
                {
                    switch (result)
                    {
                        case "win":
                            Coins.Amount += (Game._currentBet * 2);
                            break;
                        case "push":
                            Coins.Amount += Game._currentBet;
                            break;
                        default:
                            break;
                    }

                    LabelResult.Text = $"Your last round was a {result}";
                }
            }

            Coins.SaveCoins();
            UpdateCoins();
            // disable the action buttons before the delay
            ToggleActionButtons(false);
            // delay after each round
            await Task.Delay(2000);

            // clear cards
            ResetScreen();

            // check if shuffle is needed
            int cardsLeft = Game._cards.Length - Game.CardsDrawn;

            if(cardsLeft <= Game.ShuffleAtCards)
            {
                Game.CardsDrawn = 0;
                Game.ShuffleDeck();

                MessageBox.Show("deck shuffled");

            }
        }

        // event methods
        private void BTNHit_Click(object sender, RoutedEventArgs e)
        {
            BTNSplit.IsEnabled = false;
            BTNDouble.IsEnabled = false;

            if (Game._playerUsedSplit)
            {
                if(Game._activeDeck == 0)
                {
                    PlayerDrawCard(true);

                    if (Game._playerTotalSplit >= 21)
                    {
                        Game._activeDeck = 1;
                    }
                } else
                {
                    PlayerDrawCard(false);

                    if (Game._playerTotal >= 21)
                    {
                        EndGame(false);
                    }
                }
            } else
            {
                PlayerDrawCard(false);
            
                if (Game._playerTotal >= 21)
                {
                    EndGame(false);
                }
            }

        }

        private void BTNStand_Click(object sender, RoutedEventArgs e)
        {
            if (Game._playerUsedSplit && Game._activeDeck == 0)
            {
                Game._activeDeck = 1;
            } else
            {
                EndGame(false);
            }
        }

        private void BTNDouble_Click(object sender, RoutedEventArgs e)
        {
            // double the bet
            Coins.Amount -= Game._currentBet;
            Game._currentBet *= 2;
            updateBet();
            UpdateCoins();

            // draw a card
            PlayerDrawCard(false);

            // end this round
            EndGame(false);
        }

        private async void BTNSplit_Click(object sender, RoutedEventArgs e)
        {
            BTNDouble.IsEnabled = false;
            BTNSplit.IsEnabled = false;

            // setup split container
            CreateDeck();
            ColDefSplit.Width = new GridLength(1, GridUnitType.Star);

            // split the deck
            Game.SplitDeck();

            // create img for the split card
            // draw a card from the deck
            Card temp = Game._playerCardsSplit[0];

            // create img for the card
            Image cardImg = new Image();
            cardImg.Width = 110;

            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(temp.Path, UriKind.Relative);
            src.EndInit();
            cardImg.Source = src;

            cardImg.Margin = new Thickness(_marginsPlayerSplitCards[0], _marginsPlayerSplitCards[1], _marginsPlayerSplitCards[2], _marginsPlayerSplitCards[3]);
            SplitDeckPanel.Children.Add(cardImg);

            UpdatePlayerTotalSplitLabel();
            UpdatePlayerTotalLabel();

            // prepare margins for next img
            _marginsPlayerCards[1] += 30;
            _marginsPlayerSplitCards[0] = -90;
            _marginsPlayerSplitCards[1] -= 30;

            // remove second img from first deck
            ContainerPlayerCards.Children.RemoveAt(ContainerPlayerCards.Children.Count-1);

            // draw 2 cards, 1 to each deck
            await Task.Delay(500);
            PlayerDrawCard(true);
            await Task.Delay(500);
            PlayerDrawCard(false);

            // double the bet
            Coins.Amount -= Game._currentBet;
            Game._currentBet *= 2;
            updateBet();
            UpdateCoins();

            // check for blackjacks
            if (Game._playerTotalSplit == 21)
            {
                Game._activeDeck = 1;
            }
            
            if (Game._playerTotalSplit == 21 && Game._playerTotal == 21)
            {
                EndGame(false);
            }
        }

        private async void BTNDeal_Click(object sender, RoutedEventArgs e)
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
                if (i == 0 || i == 2)
                {
                    PlayerDrawCard(false);
                    await Task.Delay(500);
                }
                else
                {
                    DealerDrawCard(i == 3);
                    await Task.Delay(500);
                }
            }

            // does the player have blackjack after dealing
            if(Game._playerTotal == 21)
            {
                EndGame(true);
                return;
            }

            // insurance
            if (Game._dealerCards[0].IsAce)
            {
                // can the player afford insurance
                if (Game._currentBet / 2 <= Coins.Amount)
                {
                    // ask for insurance
                    if (MessageBox.Show("Do you want insurance", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        // add insurance
                        // add half the bet
                        Game.Insurance = Game._currentBet / 2;
                        Coins.Amount -= Game.Insurance;
                        Game.PlayerChoseInsurance = true;

                        Coins.SaveCoins();
                        UpdateCoins();
                    }
                }

                await Task.Delay(3000);

                // check for hidden 10
                if(Game._hiddenDealerCard.Value == 10)
                {
                    // did the player chose insurance
                    if (Game.PlayerChoseInsurance)
                    {
                        // pay the insurance back
                        Coins.Amount += (Game._currentBet + Game.Insurance);
                        
                    }

                    EndGame(false);
                }
            }
            
            // enable the action buttons
            ToggleActionButtons(true);

            // toggle the apropriate buttons based on your cards
            // if the cards are not equal, you cant split
            if (Game._playerCards[0].Value != Game._playerCards[1].Value)
            {
                BTNSplit.IsEnabled = false;
            }

            // if the bet cant be doubled, then disable split and double
            if (Game._currentBet > Coins.Amount)
            {
                BTNSplit.IsEnabled = false;
                BTNDouble.IsEnabled = false;
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

        private void BTN_return_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.Navigate(new BlackjackGameSettings());
        }

        private void CreateDeck()
        {
            // create stackpanel for cards
            StackPanel splitDeckContainer = new StackPanel();
            splitDeckContainer.Orientation = Orientation.Horizontal;
            splitDeckContainer.HorizontalAlignment = HorizontalAlignment.Center;
            splitDeckContainer.Margin = new Thickness(0, 20, 0, 0);

            SplitDeckPanel = splitDeckContainer;
            ContainerSplitDeck.Children.Add(splitDeckContainer);

            // create grid for label
            Grid labelGrid = new Grid();
            labelGrid.Margin = new Thickness(0, 10, 0, 0);

            // create rectangle
            Rectangle rectangle = new Rectangle()
            {
                Fill = Brushes.Black,
                Opacity = 0.5,
                Margin = new Thickness(135, 0, 135, 0)
            };

            labelGrid.Children.Add(rectangle);

            // create label
            Label labelSplitTotal = new Label();
            labelSplitTotal.HorizontalAlignment = HorizontalAlignment.Center;
            labelSplitTotal.Padding = new Thickness(0, 10, 0, 10);
            labelSplitTotal.FontSize = 20;
            labelSplitTotal.Foreground = Brushes.White;
            labelSplitTotal.Content = "0";

            SplitDeckLabel = labelSplitTotal;
            labelGrid.Children.Add(labelSplitTotal);

            ContainerSplitDeck.Children.Add(labelGrid);

            // change margin on first label
            RectFirstDeck.Margin = new Thickness(135, 0, 135, 0);
        }
    }
}
