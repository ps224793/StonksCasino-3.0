using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using StonksCasino.classes.Api;
using StonksCasino.classes.Main;
using StonksCasino.enums.poker;
using StonksCasino.Views.poker;

namespace StonksCasino.classes.poker
{
    public class PokerGame : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private PokerDeck deck = new PokerDeck();

        private List<PokerPlayer> _players;

        public List<PokerPlayer> Players
        {
            get { return _players; }
            set { _players = value; }
        }

        private int _numOfPlayersNotPlaying = 0;

        public int NumOfPlayersNotPlaying
        {
            get { return _numOfPlayersNotPlaying; }
            set { _numOfPlayersNotPlaying = value; }
        }

        private int _numOfActivePlayers;

        public int NumOfActivePlayers
        {
            get { return _numOfActivePlayers; }
            set { _numOfActivePlayers = value; }
        }

        private int _numOfPlayersAllIn;

        public int NumOfPlayersAllIn
        {
            get { return _numOfPlayersAllIn; }
            set { _numOfPlayersAllIn = value; }
        }


        private int _blindsBet = 5;

        public int BlindsBet
        {
            get { return _blindsBet; }
            set { _blindsBet = value; }
        }

        private string _currentPot;

        public string CurrentPot
        {
            get { return _currentPot; }
            set { _currentPot = value; }
        }

        private int _topBet = 0;

        public int TopBet
        {
            get { return _topBet; }
            set { _topBet = value; OnPropertyChanged("CallOrCheck"); OnPropertyChanged("RaiseOrAllIn"); }
        }

        public string RaiseOrAllIn
        {
            get
            {
                if (Players[0].Balance <= LastRaise + (TopBet - Players[0].Bet))
                {
                    return "All-in";
                }
                else
                {
                    return "Raise";
                }
            }
        }

        public string CallOrCheck
        {
            get
            {
                if (Players[0].Balance <= (TopBet - Players[0].Bet))
                {
                    return "All-in";
                }
                else if (Players[0].Bet == TopBet)
                {
                    return "Check";
                }
                else
                {
                    return "Call";
                }
            }
        }

        private int _lastRaise = 0;

        public int LastRaise
        {
            get { return _lastRaise; }
            set { _lastRaise = value; }
        }

        private string _gameState;

        public string GameState
        {
            get { return _gameState; }
            set { _gameState = value; }
        }

        private double _playerCardOpacity;

        public double PlayerCardOpacity
        {
            get { return _playerCardOpacity; }
            set { _playerCardOpacity = value; OnPropertyChanged(); }
        }

        private string _player0_CardVisibility;

        public string Player0_CardVisibility
        {
            get { return _player0_CardVisibility; }
            set { _player0_CardVisibility = value; OnPropertyChanged(); }
        }

        private string _player1_CardVisibility;

        public string Player1_CardVisibility
        {
            get { return _player1_CardVisibility; }
            set { _player1_CardVisibility = value; OnPropertyChanged(); }
        }

        private string _player2_CardVisibility;

        public string Player2_CardVisibility
        {
            get { return _player2_CardVisibility; }
            set { _player2_CardVisibility = value; OnPropertyChanged(); }
        }

        private string _player3_CardVisibility;

        public string Player3_CardVisibility
        {
            get { return _player3_CardVisibility; }
            set { _player3_CardVisibility = value; OnPropertyChanged(); }
        }

        private int _roundsSinceBlindsRaise = 0;

        public int RoundsSinceBlindsRaise
        {
            get { return _roundsSinceBlindsRaise; }
            set { _roundsSinceBlindsRaise = value; }
        }

        private bool _enablePlayerInput = false;

