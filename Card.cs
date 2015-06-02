//  Class:      Card
//  Package:    BlackJackGame
//  Summary:    Creates a Card with either a face and suit attributes or a non-standard
//              re-shuffle card

using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJackGame
{
    public class Card
    {
        private string face, suit;
        private bool resuffleCard;

        public Card()
        {   
            face = "";
            suit = "";
            resuffleCard = true;
        }

        public Card(string f, string s)
        {
            face = f;
            suit = s;
        }

        public string getFace()
        {
            return face;
        }

        public void setFace(string f)
        {
            face = f;
        }

        public string getSuit()
        {
            return suit;
        }

        public void setSuit(string s)
        {
            suit = s;
        }

        public bool isResuffleCard ()
        {
            return resuffleCard;
        }

        // determines card value based on face
        public  int getValue()
        {
            int value = 0;

            if (face.Equals("J") || face.Equals("Q") || face.Equals("K"))
                value = 10;
            else if (face.Equals("A"))
                value = 11;
            else
                value = Convert.ToInt32(face);
          
            return value;
        }

        public string toString()
        {
            return getFace() + getSuit();
        }
    }
}
