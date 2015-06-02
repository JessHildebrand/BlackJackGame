//  Class:      Deck
//  Package:    BlackJackGame
//  Summary:    Creates a standard 52-card deck with the capability to shuffle and draw the next card

using System;
using System.Collections.Generic;
using BlackJackGame;

namespace BlackJackGame
{
    public class Deck
    {
        public static int NUMFACES = 13;
        public static int NUMSUITS = 4;

        public static string[] FACES = { "0", "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        public static string[] SUITS = { "♣", "♠", "♦", "♥" };

        private static Card RESUFFLECARD = new Card();

        private List<Card> cardStack;
        private int topCardPos;

        public Deck()
        {
            cardStack = createDeck();
            addResuffleCard();
            topCardPos = size() - 1;
        }

        public Deck(int numOfDecks)
        {
            cardStack = new List<Card>();

            for (int d = 0; d < numOfDecks; d++)
                cardStack.AddRange(createDeck());

            addResuffleCard();
            topCardPos = size() - 1;
        }

        private List<Card> createDeck()
        {
            String s, f;
            List<Card> deck = new List<Card>();

            for (int face = 1; face <= NUMFACES; face++)
            {
                for (int suit = 0; suit < NUMSUITS; suit++)
                {
                    f = FACES[face];
                    s = SUITS[suit];
                    deck.Add(new Card(f, s));
                }
            }
            return deck;
        }

        // non-standard card, that when drawn, the deck must be reshuffled
        public void addResuffleCard()
        {
            cardStack.Add(RESUFFLECARD);
        }

        public void shuffle()
        {
            Random rnd = new Random();
            for (var i = 0; i < numCardsLeft(); i++)
                swap(i, rnd.Next(i, numCardsLeft()));
        }

        private void swap(int i, int j)
        {
            var temp = cardStack[i];
            cardStack[i] = cardStack[j];
            cardStack[j] = temp;
        }

        public int size()
        {
            return cardStack.Count;
        }

        public int numCardsLeft()
        {
            if (topCardPos > 0)
                return topCardPos + 1;
            else
                return 0;
        }

        public Card drawNextCard()
        {
            if (topCardPos == 0)
                return null;
            return cardStack[topCardPos--];
        }

        public String toString()
        {
            return "Deck of size " + size();
        }
    }
}
