using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PokerApplication.PokerApp;
using System.Media;
using System.IO;
using System.Drawing.Text;

namespace PokerApplication
{
   
    public partial class PokerApp : Form
    {
        //temporary add in case i need to run the game step by step in a sequence
        int onedraw = 0;
        //int d allows for the cards to be cycled through 1 at a time for dealing and drawing
        int d = 1;
        //creates an array with 52 spots for the cards to be put into
        string[] cards = new string[52];
        string[] cardsuit = { "♠", "♥", "♦", "♣" };
        string[] cardrank = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        //list of values to use for scoring between computer and user
        int royalflush = 100;
        int straightflush = 90;
        int fourofakind = 80;
        int fullhouse = 70;
        int flush = 60;
        int straight = 50;
        int threeofakind = 40;
        int twopair = 30;
        int onepair = 20;
        int acehigh = 14;
        int kinghigh = 13;
        int queenhigh = 12;
        int jackhigh = 11;
        int ten = 10;
        int nine = 9;
        int eight = 8;
        int seven = 7;
        int six = 6;
        int five = 5;
        int four = 4;
        int three = 3;
        int two = 2;

        public PokerApp()
        {
            //Application.SetHighDPIMode(highdpimode.systemaware);
            InitializeComponent();

        }

        //code for start button to begin the game nested with if statement that requires user to select computer difficulty level
        //prior to starting a game. Return a messagebox if a level is not picked.
        public void gamestartbutton_Click(object sender, EventArgs e)
        {
            if (easyradiobutton.Checked || mediumradiobutton.Checked || hardradiobutton.Checked)
            {
                //runs the shuffle sound when you start the game
                var stream = Properties.Resources.shuffling_cards_1;
                SoundPlayer myplayer = new SoundPlayer(stream);
                myplayer.Play();

                //creates the string of 52 cards and gives them a suit and a rank
                for (int b = 0; b < cards.Length; b++)
                {
                    cards[b] = cardrank[b % 13] + " " + cardsuit[b % 4];
                }
                //establishes a random variable and places each card randomly
                //the following for loop takes the cardsuit and cardrank cards and randomly puts them into a spot in the deck
                var random = new Random();
                for (int b = 0; b < cards.Length; b++)
                {
                    int c = random.Next(52);
                    string temporary = cards[b];
                    cards[b] = cards[c];
                    cards[c] = temporary;
                }
                //This deals out 5 cards to the dealer and the play in sequence off the top of the deck
                dealercard1.Text = cards[d];
                d++;
                dealercard2.Text = cards[d];
                d++;
                dealercard3.Text = cards[d];
                d++;    
                dealercard4.Text = cards[d];
                d++;
                dealercard5.Text = cards[d];
                d++;
                playercard1.Text = cards[d];
                d++;
                playercard2.Text = cards[d];
                d++;
                playercard3.Text = cards[d]; 
                d++;
                playercard4.Text = cards[d];
                d++;    
                playercard5.Text = cards[d];
                d++;

                //adds the text to the gameplay box asking user to select which cards they would like to keep
                ProgressRichTextBox.Text = "Which cards would you like to keep?";

            }
            else
            {
                //if a computer level is not selected, prompts the user to select a level first
                MessageBox.Show("Select a computer level first");
            }
        }
        //Close the game button. When selected the application closes
        private void gameclosebutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //easy, medium, and hard if statements for radio buttons that will pass
        //the computer diffuculty level into the game based on user selection.
        //Note enough time to code in how these the computer difficulty work but setup for a future upgrade
        private void easyradiobutton_CheckedChanged(object sender, EventArgs e)
        {
            if (easyradiobutton.Checked)
            {

            }
        }

        private void mediumradiobutton_CheckedChanged(object sender, EventArgs e)
        {
            if (mediumradiobutton.Checked)
            {

            }
        }

        private void hardradiobutton_CheckedChanged(object sender, EventArgs e)
        {
            if (hardradiobutton.Checked)
            {
 
            }
        }
        
        //Loads the poker application
        private void PokerApp_Load(object sender, EventArgs e)
        {
            
        }

