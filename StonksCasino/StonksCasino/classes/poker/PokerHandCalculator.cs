using StonksCasino.classes.Main;
using StonksCasino.classes.poker;
using StonksCasino.enums.card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StonksCasino.enums.poker;
using StonksCasino.Views.poker;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace StonksCasino.classes.poker
{
    class PokerHandCalculator
    {
        private ObservableCollection<string> _eventLog = new ObservableCollection<string>();

        public ObservableCollection<string> EventLog
        {
            get { return _eventLog; }
            set { _eventLog = value; }
        }


        /// <summary>
        /// This method calculates the highest possible value of cards for the player
        /// </summary>
        /// <param name="hand">This is a list of a player's cards</param>
        /// <param name="table">This is a list of card's on the table</param>
        /// <returns>The highest possible combination of five cards</returns>
        public static PokerHandValue GetHandValue(PokerPlayer player, List<Card> table)
        {
            List<Card> hand = new List<Card>();
            hand = player.Hand.ToList();
            List<Card> cards = new List<Card>();
            cards.AddRange(hand);
            cards.AddRange(table);
            cards = cards.OrderBy(x => (int)x.Value).ToList();
            bool royal = false;
            bool straight = false;
            bool flush = false;

            bool fourOfAKind = false;
            bool fullHouse = false;
            bool threeOfAKind = false;
            bool pair = false;
            bool twopair = false;

            // resultFS = result Flush Straight 
            PokerHandValue result;
            List<Card> highCards = new List<Card>();
            List<Card> resultFS = CheckFlushStraight(cards, out royal, out straight, out flush);
            List<Card> resultPair = CheckPairs(cards, out highCards, out fourOfAKind, out  fullHouse, out  threeOfAKind, out  pair, out twopair);


            #region pokerhands
            if (royal && flush)
            {
                //MessageBox.Show($"{player.PokerName} heeft een RoyalFlush, {resultFS[0].Type} {resultFS[0].Value}, {resultFS[1].Type} {resultFS[1].Value}, {resultFS[2].Type} {resultFS[2].Value} {resultFS[3].Type} {resultFS[3].Value}, {resultFS[4].Type} {resultFS[4].Value}");
                result = new PokerHandValue(PokerHand.RoyalFlush, 0, resultFS, player.PlayerID);
                return result;
            }
            else if (straight && flush)
            {
                //MessageBox.Show($"{player.PokerName} heeft een Straight Flush, {resultFS[0].Type} {resultFS[0].Value}, {resultFS[1].Type} {resultFS[1].Value}, {resultFS[2].Type} {resultFS[2].Value} {resultFS[3].Type} {resultFS[3].Value}, {resultFS[4].Type} {resultFS[4].Value}");
                result = new PokerHandValue(PokerHand.StraightFlush, (int)resultFS[2].Value, resultFS, player.PlayerID);
                return result;
            }
            else if (fourOfAKind)
            {
                //MessageBox.Show($"{player.PokerName} heeft een four of a kind, {resultPair[0].Type} {resultPair[0].Value}, {resultPair[1].Type} {resultPair[1].Value}, {resultPair[2].Type} {resultPair[2].Value} {resultPair[3].Type} {resultPair[3].Value}, {resultPair[4].Type} {resultPair[4].Value}");
                result = new PokerHandValue(PokerHand.FourOfAKind, (int)resultPair[0].Value, resultPair, highCards, player.PlayerID);
                return result;
            }
            else if (fullHouse)
            {
                //MessageBox.Show($"{player.PokerName} heeft een FullHouse, {resultPair[0].Type} {resultPair[0].Value}, {resultPair[1].Type} {resultPair[1].Value}, {resultPair[2].Type} {resultPair[2].Value} {resultPair[3].Type} {resultPair[3].Value}, {resultPair[4].Type} {resultPair[4].Value}");
                int fullHouseValue = ((int)resultPair[0].Value) * 100;
                fullHouseValue += (int)resultPair[3].Value;
                result = new PokerHandValue(PokerHand.FullHouse, fullHouseValue, resultPair, player.PlayerID);
                return result;
            }
            else if (flush)
            {
                //MessageBox.Show($"{player.PokerName} heeft een Flush, {resultFS[0].Type} {resultFS[0].Value}, {resultFS[1].Type} {resultFS[1].Value}, {resultFS[2].Type} {resultFS[2].Value} {resultFS[3].Type} {resultFS[3].Value}, {resultFS[4].Type} {resultFS[4].Value}");
                result = new PokerHandValue(PokerHand.Flush, 0, resultFS, player.PlayerID);
                return result;
            }
            else if (straight)
            {
                //MessageBox.Show($"{player.PokerName} heeft een straight, {resultFS[0].Type} {resultFS[0].Value}, {resultFS[1].Type} {resultFS[1].Value}, {resultFS[2].Type} {resultFS[2].Value} {resultFS[3].Type} {resultFS[3].Value}, {resultFS[4].Type} {resultFS[4].Value}");
                result = new PokerHandValue(PokerHand.Straight, (int)resultFS[2].Value, resultFS, player.PlayerID);
                return result;
            }
            else if (threeOfAKind)
            {
                //MessageBox.Show($"{player.PokerName} heeft een Three of a kind, {resultPair[0].Type} {resultPair[0].Value}, {resultPair[1].Type} {resultPair[1].Value}, {resultPair[2].Type} {resultPair[2].Value} {resultPair[3].Type} {resultPair[3].Value}, {resultPair[4].Type} {resultPair[4].Value}");
                result = new PokerHandValue(PokerHand.ThreeOfAKind, (int)resultPair[0].Value, resultPair, highCards, player.PlayerID);
                return result;
            }
            else if (twopair)
            {
                //MessageBox.Show($"{player.PokerName} heeft een twopair, {resultPair[0].Type} {resultPair[0].Value}, {resultPair[1].Type} {resultPair[1].Value}, {resultPair[2].Type} {resultPair[2].Value} {resultPair[3].Type} {resultPair[3].Value}, {resultPair[4].Type} {resultPair[4].Value}");
                int twoPairValue = (int)resultPair[0].Value * 100;
                twoPairValue += (int)resultPair[2].Value;
                result = new PokerHandValue(PokerHand.TwoPair, twoPairValue, resultPair, highCards, player.PlayerID);
                return result;
            }
            else if (pair)
            {
                //MessageBox.Show($"{player.PokerName} heeft een pair, {resultPair[0].Type} {resultPair[0].Value}, {resultPair[1].Type} {resultPair[1].Value}, {resultPair[2].Type} {resultPair[2].Value} {resultPair[3].Type} {resultPair[3].Value}, {resultPair[4].Type} {resultPair[4].Value}");
                result = new PokerHandValue(PokerHand.Pair, (int)resultPair[0].Value, resultPair, highCards, player.PlayerID);
                return result;
            }
            else
            {
                //MessageBox.Show($"{player.PokerName} heeft een highend, {resultPair[0].Type} {resultPair[0].Value}, {resultPair[1].Type} {resultPair[1].Value}, {resultPair[2].Type} {resultPair[2].Value} {resultPair[3].Type} {resultPair[3].Value}, {resultPair[4].Type} {resultPair[4].Value}");
                result = new PokerHandValue(PokerHand.HighCard, 0, resultPair, highCards, player.PlayerID);
                return result;
            }
            #endregion
        }

        /// <summary>
        /// Checks if a list of cards contains a straight, flush or royal flush
        /// </summary>
        /// <param name="cards">The list of cards to be checked</param>
        /// <param name="royal">Outputs whether result contains a royal flush</param>
        /// <param name="straight">Outputs whether result contains a straight</param>
        /// <param name="flush">Outputs whether result contains a flush</param>
        /// <returns>A list of cards ordered by highest value</returns>
        private static List<Card> CheckFlushStraight(List<Card> cards, out bool royal, out bool straight, out bool flush)
        {
            royal = false;
            straight = false;
            flush = false;
            // maak lijst van alle met dezelfde type
            List<Card> result = new List<Card>();
            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                List<Card> suit = cards.Select(x => x).Where(x => x.Type == type).ToList();
                if (suit.Count >= 5)
                {
                    flush = true;
                    suit = suit.OrderBy(x => (int)x.Value).ToList();
                   // List<Card> suitList = suit.ToList();

                    straight = CheckStraight(suit, out result, out royal);
                    if (!straight)
                    {
                        result = new List<Card>();
                        while (result.Count < 5)
                        {
                            result.Add(suit[suit.Count - 1]);
                            suit.RemoveAt(suit.Count - 1);
                        }
                    }
                }
            }
            if (!flush)
            {
                straight = CheckStraight(cards, out result, out royal);
            }
            return result;
        }

        /// <summary>
        /// Checks if a list of cards contains a straight, straight flush or an royal flush
        /// </summary>
        /// <param name="cardsToCheck">The list of cards to be checked</param>
        /// <param name="straight">Outputs a list of cards containing a straight</param>
        /// <param name="royal">Outputs whether result contains a flush</param>
        /// <returns>Whether result contains a straight</returns>
        private static bool CheckStraight(List<Card> cardsToCheck, out List<Card> straight, out bool royal)
        {
            royal = false;
            straight = new List<Card>();
            List<List<Card>> straights = new List<List<Card>>();

            foreach (Card card in cardsToCheck)
            {
                if (straight.Count > 0)
                {
                    if ((int)card.Value == ((int)straight[straight.Count - 1].Value))
                    {
                        // do nothing
                    }
                    else if ((int)card.Value == ((int)straight[straight.Count - 1].Value) + 1)
                    {
                        straight.Add(card);
                    }
                    else if (straight.Count < 5)
                    {
                        straights.Add(straight);
                        straight = new List<Card>();
                        straight.Add(card);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    straight.Add(card);
                }
            }

            //check if royal
            if (straight.Count == 5 && straight[straight.Count -1].Value == CardValue.Ace)
            {
                royal = true;
            }

            //check for lowest
            if (cardsToCheck[cardsToCheck.Count-1].Value == CardValue.Ace && straight.Count < 5)
            {
                straights.Add(straight);
                foreach (List<Card> straightToCheck in straights)
                {
                    if(straightToCheck.Count == 4 && straightToCheck[0].Value == CardValue.Two)
                    {
                        straight = straightToCheck;
                        straight.Add(cardsToCheck[cardsToCheck.Count - 1]);
                        straight = straight.OrderBy(x => (int)x.Value).ToList();
                    }
                }
            }

            //check if straight
            if (straight.Count >= 5)
            {
                straight.RemoveRange(0, straight.Count - 5);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a list of cards contains any combination of pairs
        /// </summary>
        /// <param name="hand">The list of cards to be checked</param>
        /// <param name="highCards">Outputs a list of cards ordered by highest value</param>
        /// <param name="fourOfAKind">Outputs whether result contains a four of a kind</param>
        /// <param name="fullHouse">Outputs whether result contains a fullhouse</param>
        /// <param name="threeOfAKind">Outputs whether result contains a three of a kind</param>
        /// <param name="pair">Outputs whether result contains a pair</param>
        /// <param name="twoPair">Outputs whether result contains two pairs</param>
        /// <returns>A list of cards containing the highest possible combination of pairs and high cards</returns>
        private static List<Card> CheckPairs(List<Card> hand, out List<Card> highCards, out bool fourOfAKind, out bool fullHouse, out bool threeOfAKind, out bool pair, out bool twoPair)
        {
            highCards = new List<Card>();
            fourOfAKind = false;
            fullHouse = false;
            threeOfAKind = false;
            pair = false;
            twoPair = false;

            List<List<Card>> pairs = new List<List<Card>>();

            List<Card> combo = new List<Card>();
            foreach (Card card in hand)
            {
                if (combo.Count == 0)
                {
                    combo.Add(card);
                }
                else if (combo[0].Value == card.Value)
                {
                    combo.Add(card);
                    if (card==hand[hand.Count-1])
                    {
                        pairs.Add(combo);
                    }
                }
                else if (combo.Count > 1)
                {
                    pairs.Add(combo);
                    combo = new List<Card>();
                    combo.Add(card);
                }
                else
                {
                    combo = new List<Card>();
                    combo.Add(card);
                }
            }
            
            if(pairs.Count > 0)
            {
                pairs.Reverse();

                //check four four of a kind
                List<Card> Result = CheckFourOfAKind(pairs, hand, out highCards, out fourOfAKind);
                if (fourOfAKind)
                {
                    return Result;
                }

                //List<List<Card>> cardList = pairs.OrderBy(x => x.Count).ToList();

                List<CardPair> cardPairs = new List<CardPair>();
                foreach (List<Card> cardlist in pairs)
                {
                    cardPairs.Add(new CardPair(cardlist));
                }
                cardPairs = cardPairs.OrderBy(x => x.Pair.Count).ThenBy(y => y.MyCardValue).ToList();

                //check for three of a kind and fullhouse
                Result = CheckThreeOfAKind(cardPairs, hand, out highCards, out threeOfAKind, out fullHouse);
                if (fullHouse || threeOfAKind)
                {
                    return Result;
                }

                //check for pair and twopair also rerturn if nothing
                Result = CheckPair(pairs, hand, out highCards, out pair, out twoPair);
                return Result;
            }
            List<Card> result = new List<Card>();
            while (result.Count < 5)
            {
                result.Add(hand[hand.Count - 1]);
                highCards.Add(hand[hand.Count - 1]);
                hand.RemoveAt(hand.Count - 1);
            }

            return result;
        }

        /// <summary>
        /// Checks if a list of cards contains a four of a kind
        /// </summary>
        /// <param name="pairs">The list of pairs to be checked</param>
        /// <param name="hand">The list of cards from which to pull a high card</param>
        /// <param name="highCards">Outputs a list containing one highcard</param>
        /// <param name="FourOfAKind">Outputs whether result contains a four of a kind</param>
        /// <returns>A list of cards possible containing a four of a kind</returns>
        private static List<Card> CheckFourOfAKind(List<List<Card>> pairs, List<Card> hand, out List<Card> highCards, out bool FourOfAKind)
        {
            highCards = new List<Card>();
            FourOfAKind = false;
            List<Card> result = new List<Card>();
            foreach (List<Card> pair in pairs)
            {
                if (pair.Count == 4)
                {
                    result.AddRange(pair);
                    foreach (Card card in pair)
                    {
                        hand.Remove(card);
                    }

                    result.Add(hand[hand.Count - 1]);
                    highCards.Add(hand[hand.Count - 1]);
                    FourOfAKind = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Checks if a list of cards contains a three of a kind or a fullhouse
        /// </summary>
        /// <param name="pairs">The list of pairs to be checked</param>
        /// <param name="hand">The list of cards to be checked</param>
        /// <param name="highCards">Outputs list of cards from which to pull two highcards</param>
        /// <param name="ThreeOfAKind">Outputs whether result contains a three of a kind</param>
        /// <param name="FullHouse">Outputs whether result contains a fullhouse</param>
        /// <returns>A list of cards possible containing a three of a kind or a fullhouse</returns>
        private static List<Card> CheckThreeOfAKind(List<CardPair> pairs, List<Card> hand, out List<Card> highCards, out bool ThreeOfAKind, out bool FullHouse)
        {
            highCards = new List<Card>();
            ThreeOfAKind  = false;
            FullHouse = false;
            List<Card> result = new List<Card>();

            if(pairs[0].MyCardValue == CardValue.Ace && pairs[0].Pair.Count == 3)
            {
                ThreeOfAKind = true;
                result.AddRange(pairs[0].Pair);
                pairs.RemoveAt(0);
            }

            for (int i = pairs.Count-1; i >= 0; i--)
            {
                if (!ThreeOfAKind && pairs[i].Pair.Count == 3)
                {
                    ThreeOfAKind = true;
                    result.AddRange(pairs[i].Pair);
                }
                else if (ThreeOfAKind)
                {
                    FullHouse = true;
                    result.Add(pairs[i].Pair[0]);
                    result.Add(pairs[i].Pair[1]);
                    return result;
                }
            }

            if (ThreeOfAKind)
            {
                
                foreach (Card card in result)
                {
                    hand.Remove(card);
                }


                result.Add(hand[hand.Count - 1]);
                result.Add(hand[hand.Count - 2]);
                highCards.Add(hand[hand.Count - 1]);
                highCards.Add(hand[hand.Count - 2]);
            }

            return result;
        }

        /// <summary>
        /// Checks if a list of cards contains a single pair of two pairs
        /// </summary>
        /// <param name="pairs">The list of pairs to be checked</param>
        /// <param name="hand">The list of cards to be checked</param>
        /// <param name="highCards">Outputs list of cards from which to pull three or one highcard(s)</param>
        /// <param name="pair">Outputs whether result contains a single pair</param>
        /// <param name="twoPair">Outputs whether result contains two pairs</param>
        /// <returns>A list of cards possible containing a single pair or two pairs</returns>
        private static List<Card> CheckPair(List<List<Card>> pairs, List<Card> hand, out List<Card> highCards, out bool pair, out bool twoPair)
        {
            highCards = new List<Card>();
            pair = false;
            twoPair = false;

            List<Card> result = new List<Card>();

            foreach (List<Card> combo in pairs)
            {
                if (result.Count < 4)
                {
                    result.AddRange(combo);
                }
            }

            if(result.Count >= 2)
            {
                pair = true;
            }
            if(result.Count >= 4)
            {
                twoPair = true;
            }

            foreach (Card card in result)
            {
                hand.Remove(card);
            }

            while (result.Count < 5)
            {
                result.Add(hand[hand.Count -1]);
                highCards.Add(hand[hand.Count - 1]);
                hand.RemoveAt(hand.Count - 1);
            }
            return result;
        }

    }
}
