using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using StonksCasino.classes.Main;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StonksCasino.enums.poker;

namespace StonksCasino.classes.poker
{
    public class PokerPlayer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _pokerName;

        public string PokerName
        {
            get { return _pokerName; }
            set { _pokerName = value; }
        }

        private string _personality;

        public string Personality
        {
            get { return _personality; }
            set { _personality = value; }
        }

        private int _playerID;

        public int PlayerID
        {
            get { return _playerID; }
            set { _playerID = value; }
        }

        private bool _isBluffing = false;

        public bool IsBluffing
        {
            get { return _isBluffing; }
            set { _isBluffing = value; }
        }


        private PokerAI _pokerAI;

        public PokerAI MyPokerAI
        {
            get { return _pokerAI; }
            set { _pokerAI = value; }
        }


        /// <summary>
        /// Represents the cards this player has in their hand
        /// </summary>
        public ObservableCollection<Card> Hand { get; set; }

        private PokerButton _button;

        public PokerButton Button
        {
            get { return _button; }
            set { _button = value; OnPropertyChanged("ButtonImageURL"); }
        }

        public string ButtonImageURL
        {
            get
            {
                switch (Button)
                {
                    case PokerButton.Dealer:
                        return "/Img/PokerButtons/Dealer.png";
                    case PokerButton.SmallBlind:
                        return "/Img/PokerButtons/SmallBlind.png";
                    case PokerButton.BigBlind:
                        return "/Img/PokerButtons/BigBlind.png";
                    default:
                        return null;
                }
            }
        }

        private bool _checked;

        /// <summary>
        /// Represents if this player is done with their turn
        /// </summary>
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        private bool _isAllIn;

        /// <summary>
        /// Represents if this player is currently all-in
        /// </summary>
        public bool IsAllIn
        {
            get { return _isAllIn; }
            set { _isAllIn = value; }
        }

        private bool _folded;

        /// <summary>
        /// Represents if this player has folded
        /// </summary>
        public bool Folded
        {
            get { return _folded; }
            set { _folded = value; }
        }

        private bool _busted;

        /// <summary>
        /// Represents if this player has no chips left
        /// </summary>
        public bool Busted
        {
            get { return _busted; }
            set { _busted = value; }
        }

        private int _balance;

        /// <summary>
        /// Represents the amount of chips this player has left
        /// </summary>
        public int Balance
        {
            get { return _balance; }
            set { _balance = value; OnPropertyChanged(); }
        }

        private int _raiseBet;

        /// <summary>
        /// Represents the amount of chips this player has left
        /// </summary>
        public int RaiseBet
        {
            get { return _raiseBet; }
            set { _raiseBet = value; OnPropertyChanged(); }
        }

        private int _bet;

        /// <summary>
        /// Represents the amount of chips this player has currently bet
        /// </summary>
        public int Bet
        {
            get { return _bet; }
            set { _bet = value; OnPropertyChanged(); }
        }

        public PokerPlayer()
        {
            MyPokerAI = new PokerAI(this);
        }

        /// <summary>
        /// Sets this player's hand using the cards given
        /// </summary>
        /// <param name="cards">A list of cards to add to this player's hand</param>
        public void SetHand(List<Card> cards)
        {
            Hand = new ObservableCollection<Card>();

            foreach (Card card in cards)
            {
                Hand.Add(card);
            }
            OnPropertyChanged("Hand");
        }

        /// <summary>
        /// Raises this player's bet with the amount given
        /// </summary>
        public int Raise(int topBet, out int raiseBet)
        {
            raiseBet = RaiseBet;
            int raised = RaiseBet - (topBet - Bet);
            Balance -= RaiseBet;
            Bet += RaiseBet;
            Checked = true;
            return raised;
            // End of this player's turn
        }

        /// <summary>
        /// Raises this player's bet to match the highest bet on the table
        /// </summary>
        /// <param name="topBet">The current highest bet on the table</param>
        public int Call(int pot, int topBet)
        {
            Balance -= (topBet - Bet);
            pot += (topBet - Bet);
            Bet = topBet;
            Checked = true;
            return pot;
            // End of this player's turn
        }

        /// <summary>
        /// Simply ends this player's turn without raising their bet
        /// </summary>
        public void Check()
        {
            Checked = true;
            // End of this player's turn
        }

        /// <summary>
        /// Raises this player's bet with all their remaining balance
        /// </summary>
        public int AllIn(int pot)
        {
            Bet += Balance;
            pot += Balance;
            Balance = 0;
            IsAllIn = true;
            return pot;
            // End of this player's turn
        }

        /// <summary>
        /// <para>
        /// Folds this player removing them from betting and forfeits their chance to win the pot
        /// <br>Any chips they have already bet remain in the pot</br>
        /// </para> 
        /// </summary>
        public void Fold()
        {
            Folded = true;
            // End of this player's turn
        }

        public string ExecuteAI(string gameState, ObservableCollection<Card> table, int topBet, int blindsBet)
        {
            bool isBluffing = IsBluffing;
            switch (gameState)
            {
                case "pre-Flop":
                    return MyPokerAI.CalcPreFlopMove(topBet, blindsBet);
                default:
                    return MyPokerAI.CalcMove(table, topBet, gameState, blindsBet);
            }
        }

    }
}
