using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
// Homework assignment for W2 weekend due Nov. 21, 2016


{

	class Program
	{

		public enum Suit
		{
			Hearts,
			Clubs,
			Diamonds,
			Spades
		}

		public enum Rank
		{
			Ace,
			Deuce,
			Three,
			Four,
			Five,
			Six,
			Seven,
			Eight,
			Nine,
			Ten,
			Jack,
			Queen,
			King
		}


		public class Card
		{
			public Suit Suit { get; set; }
			public Rank Rank { get; set; }

			public Card(Suit s, Rank r)
			{
				this.Suit = s;
				this.Rank = r;
			}

			public int GetCardValue()
			{
				var rv = 0;
				switch (this.Rank)
				{
					case Rank.Ace:
						rv = 11;
						break;
					case Rank.Deuce:
						rv = 2;
						break;
					case Rank.Three:
						rv = 3;
						break;
					case Rank.Four:
						rv = 4;
						break;
					case Rank.Five:
						rv = 5;
						break;
					case Rank.Six:
						rv = 6;
						break;
					case Rank.Seven:
						rv = 7;
						break;
					case Rank.Eight:
						rv = 8;
						break;
					case Rank.Nine:
						rv = 9;
						break;
					case Rank.Ten:
						rv = 10;
						break;
					case Rank.Jack:
						rv = 10;
						break;
					case Rank.Queen:
						rv = 10;
						break;
					case Rank.King:
						rv = 10;
						break;
					default:
						break;
				}
				return rv;
			}



			public override string ToString()
			{
				return $"The {this.Rank} of {this.Suit}";
			}
		}

		static int DealCard(Card currCard, int sum, string receiver, int count, bool hideCard)
		{

			if (hideCard)
			{
				Console.WriteLine($"{receiver} card #{count}: Face Down    - {receiver} showing {sum}");
				sum += currCard.GetCardValue();
			}
			else
			{
				sum += currCard.GetCardValue();
				Console.WriteLine($"{receiver} card #{count}: {currCard}    - {receiver} total {sum}");
			}
			return sum;
		}
		static void Main(string[] args)
		{
			// Create the deck and load in the cards
			var deck = new List<Card>();
			List<Card> randomDeck;

			foreach (Rank r in Enum.GetValues(typeof(Rank)))
			{
				foreach (Suit s in Enum.GetValues(typeof(Suit)))
				{
					deck.Add(new Card(s, r));
				}
			}

			int dealerSum;
			int dealerCount;
			int dealerShowing;
			int playerSum;
			int playerCount;
			int cardIndex;
			int holeCard;
			var wantToPlay = true;
			string hitMe;
			bool validAnswer;

			while (wantToPlay)
			{
				dealerSum = 0;
				dealerCount = 0;
				dealerShowing = 0;
				playerSum = 0;
				playerCount = 0;
				cardIndex = 0;
				hitMe = "Y";
				validAnswer = false;

				//sort (shuffle) the deck. NOTICE that the variable 'deck' is unchanged, but 'randomDeck' is the actual sorted deck.
				randomDeck = deck.OrderBy(x => Guid.NewGuid()).ToList();

				// Initial Deal - Deal 2 cards each to player and dealer
				playerSum = DealCard(randomDeck[cardIndex++], playerSum, "Player", ++playerCount, false);
				dealerSum = DealCard(randomDeck[cardIndex++], dealerSum, "Dealer", ++dealerCount, false);
				dealerShowing = dealerSum;
				playerSum = DealCard(randomDeck[cardIndex++], playerSum, "Player", ++playerCount, false);
				holeCard = cardIndex;
				dealerSum = DealCard(randomDeck[cardIndex++], dealerSum, "Dealer", ++dealerCount, true);

				if (playerSum == 21)
				{
					Console.WriteLine("\nYou have Blackjack. You win!  Congratulations.\n");
				}
				else
				{
					if (dealerSum == 21)
					{
						Console.WriteLine("\nDealer has Blackjack. You lose!  Sorry.\n");
					}
					else
					{
						// *** PLAYERS TURN ***
						Console.WriteLine($"\nYour current total is {playerSum} and the dealer is showing {dealerShowing}");
						//Player asks for card (hits) until stops or goes over 21
						while (playerSum <= 21 && hitMe == "Y")
						{
							do
							// Ask player if he wants to hit until you get a valid answer
							{
								Console.Write("Do you want to hit?  (Y/N)  ");
								hitMe = Console.ReadLine().ToUpper();
								switch (hitMe[0])
								{
									case 'Y':
										validAnswer = true;
										playerSum = DealCard(randomDeck[cardIndex++], playerSum, "Player", ++playerCount, false);
										break;

									case 'N':
										validAnswer = true;
										break;
									default:
										Console.WriteLine("Invalid answer. Try again.");
										validAnswer = false;
										break;
								}
							}
							while (!validAnswer);
						}
						// *** END PLAYERS TURN
						if (playerSum <= 21)
						{
							// *** DEALERS TURN ***
							Console.WriteLine($"\nDealers hole card is {randomDeck[holeCard]}.  Current total is {dealerSum}.  ");
							while ((dealerSum < 16))
							//while ((dealerSum < 16) || (dealerSum < playerSum))      alternate logic
								{
								Console.WriteLine("Dealer Hits");
								dealerSum = DealCard(randomDeck[cardIndex++], dealerSum, "Dealer", ++dealerCount, false);
								// *** END DEALERS TURN ***
							}
						}
						// *** DETERMINE WHO WON ***
						if (playerSum > 21)
						{
							Console.WriteLine("\nYou busted.  You lose!\n");
						}
						else if (dealerSum > 21)
						{
							Console.WriteLine("\nDealer busted.  You win!\n");
						}
						else if (playerSum > dealerSum)
						{
							Console.WriteLine("\nYour hand is higher. You Win!\n");
						}
						else if (playerSum < dealerSum)
						{
							Console.WriteLine("\nDealer hand is higher. You Lose!\n");
						}
						else
						{
							Console.WriteLine("\nYou tied so this is a push. No one wins.\n");
						}
					}
				}
				Console.ReadLine();
			}


		}
	}
}
