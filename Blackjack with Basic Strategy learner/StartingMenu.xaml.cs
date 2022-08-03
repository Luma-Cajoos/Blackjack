﻿using System;
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
        public Coins Coins { get; set; }
        const string filepath = "./Data/coins.txt";

        public StartingMenu()
        {
            InitializeComponent();

            Coins = new Coins();
            CheckResetBTN();
            UpdateCoins();
        }
        private void SaveCoinsToFile()
        {
            File.WriteAllText(filepath, Coins.ToString());
        }
        private void CheckResetBTN()
        {
            BTNReset.IsEnabled = !(Coins.Amount > 10);
        }

        private void UpdateCoins()
        {
            CoinDisplay.Text = Coins.Amount.ToString();
            CheckResetBTN();
        }

        private void BTNReset_Click(object sender, RoutedEventArgs e)
        {
            Coins.ResetCoins();
            UpdateCoins();
        }

        private void BTNPlay_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.Navigate(new BlackjackGameSettings());
        }

        private void BTNLearn_Click(object sender, RoutedEventArgs e)
        {
            
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
