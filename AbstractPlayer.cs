//  Class:      AbstractPlayer
//  Package:    BlackJackGame
//  Summary:    Abstract class for all players of the games (including dealer). Allows player to add card, 
//              show card, and get card value

using System;
using System.Collections.Generic;
using System.Text;


namespace BlackJackGame
{
    public abstract class AbstractPlayer
    {
        private List<Card> hand;

        public AbstractPlayer()
        {
            hand = new List<Card>();
        }

        public void addCard(Card c)
        {
            hand.Add(c);
        }

        public Card showCard()
        {
            if (hand.Count > 0)
                return hand[0];
            else
                return null;
        }

        public List<Card> getHand()
        {
            return hand;
        }

        public void resetHand()
        {
            hand.Clear();
        }

        public int getHandSize()
        {
            return hand.Count;
        }

        public Card getLastCard()
        {
            if (hand.Count < 1)
                return null;

            return hand[hand.Count - 1];
        }

        public int getValueOfHand()
        {
            int value = 0;
            for (int i = 0; i < hand.Count; i++)
                value += hand[i].getValue();

            // check if there is an Ace that can take the value of 1 instead of 11
            if (value > 21)
            {
                for (int i = 0; i < hand.Count; i++)
                {
                    if (hand[i].getFace().Equals("A"))
                    {
                        value = value - 10;
                        if (value <= 21)
                            break;
                    }
                }
                return value;
            }
            else
                return value;
        }

        public String toString()
        {
            return "Players's Hand:" + hand.ToString();
        }

    }
}
