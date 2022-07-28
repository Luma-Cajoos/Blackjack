using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_with_Basic_Strategy_learner
{
    public class HiLoTrainingGame
    {
        private const int _numberOfCardsInDeck = 52;
        public int DecksInPlay { get; set; }

        public Card[] _cards;
        private int CardsDrawn { get; set; }
        public int RunningCount { get; set; }

        public HiLoTrainingGame(int decksInPlay)
        {
            DecksInPlay = decksInPlay;
            
            _cards = new Card[_numberOfCardsInDeck * DecksInPlay];

            // initial setup of the game vars
            CreateDecks();
            ShuffleDeck();
            CardsDrawn = 0;
            RunningCount = 0;

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

        public void ResetGame()
        {
            CardsDrawn = 0;
            RunningCount = 0;
            ShuffleDeck();
        }

        public Card DrawCard()
        {
            if (CardsDrawn >= _cards.Length)
            {
                // return the card with back image and a name of resetCard
                return new Card("ResetCard", "/Images/GameAssets/cards/back.png", 0);
            }

            Card returnedCard = _cards[CardsDrawn];
            UpdateRunningCount(returnedCard.Value);
            CardsDrawn++;

            return returnedCard;
        }

        public void UpdateRunningCount(int cardValue)
        {
            if (cardValue >= 2 && cardValue <= 6)
            {
                RunningCount++;
            } else if (cardValue >= 10 && cardValue <= 11)
            {
                RunningCount--;
            }
        }

        public bool GuessRunningCount(int guess)
        {
            return guess == RunningCount;
        }
    }
}
