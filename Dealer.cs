//  Class:      Dealer
//  Package:    BlackJackGame
//  Summary:    Creates a specific player called Dealer with the capability 
//              shuffling, dealing, and hitting

using System;
using System.Collections.Generic;
using BlackJackGame;
namespace BlackJackGame
{


    public class Dealer : AbstractPlayer
    {
        private Deck deckOfCards;

        public Dealer()
        {
            deckOfCards = new Deck();
        }

        public Dealer(int numberOfDecks)
        {
            deckOfCards = new Deck(numberOfDecks);
        }

        public Deck shuffle()
        {
            deckOfCards.shuffle();
            return deckOfCards;
        }

        public Card deal()
        {
            Card card;

            if (deckOfCards.size() == 0)
            {
                Console.WriteLine("Error: There are no more cards.");
                card = null;
            }
            else
            {
                card = deckOfCards.drawNextCard();
                while (card.isResuffleCard())
                {
                    Console.WriteLine("Resuffle Card has been drawn. All undealt cards will be re-shuffled.");
                    shuffle();
                    card = deckOfCards.drawNextCard();
                }
            }
            return card;
        }
    }
}
