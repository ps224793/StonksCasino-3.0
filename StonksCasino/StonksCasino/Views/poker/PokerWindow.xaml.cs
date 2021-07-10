using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StonksCasino.classes.poker;
using StonksCasino.Views.main;
using StonksCasino.classes.Main;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Animation;
using System.Data;
using StonksCasino.classes.Api;
using System.Diagnostics;

namespace StonksCasino.Views.poker
{
    /// <summary>
    /// Interaction logic for PokerWindow.xaml
    /// </summary>
    public partial class PokerWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private PokerGame _game;

        public PokerGame Game
        {
            get { return _game; }
            set { _game = value; OnPropertyChanged(); }
        }

        private int _cardWidth;

        public int CardWidth
        {
            get { return _cardWidth; }
            set { _cardWidth = value; OnPropertyChanged(); }
        }

        public string Username
        {
            get { return User.Username; }
        }

        public int Tokens
        {
            get { return User.Tokens; }
        }

        private bool back2Library = false;
        public PokerWindow()
        {
            Game = new PokerGame();
            DataContext = this;
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetCardWidth();
        }

        public void SetCardWidth()
        {
            CardWidth = (int)one.ActualWidth;
        }

        private void Raise_Bet(object sender, RoutedEventArgs e)
        {
            if (Game.Players[0].Balance >= Game.Players[0].RaiseBet && Game.Players[0].Balance >= Game.TopBet)
            {
                Game.Raise(Game.Players[0]);
            }
            else
            {
                Game.AllIn(Game.Players[0]);
            }
        }

        private void Higher_Raise(object sender, RoutedEventArgs e)
        {
            if (Game.Players[0].Balance > Game.Players[0].RaiseBet)
            {
                Game.Players[0].RaiseBet++;
            }
        }

        private void Lower_Raise(object sender, RoutedEventArgs e)
        {
            if (Game.Players[0].RaiseBet > Game.LastRaise + (Game.TopBet - Game.Players[0].Bet))
            {
                Game.Players[0].RaiseBet--;
            }
        }

        private void btnFold_Click(object sender, RoutedEventArgs e)
        {
            Game.Fold(Game.Players[0]);
        }

        private void Call_Click(object sender, RoutedEventArgs e)
        {
            int MaxBet = Game.Players[0].Bet + Game.Players[0].Balance;
            if (MaxBet <= Game.TopBet)
            {
                Game.AllIn(Game.Players[0]);
            }
            else if (Game.Players[0].Bet < Game.TopBet)
            {
                Game.Call(Game.Players[0]);
            }
            else
            {
                Game.Check(Game.Players[0]);
            }
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!User.Logoutclick)
            {
                if (this.IsActive == true && !back2Library)
                {
                    MessageBoxResult leaving = MessageBox.Show("Weet u zeker dat u de applicatie wil afsluiten", "Afsluiten", MessageBoxButton.YesNo);
                    if (leaving == MessageBoxResult.No)
                    {
                        e.Cancel = true;
                    }
                    else if (leaving == MessageBoxResult.Yes)
                    {
                        if (!back2Library)
                        {
                            User.shutdown = false;
                            await ApiWrapper.Logout();
                            Application.Current.Shutdown();
                        }
                    }
                }
            }
        }

        public async void btnBibliotheek_Click(object sender, RoutedEventArgs e)
        {
            if (Game.Players[0].Balance > 0)
            {
                await ApiWrapper.UpdateTokens(Game.Players[0].Balance, Game.Sender);
            }
            back2Library = true;
            this.Close();

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (User.Tokens >= 500)
            {
                await ApiWrapper.UpdateTokens(-500, Game.Sender);
                await ApiWrapper.GetUserInfo();
                OnPropertyChanged("Tokens");
                Game.setupNewGame();
                await Task.Delay(1000);
                Game.StartGame();
                await Task.Delay(1);
                SetCardWidth();
                await Task.Delay(800);
                Storyboard board = (Storyboard)FindResource("sbTableIn");
                board.Begin();

                await Task.Delay(3000);

                Game.StartGame2();
            }
            else
            {
                MessageBox.Show("U heeft niet genoeg tokens.");
            }
        }

        private void Uitloggen_Click(object sender, RoutedEventArgs e)
        {
            User.Logoutclick = true;
            this.Close();
        }

        private void BtnGeldStorten_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://stonkscasino.nl/public/account-info");

        }

        private async void Button_Click_Yes(object sender, RoutedEventArgs e)
        {
            Game.NextRoundButton();
            await Task.Delay(2000);
            Game.StartGame();
            await Task.Delay(1);
            SetCardWidth();
            await Task.Delay(800);
            Storyboard board = (Storyboard)FindResource("sbTableIn");
            board.Begin();

            await Task.Delay(3000);

            Game.StartGame2();
        }
    }
}
