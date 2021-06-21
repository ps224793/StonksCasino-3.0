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
    class CardDeckBlack
    {
        private List<CardBlackjack> _shuffledCards = new List<CardBlackjack>();

        public CardDeckBlack()
        {

        }

        public CardDeckBlack(List<CardBlackjack> cards)
        {
            AddCards(cards);
        }

        public void AddCards(List<CardBlackjack> addCards)
        {
            _shuffledCards.AddRange(addCards.Shuffle());
        }

        public bool DrawCard(out CardBlackjack card)
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

        public CardBlackjack DrawCard()
        {
            if (_shuffledCards.Count > 0)
            {
                CardBlackjack card = _shuffledCards[0];
                _shuffledCards.RemoveAt(0);
                return card;
            }
            return null;
        }

        public void AssembleDeck(int numberOfDecks)
        {
            List<CardBlackjack> cards = new List<CardBlackjack>();
            for (int i = 0; i < numberOfDecks; i++)
            {
                foreach (BlackcardValue value in Enum.GetValues(typeof(BlackcardValue)))
                {
                    foreach (CardType type in Enum.GetValues(typeof(CardType)))
                    {
                        cards.Add(new CardBlackjack(type, value, CardBackColor.Blue));
                    }
                }
            }

            AddCards(cards);
        }
    }
}
