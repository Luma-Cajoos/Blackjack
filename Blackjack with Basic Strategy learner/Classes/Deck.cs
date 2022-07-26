﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_with_Basic_Strategy_learner
{
    public class Deck
    {
        private const int _numberOfCardsInDeck = 52;
        private string[] _numbers = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "j", "q", "k", "a" };
        private string[] _types = { "c", "d", "h", "s" };
        public Card[] _cards = new Card[_numberOfCardsInDeck];

        public Deck()
        {
            CreateDeck();

        }

        public void CreateDeck()
        {
            int counter = 0;
            for (int i = 0; i < _numbers.Length; i++)
            {
                for (int j = 0; j < _types.Length; j++)
                {
                    string path = "/Images/GameAssets/cards/";
                    int value;
                    bool isAce = false;

                    switch (_numbers[i])
                    {
                        case "j":
                            path += "jack";
                            value = 10;
                            break;
                        case "q":
                            path += "queen";
                            value = 10;
                            break;
                        case "k":
                            path += "king";
                            value = 10;
                            break;
                        case "a":
                            path += "ace";
                            value = 11;
                            isAce = true;
                            break;
                        default:
                            path += _numbers[i];
                            value = int.Parse( _numbers[i]);
                            break;
                    }

                    path += "_of_";

                    switch (_types[j])
                    {
                        case "c":
                            path += "clubs";
                            break;
                        case "s":
                            path += "spades";
                            break;
                        case "h":
                            path += "hearts";
                            break;
                        case "d":
                            path += "diamonds";
                            break;
                    }

                    path += ".png";

                    _cards[counter] = new Card($"{_numbers[i]}{_types[j]}", path, value, isAce);

                    counter++;
                }
            }
        }

        

    }
}