        public bool EnablePlayerInput
        {
            get { return _enablePlayerInput; }
            set { _enablePlayerInput = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _eventLog = new ObservableCollection<string>();

        public ObservableCollection<string> EventLog
        {
            get { return _eventLog; }
            set { _eventLog = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Card> _table;

        public ObservableCollection<Card> MyTable
        {
            get { return _table; }
            set { _table = value; OnPropertyChanged(); }
        }

        private Dictionary<string, PokerPot> _pots;

        public Dictionary<string, PokerPot> Pots
        {
            get { return _pots; }
            set { _pots = value; }
        }

        private int _numSidePots;

        public int NumSidePots
        {
            get { return _numSidePots; }
            set { _numSidePots = value; }
        }


        private Storyboard _boardOut;

        public Storyboard BoardOut
        {
            get { return _boardOut; }
            set { _boardOut = value; }
        }

        private int _gameCount = 1;

        public int GameCount
        {
            get { return _gameCount; }
            set { _gameCount = value; }
        }

        private string _player0Color = "White";

        public string Player0Color
        {
            get { return _player0Color; }
            set { _player0Color = value; OnPropertyChanged(); }
        }

        private string _player1Color = "White";

        public string Player1Color
        {
            get { return _player1Color; }
            set { _player1Color = value; OnPropertyChanged(); }
        }

        private string _player2Color = "White";

        public string Player2Color
        {
            get { return _player2Color; }
            set { _player2Color = value; OnPropertyChanged(); }
        }

        private string _player3Color = "White";

        public string Player3Color
        {
            get { return _player3Color; }
            set { _player3Color = value; OnPropertyChanged(); }
        }

        private string _sender = "Poker";
        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }


        private string btnStartDinges = "Visible";

        public string GameActive
        {
            get { return btnStartDinges; }
            set { btnStartDinges = value; OnPropertyChanged(); }
        }

        private string _nextRoundGrid = "Hidden";
        public string NextRoundGrid
        {
            get { return _nextRoundGrid; }
            set { _nextRoundGrid = value; OnPropertyChanged(); }
        }

        private int _gameSpeed = 2;
        public int GameSpeed
        {
            get { return _gameSpeed; }
            set { _gameSpeed = value; OnPropertyChanged(); }
        }

        private Random _rng;

        public Random RNG
        {
            get { return _rng; }
            set { _rng = value; }
        }

        public PokerGame()
        {
            RNG = new Random();
            Players = new List<PokerPlayer>();
            Pots = new Dictionary<string, PokerPot>();
            NumOfActivePlayers = 0;
            PlayerCardOpacity = 1;
            for (int i = 0; i < 4; i++)
            {
                Players.Add(new PokerPlayer());
                Players[i].Balance = 0;
                Players[i].RaiseBet = 0;
                Players[i].Bet = 0;
                Players[i].PlayerID = i;
                Players[i].Button = PokerButton.None;
                switch (i)
                {
                    case 0:
                        Players[i].PokerName = $"{User.Username}";
                        break;
                    case 1:
                        Players[i].PokerName = "Gambletron2000";
                        break;
                    case 2:
                        Players[i].PokerName = "ThePokernator";
                        break;
                    case 3:
                        Players[i].PokerName = "MyloBot";
                        break;
                }
            }
        }

        public async void setupNewGame()
        {
            // pay out winnings
            foreach (PokerPlayer player in Players)
            {
                if (player.PlayerID == 0 && !player.Busted)
                {
                    await ApiWrapper.UpdateTokens(1500, Sender);
                }
                player.Balance = 500;
                player.RaiseBet = 0;
                player.Bet = 0;
                player.Button = PokerButton.None;
                player.Busted = false;
                if (player.PlayerID == 0)
                {
                    player.Personality = "player";
                }
                else
                {
                    int personality = RNG.Next(1, 101);
                    if (personality <= 15)
                    {
                        player.Personality = "loose-passive";
                    }
                    else if (personality > 15 && personality <= 40)
                    {
                        player.Personality = "loose-aggressive";
                    }
                    else if (personality > 40 && personality <= 65)
                    {
                        player.Personality = "tight-passive";
                    }
                    else if (personality > 65)
                    {
                        player.Personality = "tight-aggressive";
                    }
                }
            }
            GameCount = 1;
            EventLog.Clear();
            RoundsSinceBlindsRaise = 0;
            BlindsBet = 5;
            NumOfActivePlayers = Players.Count;
            NumSidePots = 0;
            int startingDealer = RNG.Next(0, 4);
            PassButtons(3, startingDealer);
        }

        private void SetPlayerHand(PokerPlayer player)
        {
            List<Card> cards = new List<Card>();

            //daar
            //switch (player.PlayerID)
            //{
            //    case 0:
            //        cards.Add(new Card(enums.card.CardType.Diamonds, enums.card.CardValue.Ace, enums.card.CardBackColor.Blue));
            //        cards.Add(new Card(enums.card.CardType.Diamonds, enums.card.CardValue.King, enums.card.CardBackColor.Blue));
            //        break;
            //    case 1:
            //        cards.Add(new Card(enums.card.CardType.Spades, enums.card.CardValue.Two, enums.card.CardBackColor.Blue));
            //        cards.Add(new Card(enums.card.CardType.Spades, enums.card.CardValue.Three, enums.card.CardBackColor.Blue));
            //        break;
            //    case 2:
            //        cards.Add(new Card(enums.card.CardType.Hearts, enums.card.CardValue.Two, enums.card.CardBackColor.Blue));
            //        cards.Add(new Card(enums.card.CardType.Spades, enums.card.CardValue.Two, enums.card.CardBackColor.Blue));
            //        break;
            //    case 3:
            //        cards.Add(new Card(enums.card.CardType.Clubs, enums.card.CardValue.Five, enums.card.CardBackColor.Blue));
            //        cards.Add(new Card(enums.card.CardType.Spades, enums.card.CardValue.Seven, enums.card.CardBackColor.Blue));
            //        break;
            //}
            cards.Add(deck.DrawCard());
            cards.Add(deck.DrawCard());

            foreach (Card card in cards)
            {
                if (player.PlayerID != 0)
                {
                    card.Turned = true;
                }
            }
            player.SetHand(cards);
        }

        public void Raise(PokerPlayer player)
        {
            if (player.RaiseBet <= player.Balance && player.RaiseBet >= (LastRaise + (TopBet - player.Bet)))
            {
                int raised;
                int raiseBet = player.Raise(TopBet, out raised);
                LastRaise = raiseBet;
                TopBet += raiseBet;
                Pots[CurrentPot].Chips += raised;
            }
            else if (player.RaiseBet <= player.Balance && player.RaiseBet < (LastRaise + (TopBet - player.Bet)))
            {
                player.RaiseBet = LastRaise + (TopBet - player.Bet);
                int raised;
                int raiseBet = player.Raise(TopBet, out raised);
                TopBet += raiseBet;
                Pots[CurrentPot].Chips += raised;
            }
            if (player.Balance == 0) { AllIn(player); }
            else
            {
                EventLog.Add($"{player.PokerName} raised with {LastRaise}");
            }
            ScrollListbox();
            
            resetCheckedPlayers(player);
            if (player.PlayerID == 0) 
            {
                DisablePlayerInput(); 
                if (player.Balance != 0)
                {
                    WagerRound(player);
                }
            }
        }

        public void Fold(PokerPlayer player)
        {
            player.Fold();
            bool exitloop = false;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    if (player.PlayerID != 0)
                    {
                        clearPlayerHand(window as PokerWindow, player);
                    }
                    exitloop = true;
                }
                if (exitloop) break;
            }
            NumOfPlayersNotPlaying++;
            EventLog.Add($"{player.PokerName} folds");
            ScrollListbox();
            if (player.PlayerID == 0) 
            {
                DisablePlayerInput();
                WagerRound(player); 
            }
        }

        public void Call(PokerPlayer player)
        {
            if (player.Balance >= (TopBet - player.Bet))
            {
                Pots[CurrentPot].Chips = player.Call(Pots[CurrentPot].Chips, TopBet);
                EventLog.Add($"{player.PokerName} calls");
            }
            else
            {
                AllIn(player);
            }
            ScrollListbox();
            if (player.PlayerID == 0) 
            {
                DisablePlayerInput();
                WagerRound(player); 
            }
        }

        public void Check(PokerPlayer player)
        {
            player.Check();
            EventLog.Add($"{player.PokerName} checks");
            ScrollListbox();
            if (player.PlayerID == 0) 
            {
                DisablePlayerInput();
                WagerRound(player); 
            }
        }

        public void AllIn(PokerPlayer player)
        {
            if (player.Balance > 0)
            {
                Pots[CurrentPot].Chips = player.AllIn(Pots[CurrentPot].Chips);
            }
            else
            {
                player.IsAllIn = true;
            }
            NumOfPlayersAllIn++;
            // Switch from MainPot to SidePot
            List<PokerPlayer> eligablePlayers = new List<PokerPlayer>();
            if ((NumOfPlayersAllIn + NumOfPlayersNotPlaying) >= (Players.Count - 1))
            {
                foreach (PokerPlayer eligablePlayer in Players)
                {
                    if (eligablePlayer.Busted != true && eligablePlayer.IsAllIn != true && eligablePlayer.Folded != true) { eligablePlayers.Add(eligablePlayer); }
                }
                PokerPot newSidePot = new PokerPot(eligablePlayers, 0);
                NumSidePots++;
                string PotName = $"SidePot{NumSidePots}";
                Pots.Add(PotName, newSidePot);
                CurrentPot = PotName;
            }
            EventLog.Add($"{player.PokerName} goes all-in");
            ScrollListbox();
            if (player.PlayerID == 0) 
            {
                DisablePlayerInput();
                WagerRound(player); 
            }
        }

        public void StartGame()
        {
            GameActive = "Hidden";
            NextRoundGrid = "Hidden";
            EventLog.Add($"======{GameCount++}e GAME======");
            if (RoundsSinceBlindsRaise >= 5)
            {
                RaiseBlinds();
            }
            else
            {
                EventLog.Add($"The Small Blind is { BlindsBet }");
                EventLog.Add($"The Big Blind is {BlindsBet * 2}");
                ScrollListbox();
            }
            PlayerCardOpacity = 1;
            deck = new PokerDeck();
            GameState = "pre-Flop";
            foreach (PokerPlayer player in Players)
            {
                if (player.Busted) { NumOfPlayersNotPlaying++; }
            }
            PlaceBlinds();
            DealHoleCards();
            SetTable();
        }
        public void StartGame2()
        {
            EventLog.Add("======PRE-FLOP======");

            foreach (PokerPlayer player in Players)
            {
                bool exitLoop = false;
                switch (NumOfActivePlayers)
                {
                    case 2:
                    case 3:
                        if (player.Button == PokerButton.Dealer)
                        {
                            WagerRound(player);
                            exitLoop = true;
                        }
                        break;
                    default:
                        if (player.Button == PokerButton.None)
                        {
                            WagerRound(player);
                            exitLoop = true;
                        }
                        break;
                }
                if (exitLoop) break;
            }
        }

        private void PlaceBlinds()
        {
            List<PokerPlayer> eligablePlayers = new List<PokerPlayer>();
            foreach (PokerPlayer player in Players)
            {
                if (player.Busted != true) { eligablePlayers.Add(player); }
            }
            PokerPot MainPot = new PokerPot(eligablePlayers, 0);
            string PotName = "MainPot";
            Pots.Add(PotName, MainPot);
            CurrentPot = PotName;
            foreach (PokerPlayer player in Players)
            {
                switch (NumOfActivePlayers)
                {
                    case 2:
                        switch (player.Button)
                        {
                            case PokerButton.Dealer:
                                if (player.Balance >= BlindsBet)
                                {
                                    player.Bet += BlindsBet;
                                    player.Balance -= BlindsBet;
                                    Pots[CurrentPot].Chips += BlindsBet;
                                }
                                else
                                {
                                    AllIn(player);
                                }
                                break;
                            case PokerButton.BigBlind:
                                if (player.Balance >= (BlindsBet * 2))
                                {
                                    player.Bet += BlindsBet * 2;
                                    player.Balance -= BlindsBet * 2;
                                    Pots[CurrentPot].Chips += BlindsBet * 2;
                                    TopBet = BlindsBet * 2;
                                    LastRaise = TopBet;
                                }
                                else
                                {
                                    AllIn(player);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        switch (player.Button)
                        {
                            case PokerButton.SmallBlind:
                                if (player.Balance >= BlindsBet)
                                {
                                    player.Bet += BlindsBet;
                                    player.Balance -= BlindsBet;
                                    Pots[CurrentPot].Chips += BlindsBet;
                                }
                                else
                                {
                                    player.AllIn(Pots[CurrentPot].Chips);
                                }
                                break;
                            case PokerButton.BigBlind:
                                if (player.Balance >= (BlindsBet * 2))
                                {
                                    player.Bet += BlindsBet * 2;
                                    player.Balance -= BlindsBet * 2;
                                    Pots[CurrentPot].Chips += BlindsBet * 2;
                                    TopBet = BlindsBet * 2;
                                    LastRaise = TopBet;
                                }
                                else
                                {
                                    player.AllIn(Pots[CurrentPot].Chips);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }
        }

        private async void DealHoleCards()
        {
            foreach (PokerPlayer player in Players)
            {
                if (player.Busted != true)
                {
                    SetPlayerHand(player);
                }
            }
            bool exitloop = false;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    exitloop = true;
                    await Task.Delay(500);
                    foreach (PokerPlayer player in Players)
                    {
                        if (player.Busted != true || player.Hand.Count > 1)
                        {
                            placePlayerHand(window as PokerWindow, player);
                        }
                    }
                    if (exitloop) break;
                }
            }
        }

        private void placePlayerHand(PokerWindow window, PokerPlayer player)
        {
            if (player.Hand.Count > 1)
            {
                Storyboard player_out = (Storyboard)(window as PokerWindow).FindResource($"sbPlayer{player.PlayerID}_In");
                player_out.Begin();
            }
        }

        public async void WagerRound(PokerPlayer player)
        {
            int i = 0;
            int startingPlayer = player.PlayerID;
            if (GameState != "End")
            {
                if (Players[0].Folded == true && Players[0].Hand.Count > 1)
                {
                    PlayerCardOpacity = 0.5;
                }
                for (i = 0; i < Players.Count; i++)
                {
                    if (NumOfPlayersNotPlaying < (Players.Count - 1))
                    {
                        int currentPlayer = (i + startingPlayer) % Players.Count;
                        if (
                            !Players[currentPlayer].Busted && 
                            !Players[currentPlayer].Checked && 
                            !Players[currentPlayer].IsAllIn && 
                            !Players[currentPlayer].Folded &&
                            ((NumOfPlayersAllIn + NumOfPlayersNotPlaying) < (Players.Count - 1) ||
                            (NumOfPlayersAllIn + NumOfPlayersNotPlaying) >= (Players.Count - 1) &&
                            Players[currentPlayer].Bet < TopBet)
                            )
                        {
                            if (Players[currentPlayer].PlayerID != 0)
                            {
                                //hier
                                // Execute algorithm
                                ShowCurrentPlayer(Players[currentPlayer].PlayerID); //deze
                                await Task.Delay(GameSpeed * 1000);                 //deze
                                string action = Players[currentPlayer].ExecuteAI(GameState, _table, TopBet, BlindsBet);
                                switch (action)
                                {
                                    case "raise":
                                        Raise(Players[currentPlayer]);
                                        startingPlayer = currentPlayer;
                                        i = 0;
                                        break;
                                    case "fold":
                                        Fold(Players[currentPlayer]);
                                        break;
                                    case "check":
                                        Check(Players[currentPlayer]);
                                        break;
                                    case "call":
                                        Call(Players[currentPlayer]);
                                        break;
                                    case "all-in":
                                        AllIn(Players[currentPlayer]);
                                        break;
                                }
                            }
                            else
                            {
                                //MessageBox.Show($"{Players[currentPlayer].PokerName} is aan de beurt");
                                ShowCurrentPlayer(Players[currentPlayer].PlayerID);

                                //EventLog.Add($"{Players[currentPlayer].PokerName} is aan de beurt");
                                ScrollListbox();
                                Players[currentPlayer].RaiseBet = LastRaise + (TopBet - Players[currentPlayer].Bet);

                                EnablePlayerInput = true;
                                break;
                            }
                        }
                    }
                    else { break; }
                }
            }
            if (NumOfPlayersNotPlaying >= (Players.Count - 1))
            {
                foreach (PokerPlayer playerToCheck in Players)
                {
                    bool exitLoop = false;
                    if (!playerToCheck.Busted && !playerToCheck.Folded)
                    {
                        exitLoop = true;
                        WinByFold(playerToCheck);
                    }
                    if (exitLoop) break;
                }
            }
            else
            {
                if (i == Players.Count)
                {
                    switch (GameState)
                    {
                        case "pre-Flop":
                            PlaceFlop();
                            break;
                        case "Flop":
                            PlaceTurn();
                            break;
                        case "Turn":
                            PlaceRiver();
                            break;
                        case "River":
                            showdown();
                            break;
                    }
                }
            }
        }

        private void ShowCurrentPlayer(int player_ID)
        {
            Player0Color = "White";
            Player1Color = "White";
            Player2Color = "White";
            Player3Color = "White";
            switch (player_ID)
            {
                case 0:
                    Player0Color = "#FFFF6464";
                    break;
                case 1:
                    Player1Color = "#FFFF6464";
                    break;
                case 2:
                    Player2Color = "#FFFF6464";
                    break;
                case 3:
                    Player3Color = "#FFFF6464";
                    break;
            }
        }

        private void ScrollListbox()
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.GetType() == typeof(PokerWindow))
                {
                    ListBox lbEventLog = (ListBox)(item as PokerWindow).FindName("lbEventLog");
                    if (VisualTreeHelper.GetChildrenCount(lbEventLog) > 0)
                    {
                        Border border = (Border)VisualTreeHelper.GetChild(lbEventLog, 0);
                        ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                        scrollViewer.ScrollToBottom();
                    }
                }
            }
        }

        public void DisablePlayerInput()
        {
            EnablePlayerInput = false;
        }

        private void SetTable()
        {
            ObservableCollection<Card> cards = new ObservableCollection<Card>();
            for (int i = 0; i < 5; i++)
            {
                Card card = deck.DrawCard();
                card.Turned = true;
                cards.Add(card);
            }
            //daar
            //cards.Add(new Card(enums.card.CardType.Diamonds, enums.card.CardValue.Ten, enums.card.CardBackColor.Blue));
            //cards.Add(new Card(enums.card.CardType.Diamonds, enums.card.CardValue.Jack, enums.card.CardBackColor.Blue));
            //cards.Add(new Card(enums.card.CardType.Diamonds, enums.card.CardValue.Queen, enums.card.CardBackColor.Blue));
            //cards.Add(new Card(enums.card.CardType.Spades, enums.card.CardValue.Eight, enums.card.CardBackColor.Blue));
            //cards.Add(new Card(enums.card.CardType.Spades, enums.card.CardValue.Six, enums.card.CardBackColor.Blue));

            MyTable = cards;
        }

        private void resetCheckedPlayers(PokerPlayer currentPlayer)
        {
            foreach (PokerPlayer player in Players)
            {
                if (currentPlayer != null)
                {
                    if (player.Busted != true && player.IsAllIn != true && player.Folded != true && player.PlayerID != currentPlayer.PlayerID)
                    {
                        player.Checked = false;
                    }
                }
                else
                {
                    if (player.Busted != true && player.IsAllIn != true && player.Folded != true)
                    {
                        player.Checked = false;
                    }
                }
            }
        }

        private void newWagerRound()
        {
            if (GameState != "End")
            {
                foreach (PokerPlayer player in Players)
                {
                    switch (NumOfActivePlayers)
                    {
                        case 2:
                            if (player.Button == PokerButton.Dealer && GameState == "pre-Flop")
                            {
                                WagerRound(player);
                            }
                            else if (player.Button == PokerButton.BigBlind)
                            {
                                WagerRound(player);
                            }
                            break;
                        case 3:
                            if (player.Button == PokerButton.Dealer && GameState == "pre-Flop")
                            {
                                WagerRound(player);
                            }
                            else if (player.Button == PokerButton.SmallBlind)
                            {
                                WagerRound(player);
                            }
                            break;
                        default:
                            if (player.Button == PokerButton.None && GameState == "pre-Flop")
                            {
                                WagerRound(player);
                            }
                            else if (player.Button == PokerButton.SmallBlind)
                            {
                                WagerRound(player);
                            }
                            break;
                    }
                }
            }
        }

        private async void PlaceFlop()
        {
            EventLog.Add("======FLOP======");

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    Storyboard board = (Storyboard)(window as PokerWindow).FindResource("sbFlop");
                    board.Begin();

                    await Task.Delay(300);
                    MyTable[0].Turned = false;
                    await Task.Delay(500);
                    MyTable[1].Turned = false;
                    await Task.Delay(500);
                    MyTable[2].Turned = false;

                }
            }
            GameState = "Flop";
            resetCheckedPlayers(null);
            newWagerRound();
        }

        public async void PlaceTurn()
        {
            EventLog.Add("======TURN======");

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    Storyboard board = (Storyboard)(window as PokerWindow).FindResource("sbTurn");
                    board.Begin();

                    await Task.Delay(300);
                    MyTable[3].Turned = false;
                }
            }
            GameState = "Turn";
            resetCheckedPlayers(null);
            newWagerRound();
        }

        public async void PlaceRiver()
        {
            EventLog.Add("======RIVER======");

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    Storyboard board = (Storyboard)(window as PokerWindow).FindResource("sbRiver");
                    board.Begin();

                    await Task.Delay(300);
                    MyTable[4].Turned = false;
                }
            }
            GameState = "River";
            resetCheckedPlayers(null);
            newWagerRound();
        }

        public async void showdown()
        {
            EventLog.Add("======SHOWDOWN======");

            List<PokerHandValue> playerHands = new List<PokerHandValue>();

            List<PokerPlayer> activePlayers = new List<PokerPlayer>();
            foreach (PokerPlayer player in Players)
            {
                if (player.Busted != true && player.Folded != true)
                {
                    PokerHandValue result = PokerHandCalculator.GetHandValue(player, _table.ToList());
                    // playerHands.Add(result);
                    activePlayers.Add(player);
                    if (player.IsBluffing == true) { player.MyPokerAI.BluffsCaught++; }
                }
            }
            await showCards(activePlayers);

            foreach (KeyValuePair<string, PokerPot> pot in Pots)
            {
                if (pot.Value.Chips > 0)
                {
                    List<PokerHandValue> eligableHands = new List<PokerHandValue>();
                    foreach (PokerPlayer eligablePlayer in pot.Value.EligablePlayers)
                    {
                        if (eligablePlayer.Folded != true)
                        {
                            PokerHandValue result = PokerHandCalculator.GetHandValue(eligablePlayer, _table.ToList());
                            eligableHands.Add(result);
                        }
                    }
                    eligableHands = eligableHands.OrderBy(x => x.MyPokerHand).ToList();
                    List<PokerHandValue> eligablehighestHands = new List<PokerHandValue>();
                    foreach (PokerHandValue eligableHand in eligableHands)
                    {
                        if (eligableHand.MyPokerHand == eligableHands[0].MyPokerHand)
                        {
                            eligablehighestHands.Add(eligableHand);
                        }
                    }
                    if (eligablehighestHands.Count > 1)
                    {
                        for (int handToCompare = 1; handToCompare < eligablehighestHands.Count; handToCompare++)
                        {
                            for (int cardToCompare = 0; cardToCompare < 5; cardToCompare++)
                            {
                                if (eligablehighestHands[0].MyPokerHand == PokerHand.Straight ||
                                    eligablehighestHands[0].MyPokerHand == PokerHand.StraightFlush)
                                {
                                    if (eligablehighestHands[0].Hand[2].Value > eligablehighestHands[handToCompare].Hand[2].Value)
                                    {
                                        eligablehighestHands.RemoveAt(handToCompare);
                                        handToCompare = 0;
                                        break;
                                    }
                                    else if (eligablehighestHands[0].Hand[2].Value < eligablehighestHands[handToCompare].Hand[2].Value)
                                    {
                                        eligablehighestHands.RemoveAt(0);
                                        handToCompare = 0;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (eligablehighestHands[0].Hand[cardToCompare].Value > eligablehighestHands[handToCompare].Hand[cardToCompare].Value)
                                    {
                                        eligablehighestHands.RemoveAt(handToCompare);
                                        handToCompare = 0;
                                        break;
                                    }
                                    else if (eligablehighestHands[0].Hand[cardToCompare].Value < eligablehighestHands[handToCompare].Hand[cardToCompare].Value)
                                    {
                                        eligablehighestHands.RemoveAt(0);
                                        handToCompare = 0;
                                        break;
                                    }
                                }
                            }
                        }
                        if (eligablehighestHands.Count > 1)
                        {
                            EventLog.Add("Gelijkspel");
                            MessageWinningHand(eligablehighestHands, pot.Key);
                            for (int winningHand = 0; winningHand < eligablehighestHands.Count; winningHand++)
                            {
                                foreach (PokerPlayer player in Players)
                                {
                                    if (player.PlayerID == eligablehighestHands[winningHand].PlayerID)
                                    {
                                        player.Balance += pot.Value.Chips / eligablehighestHands.Count;
                                        ShowWinner(player.PlayerID);
                                    }
                                }
                            }
                            for (int player = 0; player < Players.Count; player++)
                            {
                                if ((Players[player].Button == PokerButton.SmallBlind || Players[player].Button == PokerButton.Dealer) && NumOfActivePlayers > 2)
                                {
                                    Players[player].Balance += pot.Value.Chips % eligablehighestHands.Count;
                                }
                            }
                            foreach (PokerPlayer player in Players)
                            {
                                player.Bet = 0;
                            }
                            await Task.Delay(3000);
                        }
                        else
                        {
                            Players[eligablehighestHands[0].PlayerID].Balance += pot.Value.Chips;
                            MessageWinningHand(eligablehighestHands, pot.Key);
                            ShowWinner(Players[eligablehighestHands[0].PlayerID].PlayerID);
                            await Task.Delay(3000);
                        }
                    }
                    else
                    {
                        Players[eligablehighestHands[0].PlayerID].Balance += pot.Value.Chips;
                        MessageWinningHand(eligablehighestHands, pot.Key);
                        ShowWinner(Players[eligablehighestHands[0].PlayerID].PlayerID);
                        await Task.Delay(3000);
                    }
                }
            }
            //playerHands = playerHands.OrderBy(x => x.MyPokerHand).ToList();
            //List<PokerHandValue> highestHands = new List<PokerHandValue>();
            //foreach (PokerHandValue playerHand in playerHands)
            //{
            //    if (playerHand.MyPokerHand == playerHands[0].MyPokerHand)
            //    {
            //        highestHands.Add(playerHand);
            //    }
            //}
            //if (highestHands.Count > 1)
            //{
            //    for (int handToCompare = 1; handToCompare < highestHands.Count; handToCompare++)
            //    {
            //        for (int cardToCompare = 0; cardToCompare < 5; cardToCompare++)
            //        {
            //            if (highestHands[0].MyPokerHand == PokerHand.Straight ||
            //                highestHands[0].MyPokerHand == PokerHand.StraightFlush)
            //            {
            //                if (highestHands[0].Hand[2].Value > highestHands[handToCompare].Hand[2].Value)
            //                {
            //                    highestHands.RemoveAt(handToCompare);
            //                    handToCompare = 0;
            //                    break;
            //                }
            //                else if (highestHands[0].Hand[2].Value < highestHands[handToCompare].Hand[2].Value)
            //                {
            //                    highestHands.RemoveAt(0);
            //                    handToCompare = 0;
            //                    break;
            //                }
            //            }
            //            else
            //            {
            //                if (highestHands[0].Hand[cardToCompare].Value > highestHands[handToCompare].Hand[cardToCompare].Value)
            //                {
            //                    highestHands.RemoveAt(handToCompare);
            //                    handToCompare = 0;
            //                    break;
            //                }
            //                else if (highestHands[0].Hand[cardToCompare].Value < highestHands[handToCompare].Hand[cardToCompare].Value)
            //                {
            //                    highestHands.RemoveAt(0);
            //                    handToCompare = 0;
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //    if (highestHands.Count > 1)
            //    {
            //        EventLog.Add("Gelijkspel");
            //        MessageWinningHand(highestHands);
            //        for (int winningHand = 0; winningHand < highestHands.Count; winningHand++)
            //        {
            //            foreach (PokerPlayer player in Players)
            //            {
            //                if (player.PlayerID == highestHands[winningHand].PlayerID)
            //                {
            //                    player.Balance += Pots[CurrentPot].Chips / highestHands.Count;
            //                    ShowWinner(player.PlayerID);
            //                }
            //            }
            //        }
            //        for (int player = 0; player < Players.Count; player++)
            //        {
            //            if ((Players[player].Button == PokerButton.SmallBlind || Players[player].Button == PokerButton.Dealer) && NumOfActivePlayers > 2)
            //            {
            //                Players[player].Balance += Pots[CurrentPot].Chips % highestHands.Count;
            //            }
            //        }
            //        foreach (PokerPlayer player in Players)
            //        {
            //            player.Bet = 0;
            //        }
            //        await Task.Delay(3000);
            //    }
            //    else
            //    {
            //        Players[highestHands[0].PlayerID].Balance += Pots[CurrentPot].Chips;
            //        MessageWinningHand(highestHands);
            //        ShowWinner(Players[highestHands[0].PlayerID].PlayerID);
            //        await Task.Delay(3000);
            //    }
            //}
            //else
            //{
            //    Players[highestHands[0].PlayerID].Balance += Pots[CurrentPot].Chips;
            //    MessageWinningHand(highestHands);
            //    ShowWinner(Players[highestHands[0].PlayerID].PlayerID);
            //    await Task.Delay(3000);
            //}


            GameState = "End";

            if (NumOfActivePlayers <= 1)
            {
                NextRoundGrid = "Visible";
            }
            else if (GameCount > 1)
            {
                NextRoundGrid = "Visible";
            }
            else
            {
                GameActive = "Visible";
            }
        }

        public void NextRoundButton()
        {
            EndGame();
            if (NumOfActivePlayers <= 1) { setupNewGame(); }
            NextRoundGrid = "Hidden";
        }

        private void MessageWinningHand(List<PokerHandValue> winningHands, string potName)
        {
            foreach (PokerHandValue winningHand in winningHands)
            {
                //suitHands
                string sh0 = $"{winningHand.Hand[0].Type}".Replace("Spades", "♠").Replace("Clubs", "♣").Replace("Hearts", "♥").Replace("Diamonds", "♦");
                string sh1 = $"{winningHand.Hand[1].Type}".Replace("Spades", "♠").Replace("Clubs", "♣").Replace("Hearts", "♥").Replace("Diamonds", "♦");
                string sh2 = $"{winningHand.Hand[2].Type}".Replace("Spades", "♠").Replace("Clubs", "♣").Replace("Hearts", "♥").Replace("Diamonds", "♦");
                string sh3 = $"{winningHand.Hand[3].Type}".Replace("Spades", "♠").Replace("Clubs", "♣").Replace("Hearts", "♥").Replace("Diamonds", "♦");
                string sh4 = $"{winningHand.Hand[4].Type}".Replace("Spades", "♠").Replace("Clubs", "♣").Replace("Hearts", "♥").Replace("Diamonds", "♦");
                //valuehands
                string vh0 = $"{winningHand.Hand[0].Value}";
                string vh1 = $"{winningHand.Hand[1].Value}";
                string vh2 = $"{winningHand.Hand[2].Value}";
                string vh3 = $"{winningHand.Hand[3].Value}";
                string vh4 = $"{winningHand.Hand[4].Value}";
                //pokerhand
                string ph = $"{winningHand.MyPokerHand}";

                string[] search = { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };
                string[] _replace = { "2", "3", "4", "5", "6", "7", "8", "9", "10" };

                int pCounter = 0;
                foreach (string i in search)
                {
                    vh0 = vh0.Replace(i, _replace[pCounter]);
                    vh1 = vh1.Replace(i, _replace[pCounter]);
                    vh2 = vh2.Replace(i, _replace[pCounter]);
                    vh3 = vh3.Replace(i, _replace[pCounter]);
                    vh4 = vh4.Replace(i, _replace[pCounter]);
                    pCounter++;
                }
                string[] vhArray = { vh0, vh1, vh2, vh3, vh4 };
                int xCounter = 0;
                foreach (string x in vhArray)
                {
                    if (x == "10")
                    {
                        vhArray[xCounter] = vhArray[xCounter].Substring(0, 2);
                    }
                    else
                    {
                        vhArray[xCounter] = vhArray[xCounter].Substring(0, 1);
                    }
                    xCounter++;
                }
                string winnerCards = $"{ph}  {vhArray[0]}{sh0}  {vhArray[1]}{sh1}  {vhArray[2]}{sh2}  {vhArray[3]}{sh3}  {vhArray[4]}{sh4}";

                EventLog.Add($"{Players[winningHand.PlayerID].PokerName} wins {potName} -{winnerCards}-");
            }
            ScrollListbox();
        }

        private async void ShowWinner(int player_ID)
        {
            Player0Color = "White";
            Player1Color = "White";
            Player2Color = "White";
            Player3Color = "White";
            switch (player_ID)
            {
                case 0:
                    Player0Color = "Gold";
                    for (int i = 0; i < 14; i++)
                    {
                        await Task.Delay(200);
                        if (Player0_CardVisibility != "Hidden")
                        {
                            Player0_CardVisibility = "Hidden";
                        }
                        else
                        {
                            Player0_CardVisibility = "Visible";
                        }
                    }
                    break;

                case 1:
                    Player1Color = "Gold";
                    for (int i = 0; i < 14; i++)
                    {
                        await Task.Delay(200);
                        if (Player1_CardVisibility != "Hidden")
                        {
                            Player1_CardVisibility = "Hidden";
                        }
                        else
                        {
                            Player1_CardVisibility = "Visible";
                        }
                    }
                    break;
                case 2:
                    Player2Color = "Gold";
                    for (int i = 0; i < 14; i++)
                    {
                        await Task.Delay(200);
                        if (Player2_CardVisibility != "Hidden")
                        {
                            Player2_CardVisibility = "Hidden";
                        }
                        else
                        {
                            Player2_CardVisibility = "Visible";
                        }
                    }
                    break;
                case 3:
                    Player3Color = "Gold";
                    for (int i = 0; i < 14; i++)
                    {
                        await Task.Delay(200);
                        if (Player3_CardVisibility != "Hidden")
                        {
                            Player3_CardVisibility = "Hidden";
                        }
                        else
                        {
                            Player3_CardVisibility = "Visible";
                        }
                    }
                    break;
            }
        }

        private async void WinByFold(PokerPlayer player)
        {
            foreach (PokerPot pot in Pots.Values)
            {
                player.Balance += pot.Chips;
            }
            if (player.IsBluffing == true) { player.MyPokerAI.SuccesfulBluffs++; }
            EventLog.Add($"{player.PokerName} wins  -by fold-");
            ShowWinner(player.PlayerID);
            await Task.Delay(3000);
            GameState = "End";
            if (GameCount > 1)
            {
                NextRoundGrid = "Visible";
            }
            else
            {
                GameActive = "Visible";
            }
        }

        private async Task showCards(List<PokerPlayer> Players)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    for (int i = 0; i < Players.Count; i++)
                    {
                        if (Players[i].PlayerID != 0 && Players[i].Hand[0] != null && Players[i].Hand[1] != null)
                        {
                            Storyboard playerCards = (Storyboard)(window as PokerWindow).FindResource($"sbPlayer{Players[i].PlayerID}");
                            playerCards.Begin();

                            await Task.Delay(300);
                            Players[i].Hand[0].Turned = false;
                            Players[i].Hand[1].Turned = false;
                        }
                    }
                }
            }
        }

        private void clearPlayerHand(PokerWindow window, PokerPlayer player)
        {
            if (player.Hand != null)
            {
                Storyboard player_out = (Storyboard)(window as PokerWindow).FindResource($"sbPlayer{player.PlayerID}_Out");
                player_out.Begin();
            }
        }

        private async Task ClearTable()
        {
            bool exitLoop = false;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    foreach (PokerPlayer player in Players)
                    {
                        clearPlayerHand((window as PokerWindow), player);
                    }
                    if (MyTable.Count > 0)
                    {
                        Storyboard board = (Storyboard)(window as PokerWindow).FindResource("sbTableOut");
                        board.Begin();
                        exitLoop = true;
                    }
                    else
                    {
                        MessageBox.Show("Er is iets mis gegaan met de animatie");
                    }
                    await Task.Delay(500);
                    if (exitLoop) break;
                }
            }
        }

        public async void EndGame()
        {
            Player0Color = "White";
            Player1Color = "White";
            Player2Color = "White";
            Player3Color = "White";
            RoundsSinceBlindsRaise++;
            Pots.Clear();
            TopBet = 0;
            NumOfPlayersNotPlaying = 0;
            NumOfPlayersAllIn = 0;
            NumSidePots = 0;
            await ClearTable();
            int currentDealer = 0;
            foreach (PokerPlayer player in Players)
            {
                if (!player.Busted)
                {
                    player.Bet = 0;
                    if (player.Hand != null)
                    {
                        player.Hand.Clear();
                    }
                    player.Checked = false;
                    player.Folded = false;
                    player.IsAllIn = false;
                    if (player.Balance <= 0)
                    {
                        player.Busted = true;
                        NumOfActivePlayers--;
                    }
                }
                if (player.Button == PokerButton.Dealer)
                {
                    currentDealer = player.PlayerID;
                }
                player.Button = PokerButton.None;
            }
            switch (NumOfActivePlayers)
            {
                case 2:
                    PassButtons(1, currentDealer);
                    break;
                case 3:
                    PassButtons(2, currentDealer);
                    break;
                default:
                    PassButtons(3, currentDealer);
                    break;
            }
            MyTable.Clear();
        }

        private void PassButtons(int numOfButtons, int currentDealer)
        {
            for (int x = 0; x < numOfButtons; x++)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    int newDealer = (i + currentDealer + 1) % Players.Count;
                    if (Players[newDealer].Busted != true && Players[newDealer].Button == PokerButton.None)
                    {
                        switch (x)
                        {
                            case 0:
                                if (numOfButtons == 1)
                                {
                                    Players[newDealer].Button = PokerButton.Dealer;
                                }
                                else
                                {
                                    Players[newDealer].Button = PokerButton.Dealer;
                                }
                                x++;
                                break;
                            case 1:
                                if (numOfButtons == 1)
                                {
                                    Players[newDealer].Button = PokerButton.BigBlind;
                                }
                                else
                                {
                                    Players[newDealer].Button = PokerButton.SmallBlind;
                                }
                                x++;
                                break;
                            case 2:
                                Players[newDealer].Button = PokerButton.BigBlind;
                                x++;
                                break;
                            default:
                                Players[newDealer].Button = PokerButton.None;
                                break;
                        }
                    }
                    else if (Players[newDealer].Busted == true)
                    {
                        Players[newDealer].Button = PokerButton.None;
                    }
                }
            }
        }

        private void RaiseBlinds()
        {
            RoundsSinceBlindsRaise = 0;
            BlindsBet *= 2;
            EventLog.Add($"The Blinds have been raised");
            EventLog.Add($"The Small Blind is now { BlindsBet }");
            EventLog.Add($"The Big Blind is now {BlindsBet * 2}");
            ScrollListbox();
        }
    }
}
