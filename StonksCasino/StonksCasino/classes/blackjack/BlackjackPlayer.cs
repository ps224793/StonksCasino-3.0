using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using StonksCasino.classes.Main;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StonksCasino.classes.blackjack
{
    public class BlackjackPlayer : PropertyChange
    {
        public int Score
        {
            get { return GetScore(); }
        }

        private ObservableCollection<CardBlackjack> _hand;
        private bool _reset = false;
                        
        public bool MyReset
        {
            get { return _reset; }
            set { _reset = value; }
        }

        public ObservableCollection<CardBlackjack> Hand
        {
            get { return _hand; }
            set { _hand = value; OnPropertyChanged(); }
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
            Hand.Add(card);
            OnPropertyChanged("Score");
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
                            set = false;
                        }
                    }
                    else
                    {
                        score += (int)card.Value;
                        set = false;
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

        public void GameOver()
        {
            Hand.Clear();
            
            _reset = true;
            OnPropertyChanged("Score");

        }
    }
}
