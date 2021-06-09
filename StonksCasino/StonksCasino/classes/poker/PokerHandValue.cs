using StonksCasino.classes.Main;
using StonksCasino.enums.poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StonksCasino.classes.poker
{
    public class PokerHandValue
    {
		private PokerHand _pokerHand;

		public PokerHand MyPokerHand
		{
			get { return _pokerHand; }
			set { _pokerHand = value; }
		}

        private int _playerID;

        public int PlayerID
		{
            get { return _playerID; }
            set { _playerID = value; }
        }

        private int _handValue;

		public int HandValue
		{
			get { return _handValue; }
			set { _handValue = value; }
		}

		private List<Card> _hand;

		public List<Card> Hand
		{
			get { return _hand; }
			set { _hand = value; }
		}

		private List<Card> _highCards = new List<Card>();

		public List<Card> HighCards
		{
			get { return _highCards; }
			set { _highCards = value; }
		}

		public PokerHandValue(PokerHand pokerHand)
		{
			MyPokerHand = pokerHand;
		}

		public PokerHandValue(PokerHand pokerHand, int handValue, List<Card> hand, int playerID)
		{
			MyPokerHand = pokerHand;
			HandValue = handValue;
			Hand = hand;
			PlayerID = playerID;
		}

		public PokerHandValue(PokerHand pokerHand, int handValue, List<Card> hand, List<Card> highCards, int playerID)
		{
			MyPokerHand = pokerHand;
			HandValue = handValue;
			Hand = hand;
			HighCards.AddRange(highCards);
			PlayerID = playerID;
		}
	}
}
