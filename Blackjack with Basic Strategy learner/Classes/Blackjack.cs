using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_with_Basic_Strategy_learner
{
    public class Blackjack
    {
        // deck setup
        private const int _numberOfCardsInDeck = 52;
        public int DecksInPlay { get; set; }

        public Card[] _cards;
        public int CardsDrawn { get; set; }

        // card value totals
        public List<Card> _playerCards = new List<Card>();
        public List<Card> _dealerCards = new List<Card>();

        public int _playerTotal = 0;
        public int _dealerTotal = 0;

        // hidden dealer card
        public Card _hiddenDealerCard = null;
        public string _hiddenCardPath = "/Images/GameAssets/cards/back.png";

        // betting vars
        public double _currentBet = 0;

        public bool _bettingAllowed = true;

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

        public void TurnHiddenCard()
        {
            _dealerCards.Add(_hiddenDealerCard);
            UpdateTotalsAndCheckForAce();
        }

        public string PlayerHasWon()
        {
            string win = "win";
            string loss = "loss";
            string equal = "push";

            if(_playerTotal <= 21 && _dealerTotal > 21)
            {
                return win;
            } else if (_playerTotal <= 21 && _dealerTotal < _playerTotal)
            {
                return win;
            } else if(_playerTotal == _dealerTotal)
            {
                return equal;
            }

            return loss;
        }

        public void ResetGame()
        {
            _dealerCards.Clear();
            _playerCards.Clear();
            _currentBet = 0;
            _dealerTotal = 0;
            _playerTotal = 0;
            _bettingAllowed = true;
        }

        public void UpdateTotalsAndCheckForAce()
        {
            // check for ace in player cards

            // count total
            UpdatePlayerTotal();
            for (int i = 0; i < _playerCards.Count; i++)
            {
                if (_playerCards[i].IsAce)
                {
                    // check if total is over 21, if true total - 10
                    if(_playerTotal > 21)
                    {
                        _playerTotal -= 10;
                    }
                }
            }
            
            // check for ace in dealer cards

            // count total
            UpdateDealerTotal();
            for (int i = 0; i < _dealerCards.Count; i++)
            {
                if (_dealerCards[i].IsAce)
                {
                    // check if total is over 21, if true total - 10
                    if (_dealerTotal > 21)
                    {
                        _dealerTotal -= 10;
                    }
                }
            }
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

        // card totals methods
        public void UpdatePlayerTotal()
        {
            // reset for counting
            _playerTotal = 0;

            for (int i = 0; i < _playerCards.Count; i++)
            {
                _playerTotal += _playerCards[i].Value;
            }
        }
        
        public void UpdateDealerTotal()
        {
            // reset for counting
            _dealerTotal = 0;

            for (int i = 0; i < _dealerCards.Count; i++)
            {
                _dealerTotal += _dealerCards[i].Value;
            }
        }
    }
}
