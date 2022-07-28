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

            } else
            {
                IMGCard.Source = new BitmapImage(new Uri(playCard.Path, UriKind.Relative));
                MessageBox.Show("deck is empty");
            }
        }

        private void InputCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                bool guessIsCorrect = Game.GuessRunningCount(int.Parse(InputCount.Text));

                if (guessIsCorrect)
                {
                    LabelGuessResult.Content = $"CORRECT";
                } else
                {
                    LabelGuessResult.Content = $"WRONG IT WAS {Game.RunningCount}";
                }

                InputCount.Text = "";
                UpdateCard();
            }
        }
    }
}
