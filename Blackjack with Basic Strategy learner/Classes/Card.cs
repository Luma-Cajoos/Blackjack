using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_with_Basic_Strategy_learner
{
    public class Card
    {
        public int Value { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsAce { get; set; }

        public Card(string name, string path, int value, bool isAce)
        {
            Name = name;
            Path = path;
            Value = value;
            IsAce = isAce;
        }
    }
}
