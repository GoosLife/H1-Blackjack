using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    // Struct representing a playing card.
    struct Card
    {
        public string Name;
        public int Value; // The value of the card - from 1 to 10
        public string Color; // Color of the card

        public Card(int colorId, int value)
        {
            // Get color by id
            switch(colorId)
            {
                case 0:
                    Color = "♠";
                    break;
                case 1:
                    Color = "♣";
                    break;
                case 2:
                    Color = "♥";
                    break;
                case 3:
                    Color = "♦";
                    break;
                default:
                    throw new ArgumentException("Invalid card color.");
            }

            // Get name and value
            switch (value)
            {
                // These four values have different names from their values.
                case 1:
                    Value = value;
                    Name = "A";
                    break;
                case 11:
                    Value = 10;
                    Name = "J";
                    break;
                case 12:
                    Value = 10;
                    Name = "Q";
                    break;
                case 13:
                    Value = 10;
                    Name = "K";
                    break;
                // All other cards have the same name as their value.
                default:
                    Value = value;
                    Name = Value.ToString();
                    break;
            }
        }

        public override string ToString()
        {
            return Color + Name;
        }
    }

    internal class Deck
    {
        private Card[] cards;
        public Card[] Cards
        {
            get => cards;
            private set => cards = value;
        }

        public Deck()
        {
            Reset();   // Start by calling the reset method, as it creates the Cards array and adds all 52 cards to it.
            Shuffle(); // Shuffle the deck
        }

        // Resets the deck and adds the 52 standard playing cards.
        public void Reset()
        {
            // Reset the list of cards
            Cards = new Card[52];

            // Add all the cards anew
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    int index = (j - 1) + (13 * i); // The index the card must be placed on. Each card will be placed on the index equal to its value, shifted by 13 * i, to avoid the cards of the previous color.
                    Cards[index] = (new Card(i, j));
                }
            }
        }

        // Shuffle the deck using the Fisher-Yates shuffle.
        public void Shuffle()
        {
            // Used to draw a random card
            Random r = new Random();

            // Iterate over all the cards
            for (int i = 0; i < Cards.Length; i++)
            {
                int randomNumber = r.Next(Cards.Length - i); // Draw random card index from the remaining cards
                Card card = Cards[randomNumber]; // Take the card from the drawn index
                Cards[randomNumber] = Cards[i]; // Put the card at the current index where the randomly drawn card is
                Cards[i] = card; // Add the drawn card to the currently iterated index
            }
        }

        public Card Draw()
        {
            Card drawnCard; // The card that was drawn from the deck.

            Random r = new Random(); // Used to draw a random card from the deck.
            Card c = Cards[r.Next(Cards.Length - 1)]; // Draw a card from the deck.
            RemoveCardFromDeck(c); // Remove that card from the deck, so it is not drawn twice.
            return c;
        }

        // Remove a card from the deck, such as when drawing.
        private void RemoveCardFromDeck(Card c)
        {
            Card[] cardsAfterDrawing = new Card[Cards.Length - 1]; // Array containing all cards after drawing this card

            int newIndex = 0; // Index the card will have in the deck after removing the drawn card

            // Iterate over all cards, adding all but the drawn card to the new array.
            for (int i = 0; i < Cards.Length; i++)
            {
                if (!(Cards[i].Value == c.Value && Cards[i].Color == c.Color))
                {
                    cardsAfterDrawing[newIndex] = Cards[i];
                    newIndex++;
                }
            }

            Cards = cardsAfterDrawing; // The deck is now equal to the deck without the drawn card.
        }
    }
}
