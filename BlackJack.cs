//  Class:      BlackJack
//  Package:    BlackJackGame
//  Summary:    A Standard version of BlackJack implemented as a C# Console Application

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackGame;

namespace BlackJackGame
{
    class BlackJack
    {
        private Dealer dealer;
        private NamedPlayer[] players;
        private int numPlayers;
        private int numOfDecks;

        static void Main(string[] args)
        {
            BlackJack game = new BlackJack();
            game.playGame();
        }

        public void playGame()
        {
            int gameNumber = 0;
            bool keepPlaying = true;

            Console.WriteLine("\n♣  ♠  ♦  ♥   Welcome to BlackJack!  ♣  ♠  ♦  ♥\n\n");
            Console.WriteLine("In the game Blackjack, the aim of the player is to acheive a" 
                          + "\nhand closest to 21 without exceeding 21. After dealing the initial"
                          + "\ntwo cards, a player must decide to \"hit\" (draw a card) or"
                          + "\n\"stand\" (stop taking cards). If the total card value exceeds 21,"
                          + "\nthe player \"busts\" and loses.");

            initializeGame();

            while(keepPlaying)
            {
                gameNumber++; 
                Console.WriteLine("\n*** Starting game number: " + gameNumber + " ***");

                newRound();
                dealInitialHand();      // each player is dealt their initial hand of two cards.
                showAllPlayersHand();   // each player's hand is public knowledge, and must be displayed
                showDealersHand();      // reveal dealer's first card
                playersTurn();          // players take turns to either "hit" or "stand"
                dealersTurn();          // dealer reveals full hand and continues play
                determineWinners();     // display winners for the round

                Console.WriteLine("\nPlay Again? Please enter \"y\" to play again");
                keepPlaying = (Console.ReadLine()[0].Equals('y')) ? true : false;

            }
            Console.WriteLine("\nThank you for playing BlackJack!");
            Console.ReadLine();
        }


        // setup the game
        public void initializeGame()
        {

            String input, name;

            // get number of players
            Console.Write("\nHow many players are there? (Between 1-5): ");
            input = Console.ReadLine();
            while (!int.TryParse(input, out numPlayers) || numPlayers < 1 || numPlayers > 5)
            {
                Console.Write("Invalid Entry. How many players? (Between 1-5): ");
                input = Console.ReadLine();
            }
            players = new NamedPlayer[numPlayers];
            Console.WriteLine();

            // request name for each player
            for (int i = 0; i < numPlayers; i++)
            {
                Console.Write("Player " + (i + 1) + ", please enter your name: ");
                name = Console.ReadLine();
                players[i] = new NamedPlayer(name);       
            }
            Console.WriteLine();

            // get number of decks
            Console.Write("How many 52-card Decks would you like to play with? (Between 1-8): ");
            input = Console.ReadLine();
            while (!int.TryParse(input, out numOfDecks) || numOfDecks < 1 || numOfDecks > 8)
            {
                Console.Write("Invalid Entry. How many Decks of Cards? (Between 1-8): ");
                input = Console.ReadLine();
            }
            // set up the dealer with the number of decks requested
            dealer = new Dealer(numOfDecks);
            Console.WriteLine();

        }

        // Reset all player's hands and shuffle the deck
        public void newRound()
        {
            dealer.resetHand();
            dealer = new Dealer(numOfDecks);
            dealer.shuffle();
            for (int p = 0; p < players.Length; p++)
            {
                players[p].resetHand();
                players[p].setWin(false);
            }
       
        }

        // Each player is dealt the initial two cards
        public void dealInitialHand()
        {
            int card, player;
            Console.WriteLine("\nDealer is dealing two cards to each player.");

            // Deal 2 cards to each player
            for (card = 0; card < 2; card++)
            {
                for (player = 0; player < players.Length + 1; player++)
                {
                    // Dealer is the last one dealt
                    if (player == players.Length)
                        dealer.addCard(dealer.deal());
                    else
                        players[player].addCard(dealer.deal());
                }
            }
        }

        // shows each player's hand
        public void showAllPlayersHand()
        {
            Console.WriteLine("All players reveal their cards.");
            for (int p = 0; p < players.Length; p++)
            {
                Console.Write("\t" + players[p].getName() + "'s Hand: ");
                for (int c = 0; c < players[p].getHandSize(); c++)
                    Console.Write(players[p].getHand()[c].toString() + " ");
                Console.Write("\n");
            }
        }

