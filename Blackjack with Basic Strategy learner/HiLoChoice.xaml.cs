﻿using System;
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
    /// Interaction logic for HiLoChoice.xaml
    /// </summary>
    public partial class HiLoChoice : Page
    {
        public HiLoChoice()
        {
            InitializeComponent();
        }

        private void BTNCard_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.Navigate(new HiLoTrainerCard());
        }

        private void BTNGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_return_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.Navigate(new StartingMenu());
        }
    }
}
