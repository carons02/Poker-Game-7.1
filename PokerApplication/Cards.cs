using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokerApplication
{
    public class Cards
    {
       //This class is used to take the cards and establish their number and shape using the strings suit and rank
            private string suit;
            private string rank;

        //This sets the override method to outputs from the two established strings (cardSuit and cardRank)
            public Cards(string cardSuit, string cardRank)
            {
                suit = cardSuit;
                rank = cardRank;
            }
        //This will override each of the Tostring function and replace them with the rank and suit of the 52 cards in the deck
        public string GetCards()
        {
            string result = string.Empty;
            result = rank + " of " + suit;
            return result;
        }
     
    }
}
