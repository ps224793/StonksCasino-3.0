using StonksCasino.classes.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using StonksCasino.classes.blackjack;

namespace StonksCasino.classes.blackjack
{
    public class Computers : PropertyChange
    {
        private BlackJack _blackjackhit;

        public BlackJack Myblackjackhit
        {
            get { return _blackjackhit; }
            set { _blackjackhit = value; }
        }

        private int _Computervalue;

        public int MyComputervalue
        {
            get { return _Computervalue; }
            set { _Computervalue = value; OnPropertyChanged(); }
        }

        private BlackjackDeckComputer deck = new BlackjackDeckComputer();

        private List<BlackjackComputer> _computer;

        List<CardBlackjack> cards = new List<CardBlackjack>();

        public List<BlackjackComputer> Computer
        {
            get { return _computer; }
            set { _computer = value; OnPropertyChanged(); }
        }

        public Computers()
        {
            Computer = new List<BlackjackComputer>();
            Computer.Add(new BlackjackComputer());
        }

        public void ComputerDeal(int player)
        {
            SetComputerHand(Computer[0]);
            Computerhit(player);
        }

        public void Computerhit(int playervalue)
        {
            
            int Player = playervalue;
            int Bot = Computer[0].ScoreC;
 
            if (Bot < 17)
            {
                Computer[0].AddCard(deck.DrawCard());
                Computerhit(playervalue);
            }
            else if (Bot < 21 && playervalue > Bot && playervalue < 21)
            {
                Computer[0].AddCard(deck.DrawCard());
                Computerhit(playervalue);
    
            }
            
        }

        public void GameclearComputer()
        {
            Computer[0].GameOver();
        }

        public void SetComputerHand(BlackjackComputer computer)
        {
            cards.Add(deck.DrawCard());
            cards.Add(deck.DrawCard());
            computer.SetHandC(cards);          
        }
    }
}
