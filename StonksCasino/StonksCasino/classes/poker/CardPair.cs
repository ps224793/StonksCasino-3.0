using StonksCasino.classes.Main;
using StonksCasino.enums.card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.poker
{
    class CardPair
    {
		private List<Card> _pair;

		public List<Card> Pair
		{
			get { return _pair; }
			set { _pair = value; }
		}

		private CardValue _cardValue;

		public CardValue MyCardValue
		{
			get { return _cardValue; }
			set { _cardValue = value; }
		}

		public CardPair(List<Card> pair)
		{
			Pair = pair;
			MyCardValue = pair[0].Value; 
		}
	}
}
