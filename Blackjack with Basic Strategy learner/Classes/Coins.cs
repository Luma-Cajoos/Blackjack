using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_with_Basic_Strategy_learner
{
    public class Coins
    {
        public double Amount { get; set; }
        private const double _maxCoinsToReset = 10;
        private const string _filepath = "./Data/coins.txt";

        public Coins()
        {
            GetCoinsFromFile();
        }

        public void GetCoinsFromFile()
        {
            Amount = double.Parse(File.ReadAllText(_filepath));
        }

        public void SaveCoins()
        {
            File.WriteAllText(_filepath, Amount.ToString());
        }

        public void ResetCoins()
        {
            if(Amount < _maxCoinsToReset)
            {
                Amount = 500;
                SaveCoins();
            }
        }
    }
}
