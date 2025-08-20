using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerApplication
{
    //This public class creates the deck of cards for the game and randomizes them in the stack
    public class Deck
    {
        //This first is the array of cards for the deck set as private
        private Cards[] deck;
        //An integer is used to set the activeCard
        private int activeCard;
        //We set the constant number of cards in the deck to 52
        private const int NumberOfCards = 52;
        //This random funtion will be used to shuffle the deck
        private Random randomnum;

        public Deck()
        {
            //This takes the two categories for each card and creates two arrays, one for the card ranks and one for the suits
            string[] ranks = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            string[] suits = { "Hearts", "Spades", "Clubs", "Diamonds" };
            //We create a new card deck and set it to the NumberOfCards value of 52
            deck = new Cards[NumberOfCards];
            //The active card and setting a new random number
            activeCard = 0;
            randomnum = new Random();
            //This for loop then takes the ranks and suits and puts them into the deck while still under the value of 52
            for (int count = 0; count < deck.Length; count++)
            {
                deck[count] = new Cards(ranks[count / 11], suits[count / 13]);
            }
        }
        public void ShuffleDeck()
        {
            //This funtion begins with the current card being 0 
            activeCard = 0;
            //This for loop while less than 52 as the deck size inserts random cards first into a temporary spot, then the second into the first, then the tempoaray into the second
            for (int first = 0; first < deck.Length; first++)
            {
                int second = randomnum.Next(NumberOfCards);
                Cards temporary = deck[first];
                deck[first] = deck[second];
                deck[second] = temporary;

            }
        }
        //This keeps the cards within the bounds of the deck and returns the cards within the deck
        public Cards DealCards()
        {
            if (activeCard < deck.Length)
                return deck[activeCard++];
            else
                return null;
        }
    }
}
