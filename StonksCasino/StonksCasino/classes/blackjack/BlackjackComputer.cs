﻿using StonksCasino.classes.Main;
using StonksCasino.enums.card;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.blackjack
{
    public class BlackjackComputer : PropertyChange
    {
        public int ScoreC
        {
            get { return GetScoreC(); }
        }

        private ObservableCollection<CardBlackjack> _handc;

        public ObservableCollection<CardBlackjack> HandC
        {
            get { return _handc; }
            set { _handc = value; OnPropertyChanged(); }
        }

        public BlackjackComputer()
        {
            HandC = new ObservableCollection<CardBlackjack>();
            OnPropertyChanged("ScoreC");
        }

        public void SetHandC(List<CardBlackjack> cards)
        {
            HandC = new ObservableCollection<CardBlackjack>();

            foreach (CardBlackjack card in cards)
            {
                HandC.Add(card);
            }
            OnPropertyChanged("ScoreC");
        }

        public void AddCard(CardBlackjack card)
        { 
            HandC.Add(card);
            OnPropertyChanged("ScoreC");
        }

        private int GetScoreC()
        {
            bool set = false;
            int score = 0;

            foreach (CardBlackjack card in HandC)
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
                foreach (CardBlackjack card in HandC)
                {
                    if ((int)card.Value == 1 && set == true)
                    {
                        score -= 10;
                        set = false;
                    }
                }
            }
            return score;
        }

        public void GameOver()
        {
            HandC.Clear();
            OnPropertyChanged("ScoreC");
        }
    }
}