        //the draw cards button will replace cards that the user has selected to not keep in their hand.
        //For each card, it adds 1 to d so that it selects the next card in the deck so there are no repeats.
        //The onedraw integer is to ensure the user doesn't try to replace cards in their hand multiple times. Shouldn't come up, but is available as needed.
        public void DrawCardsButton_Click(object sender, EventArgs e)
        {
            if (onedraw == 0)
            {
                if (playercardchkbx1.Checked == false)
                {
                    playercard1.Text = cards[d];
                    d++;
                }
                if (playercardchkbx2.Checked == false)
                {
                    playercard2.Text = cards[d];
                    d++;
                }
                if (playercardchkbx3.Checked == false)
                {
                    playercard3.Text = cards[d];
                    d++;
                }
                if (playercardchkbx4.Checked == false)
                {
                    playercard4.Text = cards[d];
                    d++;
                }
                if (playercardchkbx5.Checked == false)
                {
                    playercard5.Text = cards[d];
                    d++;
                }
                onedraw = 1;
                //uncovers the dealer cards
                dealercard1cover.Hide();
                dealercard2cover.Hide();
                dealercard3cover.Hide();
                dealercard4cover.Hide();
                dealercard5cover.Hide();


                //puts the players cards into a list to sort through for best hand
                List<string> PlayerHand = new List<string>();
                PlayerHand.Add(playercard1.Text.ToString());
                PlayerHand.Add(playercard2.Text.ToString());
                PlayerHand.Add(playercard3.Text.ToString());
                PlayerHand.Add(playercard4.Text.ToString());
                PlayerHand.Add(playercard5.Text.ToString());
                //puts the dealers cards into a list to sort through for best hand
                List<string> DealerHand = new List<string>();
                DealerHand.Add(dealercard1.Text.ToString());
                DealerHand.Add(dealercard2.Text.ToString());
                DealerHand.Add(dealercard3.Text.ToString());
                DealerHand.Add(dealercard4.Text.ToString());
                DealerHand.Add(dealercard5.Text.ToString());



                //checks player hand for a flush of any suit
                bool PallHaveSpades = PlayerHand.TrueForAll(n => n.Contains("♠"));
                bool PallHaveHearts = PlayerHand.TrueForAll(n => n.Contains("♥"));
                bool PallHaveDiamonds = PlayerHand.TrueForAll(n => n.Contains("♦"));
                bool PallHaveClubs = PlayerHand.TrueForAll(n => n.Contains("♣"));
                bool DallHaveSpades = DealerHand.TrueForAll(n => n.Contains("♠"));
                bool DallHaveHearts = DealerHand.TrueForAll(n => n.Contains("♥"));
                bool DallHaveDiamonds = DealerHand.TrueForAll(n => n.Contains("♦"));
                bool DallHaveClubs = DealerHand.TrueForAll(n => n.Contains("♣"));
            


                //still left to code: straight flush, 4 of a kind, full house, 3 of a kind, 2 pair, pair
                //checks if player has a royal flush
                if (PlayerHand.Contains("10") && PlayerHand.Contains("J") && PlayerHand.Contains("Q") && PlayerHand.Contains("K") && PlayerHand.Contains("A"))
                {
                    if (PallHaveSpades || PallHaveClubs || PallHaveDiamonds || PallHaveHearts)
                    {
                        MessageBox.Show("You Have a Royal Flush");
                    }
                    //checks if player has a straight 10-A
                    MessageBox.Show("You have a straight");
                }
                //checks if player has a flush
                if (PallHaveSpades || PallHaveClubs || PallHaveDiamonds || PallHaveHearts)
                {
                    MessageBox.Show("you have a flush");
                }
                //checks if player has a straight 2-6
                if (PlayerHand.Contains("2") && PlayerHand.Contains("3") && PlayerHand.Contains("4") && PlayerHand.Contains("5") && PlayerHand.Contains("6"))
                {
                    MessageBox.Show("You have a straight");
                }
                //checks if player has a straight 3-7
                if (PlayerHand.Contains("7") && PlayerHand.Contains("3") && PlayerHand.Contains("4") && PlayerHand.Contains("5") && PlayerHand.Contains("6"))
                {
                    MessageBox.Show("You have a straight");
                }
                //checks if player has a straight 4-8
                if (PlayerHand.Contains("7") && PlayerHand.Contains("8") && PlayerHand.Contains("4") && PlayerHand.Contains("5") && PlayerHand.Contains("6"))
                {
                    MessageBox.Show("You have a straight");
                }
                //checks if player has a straight 5-9
                if (PlayerHand.Contains("7") && PlayerHand.Contains("8") && PlayerHand.Contains("9") && PlayerHand.Contains("5") && PlayerHand.Contains("6"))
                {
                    MessageBox.Show("You have a straight");
                }
                //checks if player has a straight 6-10
                if (PlayerHand.Contains("7") && PlayerHand.Contains("8") && PlayerHand.Contains("9") && PlayerHand.Contains("10") && PlayerHand.Contains("6"))
                {
                    MessageBox.Show("You have a straight");
                }
                //checks if player has a straight 7-J
                if (PlayerHand.Contains("7") && PlayerHand.Contains("8") && PlayerHand.Contains("9") && PlayerHand.Contains("10") && PlayerHand.Contains("J"))
                {
                    MessageBox.Show("You have a straight");
                }
                //checks if player has a straight 8-Q
                if (PlayerHand.Contains("Q") && PlayerHand.Contains("8") && PlayerHand.Contains("9") && PlayerHand.Contains("10") && PlayerHand.Contains("J"))
                {
                    MessageBox.Show("You have a straight");
                }
                //checks if player has a straight 9-K
                if (PlayerHand.Contains("Q") && PlayerHand.Contains("K") && PlayerHand.Contains("9") && PlayerHand.Contains("10") && PlayerHand.Contains("J"))
                {
                    MessageBox.Show("You have a straight");
                }





                //checks if dealer has a royal flush
                if (DealerHand.Contains("10") && DealerHand.Contains("J") && DealerHand.Contains("Q") && DealerHand.Contains("K") && DealerHand.Contains("A"))
                {
                    if (DallHaveSpades || DallHaveClubs || DallHaveDiamonds || DallHaveHearts)
                    {
                        MessageBox.Show("You Have a Royal Flush");
                    }
                    //checks if dealer has a straight 10-A
                    MessageBox.Show("You have a straight");
                }
                //checks if dealer has a flush
                if (DallHaveSpades || DallHaveClubs || DallHaveDiamonds || DallHaveHearts)
                {
                    MessageBox.Show("you have a flush");
                }
                //checks if dealer has a straight 2-6
                if (DealerHand.Contains("2") && DealerHand.Contains("3") && DealerHand.Contains("4") && DealerHand.Contains("5") && DealerHand.Contains("6"))
                {
                    MessageBox.Show("You have a straight");
                }
                //checks if dealer has a straight 3-7
                if (DealerHand.Contains("7") && DealerHand.Contains("3") && DealerHand.Contains("4") && DealerHand.Contains("5") && DealerHand.Contains("6"))
                {
                    MessageBox.Show("You have a straight");
                }
                //checks if dealer has a straight 4-8
                if (DealerHand.Contains("7") && DealerHand.Contains("8") && DealerHand.Contains("4") && DealerHand.Contains("5") && DealerHand.Contains("6"))
                {
                    MessageBox.Show("You have a straight");
                }
                //checks if dealer has a straight 5-9
                if (DealerHand.Contains("7") && DealerHand.Contains("8") && DealerHand.Contains("9") && DealerHand.Contains("5") && DealerHand.Contains("6"))
                {
                    MessageBox.Show("You have a straight");
                }
                //checks if dealer has a straight 6-10
                if (DealerHand.Contains("7") && DealerHand.Contains("8") && DealerHand.Contains("9") && DealerHand.Contains("10") && DealerHand.Contains("6"))
                {
                    MessageBox.Show("You have a straight");
                }
                //checks if dealer has a straight 7-J
                if (DealerHand.Contains("7") && DealerHand.Contains("8") && DealerHand.Contains("9") && DealerHand.Contains("10") && DealerHand.Contains("J"))
                {
                    MessageBox.Show("You have a straight");
                }
                //checks if dealer has a straight 8-Q
                if (DealerHand.Contains("Q") && DealerHand.Contains("8") && DealerHand.Contains("9") && DealerHand.Contains("10") && DealerHand.Contains("J"))
                {
                    MessageBox.Show("You have a straight");
                }
                //checks if dealer has a straight 9-K
                if (DealerHand.Contains("Q") && DealerHand.Contains("K") && DealerHand.Contains("9") && DealerHand.Contains("10") && DealerHand.Contains("J"))
                {
                    MessageBox.Show("You have a straight");
                }








                //A high card check between hands
                if(PlayerHand.Contains("A") && !DealerHand.Contains("A"))
                {
                    MessageBox.Show("Player wins with A high card.");
                }





            }
            //ensuring there is only 1 draw per game
            else
            {
                MessageBox.Show("you only get one draw.");
            }
            
        }

        //Future list of data to work scoring into the game between computer and player
        public enum HandRank
        {
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush
        }
        

        //Reset the game when the user clicks new deal. 
        private void NewDealButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
