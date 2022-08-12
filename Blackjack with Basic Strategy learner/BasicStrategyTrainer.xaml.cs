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
    /// Interaction logic for BasicStrategyTrainer.xaml
    /// </summary>
    public partial class BasicStrategyTrainer : Page
    {
        public Blackjack Game { get; set; }

        private double[] _marginsPlayerCards = { 0, 10, 10, 0 };
        private double[] _marginsPlayerSplitCards = { 0, 10, 10, 0 };
        Image _imageHiddenDealerCard = null;

        // split vars
        StackPanel SplitDeckPanel = null;
        Label SplitDeckLabel = null;

        // result
        public bool _actionIsCorrect;
        public BasicStrategyTrainer()
        {
            InitializeComponent();

            Game = new Blackjack(8);

            // disable action buttons
            ToggleActionButtons(false);

            // update cards left label
            UpdateLabelCardsLeft();

            // deal first round
            Deal();
        }

        private async void Deal()
        {
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
            if (Game._playerTotal == 21)
            {
                EndGame(true);
                return;
            }

            // insurance
            if (Game._dealerCards[0].IsAce)
            {
                
                    // ask for insurance
                    if (MessageBox.Show("Do you want insurance", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        // add insurance
                        Game.PlayerChoseInsurance = true;

                    }

                await Task.Delay(3000);

                // check for hidden 10
                if (Game._hiddenDealerCard.Value == 10)
                {
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
        }

        private void UpdateLabelCardsLeft()
        {
            LabelCardsLeft.Text = $"Cards left in the shoe: {Game._cards.Length - Game.CardsDrawn}";
        }

        private void UpdateLabelResult()
        {
            string result = "Your guess was ";

            if (_actionIsCorrect)
            {
                result += "correct";
            } else
            {
                result += "not correct";
            }

            LabelResult.Text = result;
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
            }
            else
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
            cardImg.Margin = new Thickness(5, 10, 0, 0);

            BitmapImage src = new BitmapImage();
            src.BeginInit();

            // check for if the card needs to be hidden
            if (isHidden)
            {
                src.UriSource = new Uri(Game._hiddenCardPath, UriKind.Relative);
                cardImg.Name = "ImageHiddenCard";
                Game._hiddenDealerCard = temp;
            }
            else
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


            // disable the action buttons before the delay
            ToggleActionButtons(false);
            // delay after each round
            await Task.Delay(2000);

            // clear cards
            ResetScreen();

            // check if shuffle is needed
            int cardsLeft = Game._cards.Length - Game.CardsDrawn;

            if (cardsLeft <= Game.ShuffleAtCards)
            {
                Game.CardsDrawn = 0;
                Game.ShuffleDeck();

                MessageBox.Show("deck shuffled");

            }

            // deal new round
            Deal();
        }

        // event methods
        private void BTNHit_Click(object sender, RoutedEventArgs e)
        {
            BTNSplit.IsEnabled = false;
            BTNDouble.IsEnabled = false;

            Game._actionChoice = "hit";

            _actionIsCorrect = Game.ResultBasicStrategy();
            UpdateLabelResult();

            if (Game._playerUsedSplit)
            {
                if (Game._activeDeck == 0)
                {
                    PlayerDrawCard(true);

                    if (Game._playerTotalSplit >= 21)
                    {
                        Game._activeDeck = 1;
                    }
                }
                else
                {
                    PlayerDrawCard(false);

                    if (Game._playerTotal >= 21)
                    {
                        EndGame(false);
                    }
                }
            }
            else
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
            Game._actionChoice = "stand";

            _actionIsCorrect = Game.ResultBasicStrategy();
            UpdateLabelResult();

            if (Game._playerUsedSplit && Game._activeDeck == 0)
            {
                Game._activeDeck = 1;
            }
            else
            {
                EndGame(false);
            }
        }

        private void BTNDouble_Click(object sender, RoutedEventArgs e)
        {
            Game._actionChoice = "double";

            _actionIsCorrect = Game.ResultBasicStrategy();
            UpdateLabelResult();

            // draw a card
            PlayerDrawCard(false);

            // end this round
            EndGame(false);
        }

        private async void BTNSplit_Click(object sender, RoutedEventArgs e)
        {
            BTNDouble.IsEnabled = false;
            BTNSplit.IsEnabled = false;

            Game._actionChoice = "split";

            _actionIsCorrect = Game.ResultBasicStrategy();
            UpdateLabelResult();

            EndGame(false);
        }


        private void BTN_return_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.Navigate(new StartingMenu());
        }
    }
}
