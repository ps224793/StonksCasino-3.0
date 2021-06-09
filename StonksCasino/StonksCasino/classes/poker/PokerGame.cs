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
using System.Windows.Media.Animation;
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

        private int _numOfActivePlayers;

        public int NumOfActivePlayers
        {
            get { return _numOfActivePlayers; }
            set { _numOfActivePlayers = value; }
        }


        private int _blindsBet = 5;

        public int BlindsBet
        {
            get { return _blindsBet; }
            set { _blindsBet = value; }
        }

        private int _currentPot = 0;

        public int CurrentPot
        {
            get
            {
                if (true)
                {
                    return MainPot;
                }
                else
                {
                    return SidePot1;
                }
            }
            set
            {
                if (true)
                {
                    MainPot = value;
                }
                else
                {
                    SidePot1 = value;
                }
            }
        }

        private int _mainPot = 0;

        public int MainPot
        {
            get { return _mainPot; }
            set { _mainPot = value; }
        }

        private int _sidePot1 = 0;

        public int SidePot1
        {
            get { return _sidePot1; }
            set { _sidePot1 = value; }
        }

        private int _sidePot2 = 0;

        public int SidePot2
        {
            get { return _sidePot2; }
            set { _sidePot2 = value; }
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



        private ObservableCollection<Card> _table;

        public ObservableCollection<Card> MyTable
        {
            get { return _table; }
            set { _table = value; OnPropertyChanged(); }
        }

        private Storyboard _boardOut;

        public Storyboard BoardOut
        {
            get { return _boardOut; }
            set { _boardOut = value; }
        }


        public PokerGame()
        {
            PlayerCardOpacity = 1;
            Players = new List<PokerPlayer>();
            for (int i = 0; i < 4; i++)
            {
                Players.Add(new PokerPlayer());
                Players[i].Balance = 500;
                Players[i].RaiseBet = 0;
                Players[i].Bet = 0;
                Players[i].PlayerID = i;
                switch (i)
                {
                    case 0:
                        Players[i].PokerName = $"{User.Username}";
                        break;
                    case 1:
                        Players[i].PokerName = "Gambletron 2000";
                        break;
                    case 2:
                        Players[i].PokerName = "The Pokernator";
                        break;
                    case 3:
                        Players[i].PokerName = "MyloBot";
                        break;
                }
                NumOfActivePlayers++;
            }
            Players[0].Button = PokerButton.Dealer;
            Players[1].Button = PokerButton.SmallBlind;
            Players[2].Button = PokerButton.BigBlind;
            Players[3].Button = PokerButton.None;
            //PokerHandCalculator.GetHandValue(_players[0].Hand.ToList(), _table.ToList());
        }

        public void CalcHand()
        {
            //List<Card> handje = new List<Card>();
            //handje.Add(new Card(enums.card.CardType.Clubs, enums.card.CardValue.Six, enums.card.CardBackColor.Blue));
            //handje.Add(new Card(enums.card.CardType.Clubs, enums.card.CardValue.Jack, enums.card.CardBackColor.Blue));

            //List<Card> tafeltje = new List<Card>();
            //tafeltje.Add(new Card(enums.card.CardType.Diamonds, enums.card.CardValue.Seven, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Clubs, enums.card.CardValue.Three, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Spades, enums.card.CardValue.Three, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Hearts, enums.card.CardValue.Three, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Hearts, enums.card.CardValue.Seven, enums.card.CardBackColor.Blue));
            //PokerHandCalculator.GetHandValue(tafeltje, handje);
            PokerHandCalculator.GetHandValue(_players[0], _table.ToList());
        }

        private void SetPlayerHand(PokerPlayer player)
        {
            List<Card> cards = new List<Card>();
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

        public void Raise(PokerPlayer currentPlayer)
        {
            if (currentPlayer.RaiseBet <= currentPlayer.Balance && currentPlayer.RaiseBet >= (LastRaise + (TopBet - currentPlayer.Bet)))
            {
                int raised;
                int raiseBet = currentPlayer.Raise(TopBet, out raised);
                LastRaise = raiseBet;
                TopBet += raiseBet;
                CurrentPot += raised;
            }
            else if (currentPlayer.RaiseBet <= currentPlayer.Balance && currentPlayer.RaiseBet < (LastRaise + (TopBet - currentPlayer.Bet)))
            {
                currentPlayer.RaiseBet = LastRaise + (TopBet - currentPlayer.Bet);
                int raised;
                int raiseBet = currentPlayer.Raise(TopBet, out raised);
                TopBet += raiseBet;
                CurrentPot += raised;
            }
            resetCheckedPlayers(currentPlayer);
            WagerRound(currentPlayer);
        }

        public void Fold(PokerPlayer player)
        {
            player.Fold();
            bool exitloop = false;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    if (player.PlayerID == 0 && GameState == "End")
                    {
                        clearPlayerHand(window as PokerWindow, player);
                    }
                    exitloop = true;
                }
                if (exitloop) break;
            }
        }

        public void Call(PokerPlayer player)
        {
            if (player.Balance >= (TopBet - player.Bet))
            {
                CurrentPot = player.Call(CurrentPot, TopBet);
            }
        }

        public void Check(PokerPlayer player)
        {
            player.Check();
        }

        public void AllIn(PokerPlayer player)
        {
            if (player.Balance >= (TopBet - player.Bet))
            {
                CurrentPot = player.AllIn(CurrentPot);
                // Switch from MainPot to SidePot
            }
        }

        public void StartGame()
        {
            if (RoundsSinceBlindsRaise >= 5)
            {
                RaiseBlinds();
            }
            PlayerCardOpacity = 1;
            deck = new PokerDeck();
            GameState = "pre-Flop";
            PlaceBlinds();
            DealHoleCards();
            SetTable();


        }
        public void StartGame2()
        {
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
            foreach (PokerPlayer player in Players)
            {
                switch (player.Button)
                {
                    case PokerButton.SmallBlind:
                        if (player.Balance >= BlindsBet)
                        {
                            player.Bet += BlindsBet;
                            player.Balance -= BlindsBet;
                            CurrentPot += BlindsBet;
                        }
                        else
                        {
                            player.AllIn(CurrentPot);
                        }
                        break;
                    case PokerButton.BigBlind:
                        if (player.Balance >= (BlindsBet * 2))
                        {
                            player.Bet += (BlindsBet * 2);
                            player.Balance -= (BlindsBet * 2);
                            CurrentPot += (BlindsBet * 2);
                            TopBet = (BlindsBet * 2);
                            LastRaise = TopBet;
                        }
                        else
                        {
                            player.AllIn(CurrentPot);
                        }
                        break;
                    default:
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

        public void WagerRound(PokerPlayer player)
        {
            int i = 0;
            int numOfPlayersNotPlaying = 0;
            int startingPlayer = player.PlayerID;
            if (GameState != "End")
            {
                if (Players[0].Folded == true && Players[0].Hand.Count > 1)
                {
                    PlayerCardOpacity = 0.5;
                }
                for (i = 0; i < Players.Count; i++)
                {
                    int currentPlayer = (i + startingPlayer) % Players.Count;
                    if (Players[currentPlayer].Busted != true && Players[currentPlayer].Checked != true && Players[currentPlayer].IsAllIn != true && Players[currentPlayer].Folded != true)
                    {
                        if (Players[currentPlayer].PlayerID != 0)
                        {
                            // Execute algorithm
                            MessageBox.Show($"{Players[currentPlayer].PokerName} is aan de beurt");
                            if (Players[currentPlayer].Bet == TopBet)
                            {
                                Check(Players[currentPlayer]);
                            }
                            else if (Players[currentPlayer].Bet < TopBet && Players[currentPlayer].Balance >= (TopBet - Players[currentPlayer].Bet))
                            {
                                Call(Players[currentPlayer]);
                            }
                            else
                            {
                                AllIn(Players[currentPlayer]);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"{Players[currentPlayer].PokerName} is aan de beurt");
                            Players[currentPlayer].RaiseBet = LastRaise + (TopBet - Players[currentPlayer].Bet);
                            EnablePlayerInput = true;
                            break;
                        }
                    }
                    else if (Players[currentPlayer].Busted == true)
                    {
                        numOfPlayersNotPlaying++;
                    }
                }
            }
            if (numOfPlayersNotPlaying <= 3)
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
            else
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
        }

        public void DisablePlayerInput()
        {
            EnablePlayerInput = false;
        }

        private async void SetTable()
        {
            ObservableCollection<Card> cards = new ObservableCollection<Card>();
            for (int i = 0; i < 5; i++)
            {
                Card card = deck.DrawCard();
                card.Turned = true;
                cards.Add(card);
            }
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
                        case 3:
                            if (player.Button == PokerButton.SmallBlind && GameState == "pre-Flop")
                            {
                                WagerRound(player);
                            }
                            else if (player.Button == PokerButton.BigBlind)
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
            List<PokerHandValue> playerHands = new List<PokerHandValue>();

            List<PokerPlayer> activePlayers = new List<PokerPlayer>();
            foreach (PokerPlayer player in Players)
            {
                if (player.Busted != true && player.Folded != true)
                {
                    PokerHandValue result = PokerHandCalculator.GetHandValue(player, _table.ToList());
                    playerHands.Add(result);
                    activePlayers.Add(player);
                }
            }
            await showCards(activePlayers);
            playerHands = playerHands.OrderBy(x => x.MyPokerHand).ToList();
            List<PokerHandValue> highestHands = new List<PokerHandValue>();
            foreach (PokerHandValue playerHand in playerHands)
            {
                if (playerHand.MyPokerHand == playerHands[0].MyPokerHand)
                {
                    highestHands.Add(playerHand);
                }
            }
            if (highestHands.Count > 1)
            {
                for (int handToCompare = 1; handToCompare < highestHands.Count; handToCompare++)
                {
                    for (int cardToCompare = 0; cardToCompare < 5; cardToCompare++)
                    {
                        if (highestHands[0].MyPokerHand == PokerHand.Straight ||
                            highestHands[0].MyPokerHand == PokerHand.StraightFlush)
                        {
                            if (highestHands[0].Hand[2].Value > highestHands[handToCompare].Hand[2].Value)
                            {
                                highestHands.RemoveAt(handToCompare);
                                handToCompare = 0;
                                break;
                            }
                            else if (highestHands[0].Hand[2].Value < highestHands[handToCompare].Hand[2].Value)
                            {
                                highestHands.RemoveAt(0);
                                handToCompare = 0;
                                break;
                            }
                        }
                        else
                        {
                            if (highestHands[0].Hand[cardToCompare].Value > highestHands[handToCompare].Hand[cardToCompare].Value)
                            {
                                highestHands.RemoveAt(handToCompare);
                                handToCompare = 0;
                                break;
                            }
                            else if (highestHands[0].Hand[cardToCompare].Value < highestHands[handToCompare].Hand[cardToCompare].Value)
                            {
                                highestHands.RemoveAt(0);
                                handToCompare = 0;
                                break;
                            }
                        }
                    }
                }
                if (highestHands.Count > 1)
                {
                    for (int winningHand = 0; winningHand < highestHands.Count; winningHand++)
                    {
                        foreach (PokerPlayer player in Players)
                        {
                            if (player.PlayerID == highestHands[winningHand].PlayerID)
                            {
                                player.Balance += CurrentPot / highestHands.Count;
                                if (winningHand == 0)
                                {
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
                                }
                                if (winningHand == 1)
                                {
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
                                }
                                if (winningHand == 2)
                                {
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
                                }
                                if (winningHand == 3)
                                {
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
                                }
                            }
                        }
                    }
                    for (int player = 0; player < Players.Count; player++)
                    {
                        if (Players[player].Button == PokerButton.SmallBlind)
                        {
                            Players[player].Balance += CurrentPot % highestHands.Count;
                        }
                    }
                    foreach (PokerPlayer player in Players)
                    {
                        player.Bet = 0;
                    }
                    MessageBox.Show("Gelijkspel");
                }
                else
                {
                    Players[highestHands[0].PlayerID].Balance += CurrentPot;
                    MessageBox.Show($"{Players[highestHands[0].PlayerID].PokerName} heeft gewonnen!");
                }
            }
            else
            {
                Players[highestHands[0].PlayerID].Balance += CurrentPot;
                MessageBox.Show($"{Players[highestHands[0].PlayerID].PokerName} heeft gewonnen!");
            }

            GameState = "End";
            string testWonPlayer = "Player" + Players[highestHands[0].PlayerID].PlayerID.ToString() + "_CardVisibility";
            MessageBox.Show(testWonPlayer);

            switch (Players[highestHands[0].PlayerID].PlayerID)
            {
                case 0:
                    for (int i = 0; i < 14; i++)
                    {
                        if (Player0_CardVisibility != "Hidden")
                        {
                            Player0_CardVisibility = "Hidden";
                        }
                        else
                        {
                            Player0_CardVisibility = "Visible";
                        }
                        await Task.Delay(200);
                    }
                    break;
                case 1:
                    for (int i = 0; i < 14; i++)
                    {
                        if (Player1_CardVisibility != "Hidden")
                        {
                            Player1_CardVisibility = "Hidden";
                        }
                        else
                        {
                            Player1_CardVisibility = "Visible";
                        }
                        await Task.Delay(200);
                    }
                    break;
                case 2:
                    for (int i = 0; i < 14; i++)
                    {
                        if (Player2_CardVisibility != "Hidden")
                        {
                            Player2_CardVisibility = "Hidden";
                        }
                        else
                        {
                            Player2_CardVisibility = "Visible";
                        }
                        await Task.Delay(200);
                    }
                    break;
                case 3:
                    for (int i = 0; i < 14; i++)
                    {
                        if (Player3_CardVisibility != "Hidden")
                        {
                            Player3_CardVisibility = "Hidden";
                        }
                        else
                        {
                            Player3_CardVisibility = "Visible";
                        }
                        await Task.Delay(200);
                    }
                    break;
            }

            EndGame();
        }


        private void WinByFold(PokerPlayer player)
        {
            player.Balance += CurrentPot;
            MessageBox.Show($"{player.PokerName} heeft gewonnen!");
            GameState = "End";
            EndGame();
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
            if (player.Hand.Count > 1)
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
            RoundsSinceBlindsRaise++;
            CurrentPot = 0;
            await ClearTable();
            int currentDealer = 0;
            foreach (PokerPlayer player in Players)
            {
                if (!player.Busted)
                {
                    player.Bet = 0;
                    player.Hand.Clear();
                    player.Checked = false;
                    player.Folded = false;
                    player.IsAllIn = false;
                    if (player.Balance <= 0)
                    {
                        player.Busted = true;
                        NumOfActivePlayers--;
                    }
                }
                switch (NumOfActivePlayers)
                {
                    case 2:
                    case 3:
                        if (player.Button == PokerButton.SmallBlind)
                        {
                            currentDealer = player.PlayerID;
                        }
                        break;
                    default:
                        if (player.Button == PokerButton.Dealer)
                        {
                            currentDealer = player.PlayerID;
                        }
                        break;
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
                                    Players[newDealer].Button = PokerButton.SmallBlind;
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
            MessageBox.Show($"The Blinds have been raised\n" +
                            $"The Small Blind is now {BlindsBet}\n" +
                            $"The Big Blind is now {(BlindsBet * 2)}");
        }
    }
}
