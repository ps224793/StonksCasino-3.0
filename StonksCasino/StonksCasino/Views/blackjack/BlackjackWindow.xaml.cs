using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;
using StonksCasino.classes.Api;
using StonksCasino.classes.blackjack;
using StonksCasino.classes.Main;
using StonksCasino.Views.main;

namespace StonksCasino.Views.blackjack
{
    /// <summary>
    /// Interaction logic for BlackjackWindow.xaml
    /// </summary>
    public partial class BlackjackWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool back2Library = false;

        private CardBlackjack _cardturned;

        public CardBlackjack Mycardturned
        {
            get { return _cardturned; }
            set { _cardturned = value; OnPropertyChanged(); }
        }

        private BlackjackDeck deck = new BlackjackDeck();

        private BlackJack _game;

        public BlackJack Game
        {
            get { return _game; }
            set { _game = value; OnPropertyChanged(); }
        }

        private Computers _player;

        public Computers Playergame
        {
            get { return _player; }
            set { _player = value; OnPropertyChanged(); }
        }

        private Computers _computer;

        public Computers ComputerGame
        {
            get { return _computer; }
            set { _computer = value; OnPropertyChanged(); }
        }

        private Computers _computer2;

        public Computers ComputerGame2
        {
            get { return _computer2; }
            set { _computer2 = value; OnPropertyChanged(); }
        }

        public string Username
        {
            get { return User.Username; }
        }

        public int Tokens
        {
            get { return User.Tokens; }
        }

        DispatcherTimer computertimer = new DispatcherTimer();

        public BlackjackWindow()
        {
            BlackjackWindowRestart();          
            DataContext = this;
            
            InitializeComponent();

            computertimer.Interval = TimeSpan.FromMilliseconds(1);
            computertimer.Tick += computertimer_Tick;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Account();
        }

        private async void Account()
        {
            bool result = await ApiWrapper.GetUserInfo();
            OnPropertyChanged("Username");
            OnPropertyChanged("Tokens");
            if (!result)
            {
                Application.Current.Shutdown();
            }
        }

        private void Bibliotheek_Click(object sender, EventArgs e)
        {
            back2Library = true;
            this.Close();       
        }

        private void BlackjackWindowRestart()
        {
            Game = new BlackJack();
            Game.Blackjackwindow();
            computertimer.Start();
            Account();
        }

        private void computertimer_Tick(object sender, EventArgs e)
        {
            ComputerGame = new Computers();
            computertimer.Stop();
        }

        private Computers _computers = new Computers();

        public Computers MyComputers
        {
            get { return _computers; }
            set { _computers = value; OnPropertyChanged(); }
        }

        public async void Deal_click(object sender, RoutedEventArgs e)
        {

            int MyAantal = Game.MyAantal;
            if (MyAantal <= User.Tokens && MyAantal > 0)
            {
                bool result = await ApiWrapper.UpdateTokens(MyAantal * -1);
                if (result)
                {
                    Game.Deal();
                    await ApiWrapper.GetUserInfo();
                }
                else
                {
                    MessageBox.Show("Er is ergens anders met dit account ingelogd. U wordt nu uitgelogd.");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("U heeft niet genoeg tokens om te kunnen spelen!");
            }
            Account();

        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {

            Game.Hits();
            int Player = Game.Players[0].Score;

            if (Player > 21)
            {
                Game.Stands();
                ComputerGame.ComputerDeal(Player);
                Endresult();
            }

        }

        private async void Dubbelen_Click(object sender, RoutedEventArgs e)
        {
            bool ingelogd = await Checkingelogd();
            if (ingelogd)
            {
                int MyAantal = Game.MyAantal;
                if (MyAantal <= User.Tokens)
                {
                    Game.Dubbelen();
                    Game.Hits();
                    await ApiWrapper.GetUserInfo();
                    int Player = Game.Players[0].Score;
                    if (Player > 21)
                    {
                        Game.Stands();
                        ComputerGame.ComputerDeal(Player);
                        Endresult();
                    }

                }
                else
                {
                    MessageBox.Show("U heeft niet genoeg tokens om te kunnen spelen!");
                }
            }
            Account();
        }

        private async void Splitten_Click(object sender, RoutedEventArgs e)
        {
            bool ingelogd = await Checkingelogd();
            if (ingelogd)
            {
                Game.Splitte();
                await ApiWrapper.GetUserInfo();
            }
            Account();
        }

        private async void Stand_Click(object sender, RoutedEventArgs e)
        {
            bool ingelogd = await Checkingelogd();
            if (ingelogd)
            {
                int Player = Game.Players[0].Score;
                Game.Stands();
                ComputerGame.ComputerDeal(Player);
                Endresult();
                Account();
            }
        }

        private async void Endresult()
        {            
            try
            {
                int bot = ComputerGame.Computer[0].ScoreC;
                int Player = Game.Players[0].Score;

                if (Player == 21 && bot < 21 )
                {
                    MessageBox.Show("Je hebt gewonnen!");
                    Game.Gamewin();
                }
                else if (bot > 21 && Player > 21)
                {
                    MessageBox.Show("Allebij verloren!");
                }
                else if (bot > 21 && Player <= 21)
                {
                    MessageBox.Show("Je hebt gewonnen!");
                    Game.Gamewin();
                }
                else if (Player > bot && Player <= 21)
                {
                    MessageBox.Show("Je hebt gewonnen!");
                    Game.Gamewin();
                }
                else if (Player > 21 && bot <= 21)
                {
                    MessageBox.Show("Je hebt verloren!");
                }
                else if (bot > Player && bot <= 21)
                {
                    MessageBox.Show("Je hebt verloren!");
                }
                else if (bot == Player)
                {
                    MessageBox.Show("Het is gelijkspel!");
                    Game.Gamedraw();
                }

                else
                {
                    MessageBox.Show("Fout!!!!");
                }
                Game.Gameclear();
                ComputerGame.GameclearComputer();
                await ApiWrapper.GetUserInfo();
                BlackjackWindowRestart();
                Account();
            }
            catch
            {
                Account();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
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
                   Application.Current.Shutdown();
                 
                }
            }
        }

        private async Task<bool> Checkingelogd()
        {
            bool result = await ApiWrapper.CheckLogin();

            if (!result)
            {
                StonksCasino.Properties.Settings.Default.Username = "";
                StonksCasino.Properties.Settings.Default.Password = "";
                StonksCasino.Properties.Settings.Default.Save();
                User.Username = "";
                User.Tokens = 0;

                MainWindow window = new MainWindow();

                MessageBox.Show("Er is door iemand anders ingelogd op het account waar u momenteel op speelt. Hierdoor wordt u uitgelogd");
                this.Close();
                window.Show();

                return false;
            }
            return true;
        }

        private async void Uitloggen_Click(object sender, RoutedEventArgs e)
        {
            StonksCasino.Properties.Settings.Default.Username = "";
            StonksCasino.Properties.Settings.Default.Password = "";
            StonksCasino.Properties.Settings.Default.Save();
            await ApiWrapper.Logout();
            User.Username = "";
            User.Tokens = 0;

            this.Close();
        }
    }
}