        // shows dealer's card
        public void showDealersHand()
        {
            // reveal both cards if dealer has blackjack
            if (dealer.getValueOfHand() == 21)
            {
                Console.Write("Dealer has blackjack!\n Dealer reveals both cards: ");
                for (int c = 0; c < dealer.getHandSize(); c++)
                    Console.Write(dealer.getHand()[c].toString() + " ");
                Console.WriteLine();
            }
            // reveal first card, the second card (or "hole") is not revealed til round is complete
            else
                Console.WriteLine("Dealer reveals first card: "
                    + dealer.showCard().getFace() + "" + dealer.showCard().getSuit() + "\n");

        }


        // each player take their turn
        public void playersTurn()
        {
            Console.WriteLine("\n--- Players turn ---");
            
            for (int p = 0; p < players.Length; p++)
            {
                Console.WriteLine("\nPlayer: " + players[p].getName());
                
                // display player's current hand
                Console.Write("\t" + players[p].getName() + "'s Hand: ");
                for (int i = 0; i < players[p].getHandSize(); i++)
                    Console.Write(players[p].getHand()[i].toString() + " ");
                Console.WriteLine(" for a total value of " + players[p].getValueOfHand() );

                // player chooses to draw another card or not
                hitOrStand(p);

                // check to see if player has busted
                if (players[p].getValueOfHand() > 21)
                    Console.WriteLine("\t*** " + players[p].getName() + " has busted! ***\n");
                // check to see if player has BlackJack
                if (players[p].getValueOfHand() == 21)
                    Console.WriteLine("\t*** " + players[p].getName() + " has BlackJack! ***\n");
      
            }
        }

        
        // Continue checking if player wants to hit while total is under 21
        public void hitOrStand(int p)
        {
            string input;
            while (players[p].getValueOfHand() < 21)
            {

                Console.Write("\tEnter \"h\" to hit or \"s\" to stand: ");
                input = Console.ReadLine();
                if (input.Equals("h"))
                {
                    players[p].addCard(dealer.deal());
                    Console.WriteLine("\t" + players[p].getName() + " drew a " + players[p].getLastCard().toString());
                    Console.Write("\t" + players[p].getName() + "'s Hand: ");
                    for (int i = 0; i < players[p].getHandSize(); i++)
                        Console.Write(players[p].getHand()[i].toString() + " ");
                    Console.WriteLine(" for a total value of: " + players[p].getValueOfHand());
                }
                else if (input.Equals("s"))
                {
                    Console.WriteLine("\t" + players[p].getName() + " is standing with a total of: " + players[p].getValueOfHand());
                    break;
                }
                else
                    Console.Write("Invalid Entry!");
            }
            Console.WriteLine("");
        }

        // dealer plays its turn
        public void  dealersTurn()
        {
            Console.WriteLine("\n--- Dealers turn ---");

            // reveal "hole" card
            Console.WriteLine("\tDealer flips over the \"hole\" card: " + dealer.getLastCard().toString());
            Console.WriteLine("\tDealer's Hand Total: " + dealer.getValueOfHand());

            // dealer must take hits until the dealer's hand has a value of at least 17
            while (dealer.getValueOfHand() < 17)
            {
                dealer.addCard(dealer.deal());
                Console.WriteLine("\tDealer draws a: " + dealer.getLastCard().toString());
                Console.Write("\tDealer hand: ");
                for (int c = 0; c < dealer.getHandSize(); c++)
                    Console.Write(dealer.getHand()[c].toString() + " ");
                Console.WriteLine(" for a total value of " + dealer.getValueOfHand());
            }
            Console.WriteLine();

            // check to see if dealer has busted
            if (dealer.getValueOfHand() > 21)
                Console.WriteLine("\t*** Dealer has busted! ***\n");
        }


        public void determineWinners()
        {
            bool dealerWins = true;
            
            // each player's hand is compared to the dealers
            for (int p = 0; p < players.Length; p++)
            {
                // players's hand has a value greater than 21, player loses
                if (players[p].getValueOfHand() > 21)
                {
                    // do nothing
                }
                // dealer's hand has a value greater than 21, all players win
                else if (dealer.getValueOfHand() > 21)
                {
                    players[p].setWin(true); 
                    dealerWins = false;
                }
                // players having a hand with a value greater than the dealer's hand win
                else if (dealer.getValueOfHand() <= 21 && players[p].getValueOfHand() > dealer.getValueOfHand())
                {
                    players[p].setWin(true);
                    dealerWins = false;
                }

            }


            // display list of winners
            Console.WriteLine("\nList of Winners:");
            for (int p = 0; p < players.Length; p++)
            {
                if (players[p].getWin())
                    Console.WriteLine("\t" + players[p].getName() + " has won this round!");
            }
            if (dealerWins)
                Console.WriteLine("\tDealer has won this round!");
        }
    }
}
