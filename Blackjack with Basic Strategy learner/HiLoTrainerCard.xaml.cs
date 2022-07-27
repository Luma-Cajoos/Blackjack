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
        public HiLoTrainerCard()
        {
            InitializeComponent();
            Deck deck = new Deck();
        }

        private void InputCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                IMGCard.Source = new BitmapImage(new Uri("/Images/GameAssets/Cards/2_of_hearts.png", UriKind.Relative));
            }
        }
    }
}
