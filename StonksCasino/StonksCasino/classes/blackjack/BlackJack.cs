using StonksCasino.classes.Api;
using StonksCasino.classes.Main;
using StonksCasino.enums.card;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StonksCasino.classes.blackjack
{
    public class BlackJack : PropertyChange
    {
        private const string _sender = "Blackjack";

        public Visibility SplitGrid
        {
            get { return _splitted ? Visibility.Visible : Visibility.Hidden; }
        }

        public Visibility NoSplitGrid
        {
            get { return _splitted ? Visibility.Hidden : Visibility.Visible; }
        }

        private bool _splitted = false;

        public bool Splitted
        {
            get { return _splitted; }
            set { _splitted = value; OnPropertyChanged("SplitGrid"); OnPropertyChanged("NoSplitGrid"); }
        }

        private int _aantal;

        public int MyAantal
        {
            get { return _aantal; }
            set { _aantal = value; OnPropertyChanged(); }
        }

        private int _PlayerSplit;

        public int MyPlayerSplit
        {
            get { return _PlayerSplit; }
            set { _PlayerSplit = value; OnPropertyChanged(); }
        }

        private bool _tokendrop;

        public bool Tokendrop
        {
            get { return _tokendrop; }
            set { _tokendrop = value; OnPropertyChanged(); }
        }

        private bool _dubbel;

        public bool Dubbel
        {
            get { return _dubbel; }
            set { _dubbel = value; OnPropertyChanged(); }
        }

        private bool _splitten;

        public bool Splitten
        {
            get { return _splitten; }
            set { _splitten = value; OnPropertyChanged(); }
        }

        private bool _stand;

        public bool Stand
        {
            get { return _stand; }
            set { _stand = value; OnPropertyChanged(); }
        }

        private bool _hit;

        public bool Hit
        {
            get { return _hit; }
            set { _hit = value; OnPropertyChanged(); }
        }

        private bool _deals;

        public bool Deals
        {
            get { return _deals; }
            set { _deals = value; OnPropertyChanged(); }
        }

        private bool _standing;

        public bool Standing
        {
            get { return _standing; }
            set { _standing = value; OnPropertyChanged(); }
        }

        private bool _splitstands = false;

        public bool Splitstands
        {
            get { return _splitstands; }
            set { _splitstands = value; OnPropertyChanged(); }
        }


        private BlackjackDeck deck = new BlackjackDeck();

        private List<BlackjackPlayer> _players;

        List<Main.CardBlackjack> cards = new List<Main.CardBlackjack>();

        public List<BlackjackPlayer> Players
        {
            get { return _players; }
            set { _players = value; OnPropertyChanged(); }
        }

        public BlackJack()
        {
            Players = new List<BlackjackPlayer>();
            Players.Add(new BlackjackPlayer());
        }


        public void Deal()
        {

            try
            {
                if (MyAantal > 0)
                {
                    Deals = false;
                    SetPlayerHand(Players[0]);
                    Tokendrop = false;
                    Dubbel = true;
                    Splitten = CheckSplit();
                    Standing = true;
                    Hit = true;

                    //MessageBox.Show($"Het aantal ingezette Tokens: { MyAantal }");
                }
                else
                {
                    MessageBox.Show("U moet minimaal 1 token inzetten om te kunnen spelen!");
                }
            }
            catch
            {
                MessageBox.Show("U moet minimaal 1 token inzetten om te kunnen spelen!");
            }
        }

        public void SetPlayerHand(BlackjackPlayer player)
        {
            cards.Add(deck.DrawCard());
            cards.Add(deck.DrawCard());
            player.SetHand(cards);

            //MyPlayerSplit = 1;
        }

        public bool CheckSplit()
        {
            return cards[0].Value == cards[1].Value;
        }

        public async void Dubbelen()
        {
            await ApiWrapper.UpdateTokens(-MyAantal, _sender);
            Dubbel = false;
            Hit = false;
            MyAantal = MyAantal * 2;
            MessageBox.Show($"Het aantal Tokens is verdubbeld naar: { MyAantal }");
        }
        public async void Splitte()
        {
            await ApiWrapper.UpdateTokens(-MyAantal, _sender);
            Splitten = false;
            MyAantal = MyAantal * 2;
            Players[0].Split(deck.DrawCard(), deck.DrawCard());
            Splitted = true;
            Players[0].Changescore2();
            Players[0].Update();
            MessageBox.Show($"Het aantal Tokens is verdubbeld naar: { MyAantal } en de kaarten zijn gesplit!");
        }

        public void Splitfunction2()
        {
            Splitstands = true;
        }

        public void Hits()
        {
            if (Splitted == false)
            {
                Splitten = false;
                Hit = true;
                Dubbel = false;
                Players[0].AddCard(deck.DrawCard());
            }
            else if (Splitted == true)
            {
                Hit = true;
                Dubbel = false;
                Players[0].AddCard(deck.DrawCard());
            }
        }

        public void Stands()
        {
            if (Splitted == true)
            {
                if (Splitstands == false)
                {
                    Splitstand();
                    Players[0].SplitRightHand();
                    Players[0].Changescore();
                    Players[0].Update2();
                }
                else if (Splitstands == true)
                {
                    Splitstand();
                    Standing = false;
                    Hit = false;
                    Deals = true;
                    Tokendrop = true;
                    Splitstands = false;
                }
            }
            else
            {
                Splitstand();
                Deals = true;
                Tokendrop = true;
                Standing = false;
                Hit = false;
            }
        }

        public void Blackjackcomputerens()
        {
            Hit = false;
            Dubbel = false;
            Splitten = false;
        }

        public void Stand21()
        {
            Hit = false;
            Standing = false;
            Dubbel = false;
        }

        public void Splitstand()
        {
            Splitten = false;
            Dubbel = false;
        }

        public async void Gamewin()
        {
            await ApiWrapper.UpdateTokens(MyAantal * 2, _sender);
        }

        public async void Gamedraw()
        {
            await ApiWrapper.UpdateTokens(MyAantal, _sender);
        }

        public async void Gamewin2()
        {
            await ApiWrapper.UpdateTokens(MyAantal * 2, _sender);
        }

        public async void Gamedraw2()
        {
            await ApiWrapper.UpdateTokens(MyAantal, _sender);
        }

        public void Gameclear()
        {
            Players[0].GameOver();
        }

        public void Blackjackwindow()
        {
            Deals = true;
            Tokendrop = true;
            Splitten = false;
            Dubbel = false;
            Standing = false;
            Hit = false;
        }

        //public void HitStop()
        //{
        //    Hit = false;
        //}
    }
}
