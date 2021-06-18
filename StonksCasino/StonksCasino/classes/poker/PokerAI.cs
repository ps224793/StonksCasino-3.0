using StonksCasino.classes.Main;
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

        public Random  RNG
        {
            get { return _rng; }
            set { _rng = value; }
        }

        private bool _badCards =  false;

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

        private int CalcPlayOdds()
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
            return playOdds;
        }

        public string CalcPreFlopMove(out bool isBluffing)
        {
            isBluffing = false;
            int playOdds = CalcPlayOdds();
            int rngOdds = RNG.Next(0, 101);
            if (playOdds >= rngOdds)
            {
                if (BadCards == true) 
                { 
                    RoundsSinceLastBluff = 0;
                    isBluffing = true;
                }
                else { RoundsSinceLastBluff++; }
                return "play";
            }
            else
            {
                RoundsSinceLastBluff++;
                return "fold";
            }
        }
    }
}
