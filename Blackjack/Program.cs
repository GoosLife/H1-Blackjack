using System;
using System.Collections.Generic;
using System.Threading;

namespace Blackjack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();

            Hand player = new Hand();
            Hand dealer = new Hand();

            player.AddCard(deck.Draw());
            player.AddCard(deck.Draw());

            dealer.AddCard(deck.Draw());
            dealer.AddCard(deck.Draw());

            player.CalculateValues();
            dealer.CalculateValues();

            bool isRunning = true;
            bool isPlayerTurn = true;

            // Gameloop
            while (isRunning)
            {
                // Player started with blackjack
                if (player.FinalValue == 21 && dealer.FinalValue == 21)
                {
                    Console.WriteLine("Both the player and the dealer has blackjack.\nIt is a draw.");
                    isRunning = false;
                }

                // Only player has blackjack
                if (player.FinalValue == 21)
                {
                    Console.WriteLine("Blackjack! You win!");
                    isRunning = false;
                }

                // *** DO THE PLAYERS TURN ***
                while (isPlayerTurn)
                {
                    //player.CalculateValues();
                    DrawTable(dealer, player, isPlayerTurn);

                    // Player busted and lost
                    if (player.FinalValue > 21)
                    {
                        Console.WriteLine("BUST! You lose.");
                        isPlayerTurn = false; // It is no longer the players turn.
                        isRunning = false;
                        break; // Break loop when the game ends.
                    }

                    // Handle console input
                    ConsoleKey input = Console.ReadKey(true).Key;

                    switch (input)
                    {
                        // Player hits
                        case ConsoleKey.H:
                            player.AddCard(deck.Draw());
                            break;
                        // Player stands
                        case ConsoleKey.S:
                            isPlayerTurn = false;
                            break;
                        default:
                            break;
                    }
                }

                // *** DO THE DEALERS TURN ***
                while (!isPlayerTurn && isRunning)
                {
                    DrawTable(dealer, player, false);
                    //dealer.CalculateValues();

                    // Dealer has busted
                    if (dealer.FinalValue > 21)
                    {
                        Console.WriteLine("Dealer is bust! You win.");
                        isRunning = false;
                        break; // Break loop when the game ends
                    }

                    // Dealer stands on a hardcoded 16.
                    if (dealer.FinalValue < 16)
                    {
                        dealer.AddCard(deck.Draw());
                    }
                    else
                    {
                        // Output the winner when dealer is standing.
                        Console.WriteLine(dealer.FinalValue > player.FinalValue ? "Dealer has the highest hand. Dealer wins." : "You have the highest hand. You win!");
                        isRunning = false;
                        break; // Break loop when the game ends.
                    }

                    Thread.Sleep(500); // Wait 0.5 seconds between dealer turns to pace the game.
                }
            }

            Console.ReadKey();
        }

        public static void DrawTable(Hand dealer, Hand player, bool isPlayerTurn)
        {
            Console.Clear();

            // During player turn
            if (isPlayerTurn)
            {
                // *** DRAW DEALER ***
                Console.WriteLine("Dealer");
                Console.WriteLine(dealer.Cards[0]); // On the players turn, only draw one dealer card

                // In the special case of an ace as the visual card, the dealer hand has two possible values.
                if (dealer.Cards[0].Value == 1)
                {
                    Console.WriteLine("Value: 1/11");
                }
                // All other cards just have a singular value.
                else
                {
                    Console.WriteLine("Value: " + dealer.Cards[0].Value.ToString());
                }

                // *** DRAW SEPARATION LINE ***
                Console.WriteLine("--------------------");

                // *** DRAW PLAYER ***
                Console.WriteLine("Player\n");

                // Draw all player cards
                foreach (Card c in player.Cards)
                {
                    Console.Write(c + " ");
                }

                // Create space between the cards and the value
                Console.WriteLine();

                // Draw the value of the players cards
                for (int i = 0; i < player.PossibleValues.Count; i++)
                {
                    if (i == 0)
                        // The low value is always drawn
                        Console.WriteLine("Value: " + player.PossibleValues[i].ToString());
                    else
                        // When there is more than one value, do a / between the low and high value
                        Console.Write('/' + player.PossibleValues[i].ToString());
                }

                // *** DRAW PLAYER ACTIONS ***
                Console.WriteLine("Choose an action");
                Console.WriteLine("[S]tand / [H]it");
            }

            // During dealer turn
            else
            {
                // *** DRAW DEALER ***
                Console.WriteLine("Dealer\n");
                
                // Draw all the dealers cards
                foreach (Card c in dealer.Cards)
                {
                    Console.Write(c + " ");
                }

                // Create space between the cards and the value
                Console.WriteLine();

                for (int i = 0; i < dealer.PossibleValues.Count; i++)
                {
                    if (i == 0)
                        // The low value is always drawn
                        Console.WriteLine("Value: " + dealer.PossibleValues[i].ToString());
                    else
                        // When there is more than one value, do a / between the low and high value
                        Console.Write('/' + dealer.PossibleValues[i].ToString());
                }

                // *** DRAW SEPARATION LINE ***
                Console.WriteLine("--------------------");

                // *** DRAW PLAYER ***
                Console.WriteLine("Player\n");

                // Draw all player cards
                foreach (Card c in player.Cards)
                {
                    Console.Write(c + " ");
                }

                // Create space between the cards and the value
                Console.WriteLine();

                // Draw the value of the players cards
                for (int i = 0; i < player.PossibleValues.Count; i++)
                {
                    if (i == 0)
                        // The low value is always drawn
                        Console.WriteLine("Value: " + player.PossibleValues[i].ToString());
                    else
                        // When there is more than one value, do a / between the low and high value
                        Console.Write('/' + player.PossibleValues[i].ToString());
                }
            }
        }
    }
}
