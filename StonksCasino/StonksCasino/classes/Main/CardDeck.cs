using StonksCasino.classes.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using System.Security.Policy;
using StonksCasino.enums.card;

namespace StonksCasino.classes.Main
{
    class CardDeck
    {
        private List<Card> _shuffledCards = new List<Card>();

        public CardDeck()
        {

        }

        public CardDeck(List<Card> cards)
        {
            AddCards(cards);
        }

        public void AddCards(List<Card> addCards)
        {
            _shuffledCards.AddRange(addCards.Shuffle());
        }

        public bool DrawCard(out Card card)
        {
            card = null;
            if (_shuffledCards.Count > 0)
            {
                card = _shuffledCards[0];
                _shuffledCards.RemoveAt(0);
                return true;
            }
            return false;
        }

        public Card DrawCard()
        {
            if (_shuffledCards.Count > 0)
            {
                Card card = _shuffledCards[0];
                _shuffledCards.RemoveAt(0);
                return card;
            }
            return null;
        }

        public void AssembleDeck()
        {
            List<Card> cards = new List<Card>();
            foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
            {
                foreach (CardType type in Enum.GetValues(typeof(CardType)))
                {
                    cards.Add(new Card(type, value, CardBackColor.Blue));
                }
            }
            AddCards(cards);
        }


    }
}
