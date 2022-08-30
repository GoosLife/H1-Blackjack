using System;

namespace Blackjack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();

            for (int i = 0; i < 52; i++)
            {
                Card c = deck.Draw();
                Console.WriteLine(c);
            }

            Console.ReadKey();
        }
    }
}
