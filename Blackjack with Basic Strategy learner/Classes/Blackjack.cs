using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_with_Basic_Strategy_learner
{
    public class Blackjack
    {
        private const int _numberOfCardsInDeck = 52;
        public int DecksInPlay { get; set; }

        public Card[] _cards;
        public int CardsDrawn { get; set; }

        // card value totals
        public int _playerTotal = 0;
        public int _dealerTotal = 0;

        // hidden dealer card
        public Card _hiddenDealerCard = null;
        public string _hiddenCardPath = "/Images/GameAssets/cards/back.png";

        // betting vars
        public double _currentBet = 0;

        private bool _bettingAllowed = true;

        public Blackjack(int decksInPlay)
        {
            DecksInPlay = decksInPlay;

            _cards = new Card[_numberOfCardsInDeck * DecksInPlay];

            // initial setup of the game vars
            CreateDecks();
            ShuffleDeck();
            CardsDrawn = 0;
        }

        public void CreateDecks()
        {
            int cardSlot = 0;

            for (int i = 0; i < DecksInPlay; i++)
            {
                Deck deck = new Deck();

                for (int j = 0; j < deck._cards.Length; j++)
                {
                    _cards[cardSlot] = deck._cards[j];
                    cardSlot++;
                }
            }
        }

        public void ShuffleDeck()
        {
            // Fisher-Yates shuffle.
            Random r = new Random();

            for (int i = _cards.Length - 1; i > 0; --i)
            {
                int k = r.Next(i + 1);
                Card temp = _cards[i];
                _cards[i] = _cards[k];
                _cards[k] = temp;
            }
        }

        public Card DrawCard()
        {
            Card returnedCard = _cards[CardsDrawn];
            CardsDrawn++;

            return returnedCard;
        }

        // betting methods
        public void IncreaseBet(double amount, double balance)
        {
            if(_bettingAllowed)
            {
                if(_currentBet + amount >= balance)
                {
                    _currentBet = balance;
                    return;
                }

                _currentBet += amount;
            }
        }

        public void DecreaseBet(double amount)
        {
            if (_bettingAllowed)
            {
                if(_currentBet - amount <= 0)
                {
                    _currentBet = 0;
                    return;
                }

                _currentBet -= amount;
            }
        }
    }
}
