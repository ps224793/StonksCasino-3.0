using StonksCasino.classes.Api;
using StonksCasino.classes.Main;
using StonksCasino.enums.card;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StonksCasino.classes.blackjack
{
    public class BlackJack : PropertyChange
    {

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
                    SetPlayerHand(Players[0]);
                    Deals = false;
                    Tokendrop = false;
                    Dubbel = true;
                    Splitten = true;
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


        public async void Dubbelen()
        {
            await ApiWrapper.UpdateTokens(-MyAantal);
            Dubbel = false;
            MyAantal = MyAantal * 2;
            MessageBox.Show($"Het aantal Tokens is verdubbeld naar: { MyAantal }");
        }
        public void Splitte()
        {
            Splitten = false;
            MyAantal = MyAantal * 2;
            MyPlayerSplit = MyPlayerSplit / 2;
            MessageBox.Show($"Het aantal Tokens is verdubbeld naar: { MyAantal } en de kaarten zijn gesplit!");
        }

        public void Hits()
        {
            Hit = true;
            Players[0].AddCard(deck.DrawCard());
        }

        public void Stands()
        {
            Splitten = false;
            Dubbel = false;
            Standing = false;
            Hit = false;
            Deals = true;
            Tokendrop = true;
        }

        public async void Gamewin()
        {
            await ApiWrapper.UpdateTokens(MyAantal * 2);
        }

        public async void Gamedraw()
        {
            await ApiWrapper.UpdateTokens(MyAantal);
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
