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
    /// Interaction logic for HiLoTrainerCard.xaml
    /// </summary>
    public partial class HiLoTrainerCard : Page
    {
        public HiLoTrainingGame Game { get; set; }

        // timer
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool TimerStarted { get; set; } = false;

        // mistakes
        public int Mistakes { get; set; } = 0;

        public HiLoTrainerCard()
        {
            InitializeComponent();
            Game = new HiLoTrainingGame(1);
            UpdateCard();
        }

        public void UpdateCard()
        {
            Card playCard = Game.DrawCard();
            if(playCard.Name != "ResetCard")
            {
                IMGCard.Source = new BitmapImage(new Uri(playCard.Path, UriKind.Relative));
                TextBlockCardsLeft.Text = $"{52 - Game.CardsDrawn} cards left";
            } else
            {
                EndTime = DateTime.Now;
                TimeSpan time = EndTime - StartTime;
                App.ParentWindowRef.ParentFrame.Navigate(new HiLoTrainerCardReset(time, Mistakes));
            }
        }

        private void InputCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                if(InputCount.Text != "")
                {
                    if (!TimerStarted)
                    {
                        TimerStarted = true;
                        StartTime = DateTime.Now;
                    }

                    bool parsedCorrect = int.TryParse(InputCount.Text, out int guess);
                    if (parsedCorrect)
                    {
                        bool guessIsCorrect = Game.GuessRunningCount(guess);

                        if (guessIsCorrect)
                        {
                            LabelGuessResult.Content = $"CORRECT";
                        } else
                        {
                            Mistakes++;
                            LabelGuessResult.Content = $"WRONG IT WAS {Game.RunningCount}";
                        }
                        UpdateCard();

                    }
                    else
                    {
                        MessageBox.Show("Please enter a number");
                    }

                    InputCount.Text = "";
                } else
                {
                    MessageBox.Show("Please enter a number");
                }
            }
        }

        private void BTN_return_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.Navigate(new StartingMenu());
        }
    }
}
