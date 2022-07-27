using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for StartingMenu.xaml
    /// </summary>
    public partial class StartingMenu : Page
    {
        public double Coins { get; set; }
        const string filepath = "./Data/coins.txt";

        public StartingMenu()
        {
            InitializeComponent();
            GetCoinsFromFile();
            CheckResetBTN();
        }

        private void GetCoinsFromFile()
        {
            UpdateCoins(double.Parse(File.ReadAllText(filepath)));
        }
        private void SaveCoinsToFile()
        {
            File.WriteAllText(filepath, Coins.ToString());
        }

        private void IncrementCoins(double increment)
        {
            double result = Coins + increment;
            UpdateCoins(result);
        }

        private void DecrementCoins(double decrement)
        {
            double result = Coins - decrement;
            UpdateCoins(result);

        }
        private void CheckResetBTN()
        {
            BTNReset.IsEnabled = !(Coins > 10);
        }

        private void UpdateCoins(double coins)
        {
            Coins = coins;
            CoinDisplay.Text = Coins.ToString();
            CheckResetBTN();
            SaveCoinsToFile();
        }

        private void BTNReset_Click(object sender, RoutedEventArgs e)
        {
            UpdateCoins(500);
        }

        private void BTNPlay_Click(object sender, RoutedEventArgs e)
        {
            IncrementCoins(100);
        }

        private void BTNLearn_Click(object sender, RoutedEventArgs e)
        {
            DecrementCoins(100);
        }

        private void BTNHowToPlay_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.Navigate(new HowToPlay());
        }

        private void BTNHiLoTraining_Click(object sender, RoutedEventArgs e)
        {

            App.ParentWindowRef.ParentFrame.Navigate(new HiLoChoice());
        }
    }
}
