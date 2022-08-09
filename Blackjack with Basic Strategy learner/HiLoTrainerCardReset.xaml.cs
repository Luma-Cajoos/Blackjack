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
    /// Interaction logic for HiLoTrainerCardReset.xaml
    /// </summary>
    public partial class HiLoTrainerCardReset : Page
    {
        public TimeSpan Time { get; set; }
        public int Mistakes { get; set; }

        // file stuff
        public String Path { get; } = "./Data/HiLoCardPersonalBest.txt";

        public TimeSpan TimeBest { get; set; }
        public int MistakesBest { get; set; }

        public HiLoTrainerCardReset(TimeSpan time, int mistakes)
        {
            InitializeComponent();

            Time = time;
            Mistakes = mistakes;

            MistakesText.Text = $"Mistakes: {Mistakes}";

            if(time.TotalSeconds > 60)
            {
                TimerText.Text = $"Time: {time.Minutes} minutes and {time.Seconds} seconds.";
            } else
            {
                TimerText.Text = $"Time: {time.Seconds} seconds.";
            }

            GetPersonalBest();

            // when to save the new attempt
            if((Time < TimeBest && Mistakes <= MistakesBest) || Mistakes < MistakesBest || (Time == TimeBest && Mistakes < MistakesBest))
            {
                SavePersonalBest();
                GetPersonalBest();
            }

            UpdatePersonalBestTextFields();

        }

        private void GetPersonalBest()
        {
            string data = File.ReadAllText(Path);

            string[] dataArr = data.Split(',');

            MistakesBest = int.Parse(dataArr[2]);
            TimeBest = new TimeSpan(0, int.Parse(dataArr[0]), int.Parse(dataArr[1]));

        }

        private void UpdatePersonalBestTextFields()
        {
            TimerBestText.Text = $"Time: {TimeBest.Minutes} minutes and {TimeBest.Seconds} seconds.";
            MistakesBestText.Text = $"Mistakes: {MistakesBest}";
        }

        private void SavePersonalBest()
        {
            string personalBest = $"{Time.Minutes},{Time.Seconds},{Mistakes}";
            File.WriteAllText(Path, personalBest);
        }

        private void BTNMainMenu_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.Navigate(new StartingMenu());
        }

        private void BTNRestart_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.Navigate(new HiLoTrainerCard());
        }
    }
}
