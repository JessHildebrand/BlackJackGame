//  Class:      NamedPlayer
//  Package:    BlackJackGame
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJackGame
{
   public  class NamedPlayer : AbstractPlayer
    {
       String name = "";
       bool hasWon;

        public NamedPlayer(String n): base()
        {
            name = n;
            hasWon = false;
        }

        public bool getWin()
        {
            return hasWon;
        }

        public void setWin(bool w)
        {
            hasWon = w;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String n)
        {
            name = n;
        }

        public String toString()
        {
            return "Player's Name is " + getName();
        }

    }
}
