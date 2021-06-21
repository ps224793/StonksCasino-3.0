using StonksCasino.classes.Main;
using StonksCasino.classes.poker;
using StonksCasino.enums.poker;
using StonksCasino.enums.card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace StonksCasino.classes.poker
{
    public class PokerAI
    {
        private PokerPlayer _player;

        public PokerPlayer Player
        {
            get { return _player; }
            set { _player = value; }
        }

        private Random _rng;

        public Random RNG
        {
            get { return _rng; }
            set { _rng = value; }
        }

        private bool _badCards = false;

        public bool BadCards
        {
            get { return _badCards; }
            set { _badCards = value; }
        }


        private int _roundsSinceLastBluff = 0;

        public int RoundsSinceLastBluff
        {
            get { return _roundsSinceLastBluff; }
            set { _roundsSinceLastBluff = value; }
        }

        private int _succesfulBluffs;

        public int SuccesfulBluffs
        {
            get { return _succesfulBluffs; }
            set { _succesfulBluffs = value; }
        }

        private int _bluffsCaught;

        public int BluffsCaught
        {
            get { return _bluffsCaught; }
            set { _bluffsCaught = value; }
        }

        public PokerAI(PokerPlayer player)
        {
            Player = player;
            RNG = new Random();
        }
        private string CalcPosition(int NumOfActivePlayers, PokerButton button)
        {
            switch (NumOfActivePlayers)
            {
                case 4:
                    switch (button)
                    {
                        case PokerButton.Dealer:
                            return "late";
                        case PokerButton.SmallBlind:
                        case PokerButton.BigBlind:
                            return "early";
                        case PokerButton.None:
                            return "mid";
                        default:
                            return null;
                    }
                case 3:
                    switch (button)
                    {
                        case PokerButton.Dealer:
                            return "late";
                        case PokerButton.SmallBlind:
                            return "early";
                        case PokerButton.BigBlind:
                            return "mid";
                        default:
                            return null;
                    }
                case 2:
                    return "heads-up";
                default:
                    return null;
            }
        }

        private int CalcStartingHand(ObservableCollection<Card> hand, string position)
        {
            if (position == "heads-up") return 100;
            hand.OrderBy(x => (int)x.Value).ToList();
            if (hand[0].Value == hand[1].Value)
            {
                switch (hand[0].Value)
                {
                    case CardValue.Ace:
                    case CardValue.King:
                    case CardValue.Queen:
                    case CardValue.Jack:
                    case CardValue.Ten:
                    case CardValue.Nine:
                    case CardValue.Eight:
                    case CardValue.Seven:
                        return 100;
                    case CardValue.Six:
                    case CardValue.Five:
                        switch (position)
                        {
                            case "early":
                                return 0;
                            case "mid":
                            case "late":
                                return 100;
                        }
                        break;
                    case CardValue.Four:
                    case CardValue.Three:
                    case CardValue.Two:
                        switch (position)
                        {
                            case "early":
                            case "mid":
                                return 0;
                            case "late":
                                return 100;
                        }
                        break;
                }
            }
            else if (hand[0].Type == hand[1].Type)
            {
                switch (hand[0].Value)
                {
                    case CardValue.Ace:
                        switch (hand[1].Value)
                        {
                            case CardValue.King:
                            case CardValue.Queen:
                            case CardValue.Jack:
                            case CardValue.Ten:
                                return 100;
                            case CardValue.Nine:
                            case CardValue.Eight:
                            case CardValue.Seven:
                            case CardValue.Six:
                                switch (position)
                                {
                                    case "early":
                                        return 0;
                                    case "mid":
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Five:
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                        }
                        break;
                    case CardValue.King:
                        switch (hand[1].Value)
                        {
                            case CardValue.Queen:
                            case CardValue.Jack:
                            case CardValue.Ten:
                                return 100;
                            case CardValue.Nine:
                                switch (position)
                                {
                                    case "early":
                                        return 0;
                                    case "mid":
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Eight:
                            case CardValue.Seven:
                            case CardValue.Six:
                            case CardValue.Five:
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                        }
                        break;
                    case CardValue.Queen:
                        switch (hand[1].Value)
                        {
                            case CardValue.Jack:
                            case CardValue.Ten:
                                return 100;
                            case CardValue.Nine:
                            case CardValue.Eight:
                                switch (position)
                                {
                                    case "early":
                                        return 0;
                                    case "mid":
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Seven:
                            case CardValue.Six:
                            case CardValue.Five:
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.Jack:
                        switch (hand[1].Value)
                        {
                            case CardValue.Ten:
                            case CardValue.Nine:
                                return 100;
                            case CardValue.Eight:
                                switch (position)
                                {
                                    case "early":
                                        return 0;
                                    case "mid":
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Seven:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Six:
                            case CardValue.Five:
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.Ten:
                        switch (hand[1].Value)
                        {
                            case CardValue.Nine:
                                return 100;
                            case CardValue.Eight:
                                switch (position)
                                {
                                    case "early":
                                        return 0;
                                    case "mid":
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Seven:
                            case CardValue.Six:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Five:
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.Nine:
                        switch (hand[1].Value)
                        {
                            case CardValue.Eight:
                                switch (position)
                                {
                                    case "early":
                                        return 0;
                                    case "mid":
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Seven:
                            case CardValue.Six:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Five:
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.Eight:
                        switch (hand[1].Value)
                        {
                            case CardValue.Seven:
                            case CardValue.Six:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Five:
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.Seven:
                        switch (hand[1].Value)
                        {
                            case CardValue.Six:
                            case CardValue.Five:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.Six:
                        switch (hand[1].Value)
                        {
                            case CardValue.Five:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.Five:
                        switch (hand[1].Value)
                        {
                            case CardValue.Four:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.Four:
                    case CardValue.Three:
                    case CardValue.Two:
                        return 0;
                }
            }
            else
            {
                switch (hand[0].Value)
                {
                    case CardValue.Ace:
                        switch (hand[1].Value)
                        {
                            case CardValue.King:
                            case CardValue.Queen:
                            case CardValue.Jack:
                            case CardValue.Ten:
                                return 100;
                            case CardValue.Nine:
                            case CardValue.Eight:
                            case CardValue.Seven:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Six:
                            case CardValue.Five:
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.King:
                        switch (hand[1].Value)
                        {
                            case CardValue.Queen:
                            case CardValue.Jack:
                                return 100;
                            case CardValue.Ten:
                                switch (position)
                                {
                                    case "early":
                                        return 0;
                                    case "mid":
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Nine:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Eight:
                            case CardValue.Seven:
                            case CardValue.Six:
                            case CardValue.Five:
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.Queen:
                        switch (hand[1].Value)
                        {
                            case CardValue.Jack:
                            case CardValue.Ten:
                                switch (position)
                                {
                                    case "early":
                                        return 0;
                                    case "mid":
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Nine:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Eight:
                            case CardValue.Seven:
                            case CardValue.Six:
                            case CardValue.Five:
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.Jack:
                        switch (hand[1].Value)
                        {
                            case CardValue.Ten:
                                switch (position)
                                {
                                    case "early":
                                        return 0;
                                    case "mid":
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Nine:
                            case CardValue.Eight:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Seven:
                            case CardValue.Six:
                            case CardValue.Five:
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.Ten:
                        switch (hand[1].Value)
                        {
                            case CardValue.Nine:
                            case CardValue.Eight:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Seven:
                            case CardValue.Six:
                            case CardValue.Five:
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.Nine:
                        switch (hand[1].Value)
                        {
                            case CardValue.Eight:
                            case CardValue.Seven:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Six:
                            case CardValue.Five:
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.Eight:
                        switch (hand[1].Value)
                        {
                            case CardValue.Seven:
                                switch (position)
                                {
                                    case "early":
                                    case "mid":
                                        return 0;
                                    case "late":
                                        return 100;
                                }
                                break;
                            case CardValue.Six:
                            case CardValue.Five:
                            case CardValue.Four:
                            case CardValue.Three:
                            case CardValue.Two:
                                return 0;
                        }
                        break;
                    case CardValue.Seven:
                    case CardValue.Six:
                    case CardValue.Five:
                    case CardValue.Four:
                    case CardValue.Three:
                    case CardValue.Two:
                        return 0;
                }
            }
            return 0;
        }

        private int CalcPlayOdds(int topBet)
        {
            string position = CalcPosition(4, Player.Button);
            int playThreshold = CalcStartingHand(Player.Hand, position);
            if (playThreshold != 100) { BadCards = true; }
            int playOdds = playThreshold;
            for (int i = 0; i < RoundsSinceLastBluff; i++)
            {
                playOdds += 20;
            }
            for (int i = 0; i < SuccesfulBluffs; i++)
            {
                playOdds += 20;
            }
            for (int i = 0; i < BluffsCaught; i++)
            {
                playOdds -= 20;
            }
            if (Player.Bet == topBet && playOdds < 100) { playOdds = 100; }
            return playOdds;
        }

        public string CalcPreFlopMove(ObservableCollection<Card> table, int topBet)
        {
            int playOdds = CalcPlayOdds(topBet);
            int rngOdds = RNG.Next(0, 101);
            if (playOdds >= rngOdds)
            {
                if (BadCards == true)
                {
                    RoundsSinceLastBluff = 0;
                    Player.IsBluffing = true;
                }
                else { RoundsSinceLastBluff++; }
                // raise if pair or bluff
                if (Player.Bet == topBet)
                {
                    return "check";
                }
                else if (Player.Bet < topBet && Player.Balance >= (topBet - Player.Bet))
                {
                    return "call";
                }
                else
                {
                    return "all-in";
                }
            }
            else
            {
                RoundsSinceLastBluff++;
                return "fold";
            }
        }

        public string CalcMove(ObservableCollection<Card> table, int topBet, string gamestate, int blindsBet, out int raiseBet)
        {
            raiseBet = 0;
            int strength = CalcHandstrength(table, gamestate);
            // RoyalFlush       = 10000
            // StraightFlush    = 800
            // FourOfAKind      = 700
            // FullHouse        = 600
            // Flush            = 500
            // Straight         = 400
            // ThreeOfAKind     = 300
            // TwoPair          = 200
            // Pair             = 100
            // HighCard         = 0
            // raise if pair
            if (strength > 100) 
            {
                return "raise";
            }
            else if (Player.Bet == topBet)
            {
                return "check";
            }
            else if (Player.Bet < topBet && Player.Balance >= (topBet - Player.Bet))
            {
                return "call";
            }
            else
            {
                return "all-in";
            }
        }

        private int CalcHandstrength(ObservableCollection<Card> table, string gamestate)
        {
            ObservableCollection<Card> useableCards = new ObservableCollection<Card>();
            switch (gamestate)
            {
                case "Flop":
                    useableCards = AddVisibleTableCards(table, 3);
                    break;
                case "Turn":
                    useableCards = AddVisibleTableCards(table, 4);
                    break;
                case "River":
                    useableCards = AddVisibleTableCards(table, 5);
                    break;
            }
            PokerHandValue result = PokerHandCalculator.GetHandValue(Player, useableCards.ToList());
            int handstrength = 0;
            List<Card> cards = new List<Card>();
            cards.AddRange(result.Hand);
            switch (result.MyPokerHand)
            {
                case PokerHand.RoyalFlush:
                    handstrength = 10000;
                    break;
                case PokerHand.StraightFlush:
                    handstrength = 800;
                    break;
                case PokerHand.FourOfAKind:
                    handstrength = 700;
                    break;
                case PokerHand.FullHouse:
                    handstrength = 600;
                    break;
                case PokerHand.Flush:
                    handstrength = 500;
                    break;
                case PokerHand.Straight:
                    handstrength = 400;
                    break;
                case PokerHand.ThreeOfAKind:
                    handstrength = 300;
                    break;
                case PokerHand.TwoPair:
                    handstrength = 200;
                    break;
                case PokerHand.Pair:
                    handstrength = 100;
                    break;
                case PokerHand.HighCard:
                    break;
            }
            foreach (Card card in cards)
            {
                switch (card.Value)
                {
                    case CardValue.Two:
                        handstrength += 2;
                        break;
                    case CardValue.Three:
                        handstrength += 3;
                        break;
                    case CardValue.Four:
                        handstrength += 4;
                        break;
                    case CardValue.Five:
                        handstrength += 5;
                        break;
                    case CardValue.Six:
                        handstrength += 6;
                        break;
                    case CardValue.Seven:
                        handstrength += 7;
                        break;
                    case CardValue.Eight:
                        handstrength += 8;
                        break;
                    case CardValue.Nine:
                        handstrength += 9;
                        break;
                    case CardValue.Ten:
                        handstrength += 10;
                        break;
                    case CardValue.Jack:
                        handstrength += 11;
                        break;
                    case CardValue.Queen:
                        handstrength += 12;
                        break;
                    case CardValue.King:
                        handstrength += 13;
                        break;
                    case CardValue.Ace:
                        handstrength += 14;
                        break;
                }
            }
            return handstrength;
        }

        private ObservableCollection<Card> AddVisibleTableCards(ObservableCollection<Card> table, int numOfVisibleCards)
        {
            ObservableCollection<Card> useableCards = new ObservableCollection<Card>();
            for (int i = 0; i < numOfVisibleCards; i++)
            {
                useableCards.Add(table[i]);
            }
            return useableCards;
        }
    }
}
