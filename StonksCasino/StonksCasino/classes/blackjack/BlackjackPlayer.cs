using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using StonksCasino.classes.Main;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace StonksCasino.classes.blackjack
{
    public class BlackjackPlayer : PropertyChange
    {
        public int Score
        {
            get { return GetScore(); }
        }

        public int ScoreL
        {
            get { return GetScoreL(); }
        }

        public int ScoreR
        {
            get { return GetScoreR(); }
        }

        private ObservableCollection<CardBlackjack> _hand;
        private bool _reset = false;

        public bool MyReset
        {
            get { return _reset; }
            set { _reset = value; }
        }

        private bool _reset2 = false;

        public bool MyReset2
        {
            get { return _reset2; }
            set { _reset2 = value; }
        }

        private bool _reset3 = false;

        public bool MyReset3
        {
            get { return _reset3; }
            set { _reset3 = value; }
        }

        private bool _splittoright = false;

        public bool Splittoright
        {
            get { return _splittoright; }
            set { _splittoright = value; }
        }

        private bool _splittoleft = false;

        public bool Splittoleft
        {
            get { return _splittoleft; }
            set { _splittoleft = value; }
        }

        public ObservableCollection<CardBlackjack> Hand
        {
            get { return _hand; }
            set { _hand = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ObservableCollection<CardBlackjack>> _splitHand = new ObservableCollection<ObservableCollection<CardBlackjack>>();

        public ObservableCollection<ObservableCollection<CardBlackjack>> SplitHand
        {
            get { return _splitHand; }
            set { _splitHand = value; OnPropertyChanged(); }
        }

        public void Split(CardBlackjack card1, CardBlackjack card2)
        {
            SplitHand.Add(new ObservableCollection<CardBlackjack>() { Hand[0], card1 });
            SplitHand.Add(new ObservableCollection<CardBlackjack>() { Hand[1], card2 });

            Hand = SplitHand[0];
        }

        public void SplitRightHand()
        {
            Hand = SplitHand[1];
        }

        public void Update()
        {
            OnPropertyChanged("ScoreL");
        }

        public void Update2()
        {
            OnPropertyChanged("ScoreR");
        }

        public void Changescore()
        {
            Splittoright = true;
            Splittoleft = false;
        }

        public void Changescore2()
        {
            Splittoright = false;
            Splittoleft = true;
        }

        public BlackjackPlayer()
        {
            Hand = new ObservableCollection<CardBlackjack>();
            OnPropertyChanged("Score");
        }

        public void SetHand(List<CardBlackjack> cards)
        {
            Hand = new ObservableCollection<CardBlackjack>();

            foreach (CardBlackjack card in cards)
            {
                Hand.Add(card);
            }
            OnPropertyChanged("Score");
        }

        public void AddCard(CardBlackjack card)
        {
            if (Splittoright == true && Splittoleft == false)
            {
                Hand.Add(card);
                OnPropertyChanged("ScoreR");
            }
            else if (Splittoleft == true && Splittoright == false)
            {
                Hand.Add(card);
                OnPropertyChanged("ScoreL");
            }
            else
            {
                Hand.Add(card);
                OnPropertyChanged("Score");
            }

        }

        private int GetScore()
        {
            bool set = false;
            int score = 0;
            if (_reset == false)
            {
                foreach (CardBlackjack card in Hand)
                {
                    if ((int)card.Value == 11 || (int)card.Value == 12 || (int)card.Value == 13)
                    {
                        score += 10;
                    }
                    else if ((int)card.Value == 1)
                    {
                        if ((score + 11) <= 21)
                        {
                            score += (int)card.Value + 10;
                            set = true;
                        }
                        else
                        {
                            score += (int)card.Value;
                        }
                    }
                    else
                    {
                        score += (int)card.Value;
                    }
                }
                if (score > 21)
                {
                    foreach (CardBlackjack card in Hand)
                    {
                        if ((int)card.Value == 1 && set == true)
                        {
                            score -= 10;
                            set = false;
                        }
                    }
                }
            }
            else
            {
                _reset = false;
            }


            return score;
        }

        private int GetScoreL()
        {
            bool set2 = false;
            int score2 = 0;
            if (_reset2 == false)
            {
                foreach (CardBlackjack card in Hand)
                {
                    if ((int)card.Value == 11 || (int)card.Value == 12 || (int)card.Value == 13)
                    {
                        score2 += 10;
                    }

                    else if ((int)card.Value == 1)
                    {
                        if ((score2 + 11) <= 21)
                        {
                            score2 += (int)card.Value + 10;
                            set2 = true;
                        }
                        else
                        {
                            score2 += (int)card.Value;
                        }
                    }
                    else
                    {
                        score2 += (int)card.Value;
                    }
                }
                if (score2 > 21)
                {
                    foreach (CardBlackjack card in Hand)
                    {
                        if ((int)card.Value == 1 && set2 == true)
                        {
                            score2 -= 10;
                            set2 = false;
                        }
                    }
                }
            }
            else
            {
                _reset2 = false;
            }

            return score2;
        }

        private int GetScoreR()
        {
            bool set3 = false;
            int score3 = 0;
            if (_reset3 == false)
            {
                foreach (CardBlackjack card in Hand)
                {
                    if ((int)card.Value == 11 || (int)card.Value == 12 || (int)card.Value == 13)
                    {
                        score3 += 10;
                    }

                    else if ((int)card.Value == 1)
                    {
                        if ((score3 + 11) <= 21)
                        {
                            score3 += (int)card.Value + 10;
                            set3 = true;
                        }
                        else
                        {
                            score3 += (int)card.Value;
                        }
                    }
                    else
                    {
                        score3 += (int)card.Value;
                    }
                }
                if (score3 > 21)
                {
                    foreach (CardBlackjack card in Hand)
                    {
                        if ((int)card.Value == 1 && set3 == true)
                        {
                            score3 -= 10;
                            set3 = false;
                        }
                    }
                }
            }
            else
            {
                _reset3 = false;
            }


            return score3;
        }

        public void GameOver()
        {
            Hand.Clear();

            _reset = true;
            _reset2 = true;
            _reset3 = true;
            OnPropertyChanged("Score");
            OnPropertyChanged("ScoreL");
            OnPropertyChanged("ScoreR");
            Splittoright = false;
            Splittoleft = false;
        }
    }
}
