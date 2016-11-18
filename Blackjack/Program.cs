﻿using System;
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
				Console.WriteLine($"{receiver} card #{count}: Face Down    - total showing {sum}");
				sum += currCard.GetCardValue();
			}
			else
			{
				sum += currCard.GetCardValue();
				Console.WriteLine($"{receiver} card #{count}: {currCard}    - current total {sum}");
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
			var wantToPlay = true;
			var dealerWon = false;
			var playerWon = false;

			while (wantToPlay)
			{
				dealerSum = 0;
				dealerCount = 0;
				dealerShowing = 0;
				playerSum = 0;
				playerCount = 0;
				cardIndex = 0;

				//sort (shuffle) the deck. NOTICE that the variable 'deck' is unchanged, but 'randomDeck' is the actual sorted deck.
				randomDeck = deck.OrderBy(x => Guid.NewGuid()).ToList();

				// Initial Deal - Deal 2 cards each to player and dealer
				playerSum = DealCard(randomDeck[cardIndex++], playerSum, "Player\'s", ++playerCount, false);
				dealerSum = DealCard(randomDeck[cardIndex++], dealerSum, "Dealer\'s", ++dealerCount, false);
				playerSum = DealCard(randomDeck[cardIndex++], playerSum, "Player\'s", ++playerCount, false);
				dealerSum = DealCard(randomDeck[cardIndex++], dealerSum, "Dealer\'s", ++dealerCount, true);

				Console.ReadLine();

			}
			/*
			foreach (Card currcard in randomDeck)
			{
				Console.WriteLine(currcard);
				Console.WriteLine(currcard.Rank);
			}
			Console.WriteLine("and the first card again ...");
			Card myCard;
			int cardnum = 0;
			myCard = randomDeck[cardnum];
			cardnum++;

			Console.WriteLine($"First Whole card is {myCard}, just rank is {myCard.Rank}, just suit is {myCard.Suit}");
			var myValue = myCard.GetCardValue();
			Console.WriteLine($"And the card value is ... {myValue}");
			myCard = randomDeck[cardnum];

			Console.WriteLine($"Second Whole card is {myCard}, just rank is {myCard.Rank}, just suit is {myCard.Suit}");
			myValue = myCard.GetCardValue();
			Console.WriteLine($"And the card value is ... {myValue}");
			Console.ReadLine();
			*/

		}
	}
}
