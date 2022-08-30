using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Player
    {
        private int handValue;
        public int HandValue
        {
            get { return handValue; }
            set { handValue = value; }
        }

        private List<Card> hand;
        public List<Card> Hand
        {
            get { return hand; }
            set { hand = value; }
        }

        /// <summary>
        /// Hit the player with a new card.
        /// </summary>
        /// <param name="c"></param>
        public void Hit(Card c)
        {
            Hand.Add(c);
        }

        /// <summary>
        /// The player stands, forfeiting their turn.
        /// </summary>
        public void Stand()
        {

        }

        // Calculate the value of all the cards in the hand
        private void CalculateHandValue()
        {
            for (int i = 0; i < Hand.Count; i++)
            {
                HandValue += Hand[i].Value;
            }
        }
    }
}
