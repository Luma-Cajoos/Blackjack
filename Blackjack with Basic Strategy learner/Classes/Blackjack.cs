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
        public List<Card> _playerCardsSplit = new List<Card>();
        public List<Card> _dealerCards = new List<Card>();

        public int _playerTotal = 0;
        public int _playerTotalSplit = 0;
        public int _dealerTotal = 0;

        // hidden dealer card
        public Card _hiddenDealerCard = null;
        public string _hiddenCardPath = "/Images/GameAssets/cards/back.png";

        // betting vars
        public double _currentBet = 0;

        public bool _bettingAllowed = true;

        // splitting vars
        public bool _playerUsedSplit = false;
        public int _activeDeck = 0;

        // when to shuffle deck again
        public const int _shuffleAtPercent = 20;
        public int ShuffleAtCards { get; set; }

        // insurance
        public double Insurance { get; set; } = 0;
        public bool PlayerChoseInsurance { get; set; } = false;

        // basic strategy trainer vars
        public string _actionChoice;

        public Blackjack(int decksInPlay)
        {
            DecksInPlay = decksInPlay;

            _cards = new Card[_numberOfCardsInDeck * DecksInPlay];

            // initial setup of the game vars
            CreateDecks();
            ShuffleDeck();
            CardsDrawn = 0;

            ShuffleAtCards = _cards.Length * _shuffleAtPercent / 100;
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

        public void SplitDeck()
        {
            _playerUsedSplit = true;

            // move latest card to split deck
            Card temp = _playerCards[_playerCards.Count-1];
            _playerCardsSplit.Add(temp);

            _playerCards.Remove(temp);

            // update the totals
            UpdateTotalsAndCheckForAce();
        }

        public string PlayerHasWon()
        {
            string win = "win";
            string loss = "loss";
            string equal = "push";

            if (_playerTotal <= 21 && _dealerTotal > 21)
            {
                return win;
            }
            else if (_playerTotal <= 21 && _dealerTotal < _playerTotal)
            {
                return win;
            }
            else if (_playerTotal == _dealerTotal && _playerTotal <= 21)
            {
                return equal;
            }

            return loss;
        }
        public string PlayerSplitHasWon()
        {
            string win = "win";
            string loss = "loss";
            string equal = "push";

            if (_playerTotalSplit <= 21 && _dealerTotal > 21)
            {
                return win;
            }
            else if (_playerTotalSplit <= 21 && _dealerTotal < _playerTotalSplit)
            {
                return win;
            }
            else if (_playerTotalSplit == _dealerTotal && _playerTotal <= 21)
            {
                return equal;
            }

            return loss;
        }

        public void ResetGame()
        {
            _dealerCards.Clear();
            _playerCards.Clear();
            _playerCardsSplit.Clear();
            _currentBet = 0;
            _dealerTotal = 0;
            _playerTotal = 0;
            _bettingAllowed = true;
            _playerUsedSplit = false;
            _activeDeck = 0;
            Insurance = 0;
            PlayerChoseInsurance = false;
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

            if (_playerUsedSplit)
            {
                // check for ace in dealer cards

                // count total
                UpdatePlayerSplitTotal();
                for (int i = 0; i < _playerCardsSplit.Count; i++)
                {
                    if (_playerCardsSplit[i].IsAce)
                    {
                        // check if total is over 21, if true total - 10
                        if (_playerTotalSplit > 21)
                        {
                            _playerTotalSplit -= 10;
                        }
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
        
        public void UpdatePlayerSplitTotal()
        {
            // reset for counting
            _playerTotalSplit = 0;

            for (int i = 0; i < _playerCardsSplit.Count; i++)
            {
                _playerTotalSplit += _playerCardsSplit[i].Value;
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

        // basic strategy trainer methods
        public bool ResultBasicStrategy()
        {
            string correctChoice = "";

            // check for splitting
            if(_playerCards.Count == 2 && (_playerCards[0].Value == _playerCards[1].Value))
            {
                bool isSplit = PairSplittingResult();

                if (isSplit)
                {
                    correctChoice = "split";
                } else
                {
                    correctChoice = HardTotalResult();
                }
            } else
            {
                correctChoice = HardTotalResult();
            }
            

            return correctChoice == _actionChoice;
        }

        private string HardTotalResult()
        {
            string correctChoice = "";

            // check for hard totals
            switch (_playerTotal)
            {
                case <= 8:
                    correctChoice = "hit";
                    break;
                case 9:
                    if (_dealerTotal == 2)
                    {
                        correctChoice = "hit";
                    }
                    else if (_dealerTotal <= 6)
                    {
                        correctChoice = "double";
                    }
                    else
                    {
                        correctChoice = "hit";
                    }
                    break;
                case 10:
                    if (_dealerTotal <= 9)
                    {
                        correctChoice = "double";
                    }
                    else
                    {
                        correctChoice = "hit";
                    }
                    break;
                case 11:
                    correctChoice = "double";
                    break;
                case 12:
                    if (_dealerTotal <= 3)
                    {
                        correctChoice = "hit";
                    }
                    else if (_dealerTotal <= 6)
                    {
                        correctChoice = "stand";
                    }
                    else
                    {
                        correctChoice = "hit";
                    }
                    break;
                case 13:
                case 14:
                case 15:
                case 16:
                    if (_dealerTotal <= 6)
                    {
                        correctChoice = "stand";
                    }
                    else
                    {
                        correctChoice = "hit";
                    }
                    break;
                case >= 17:
                    correctChoice = "stand";
                    break;
                default:
                    break;
            }


            return correctChoice;
        }

        private bool PairSplittingResult()
        {
            bool isSplit = false;

            // check for splitting
            switch (_playerCards[0].Value)
            {
                case 2:
                case 3:
                    if (_dealerTotal <= 7)
                    {
                        isSplit = true;
                    }
                    else
                    {
                        isSplit = false;
                    }
                    break;
                case 4:
                    if (_dealerTotal <= 4)
                    {
                        isSplit = false;
                    }
                    else if (_dealerTotal <= 6)
                    {
                        isSplit = true;
                    }
                    else
                    {
                        isSplit = false;
                    }
                    break;
                case 5:
                    isSplit = false;
                    break;
                case 6:
                    if (_dealerTotal <= 6)
                    {
                        isSplit = true;
                    }
                    else
                    {
                        isSplit = false;
                    }
                    break;
                case 7:
                    if (_dealerTotal <= 7)
                    {
                        isSplit = true;
                    }
                    else
                    {
                        isSplit = false;
                    }
                    break;
                case 8:
                    isSplit = true;
                    break;
                case 9:
                    if (_dealerTotal <= 6)
                    {
                        isSplit = true;
                    }
                    else if (_dealerTotal == 7)
                    {
                        isSplit = false;
                    }
                    else if (_dealerTotal <= 9)
                    {
                        isSplit = true;
                    }
                    else
                    {
                        isSplit = false;
                    }
                    break;
                case 10:
                    isSplit = false;
                    break;
                case >= 11:
                    isSplit = true;
                    break;
                default:
                    break;
            }


            return isSplit;
        }
    }
}
