using System;
using System.Collections.Generic;
using System.Data;
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
using StonksCasino.classes.Main;
using StonksCasino.Views.Roulette;
using StonksCasino.Views.blackjack;
using StonksCasino.Views.poker;
using StonksCasino.Views.slotmachine;
using StonksCasino.Views.horserace;
using StonksCasino.classes.Api;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace StonksCasino.Views.main
{
    public partial class LibraryWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string Username 
        {
            get { return User.Username; }
        }

        public int Tokens 
        {
            get { return User.Tokens; }
        }


        public LibraryWindow()
        {
            Account();
            DataContext = this;
            InitializeComponent();
        }
        private async void Account()
        {
            bool result = await ApiWrapper.GetUserInfo();
            OnPropertyChanged("Username");
            OnPropertyChanged("Tokens");
            if(!result)
            {
                Application.Current.Shutdown();
            }
        }

        private void Roullete_click(object sender, RoutedEventArgs e)
        {
            Roulettegame();
        }

        private void ImgRoulette_Click(object sender, RoutedEventArgs e)
        {
            Roulettegame();
        }

        private async void Roulettegame()
        {
            Account();
            RouletteWindow roulette = new RouletteWindow();
            this.Hide();
            roulette.ShowDialog();
            await uitloggencheck();            
        }

        private void Blackjack_click(object sender, RoutedEventArgs e)
        {
            Blackjackgame();
        }

        private void ImgBlackjack_Click(object sender, RoutedEventArgs e)
        {
            Blackjackgame();
        }

        private async void Blackjackgame()
        {
            Account();
            BlackjackWindow blackjack = new BlackjackWindow();
            this.Hide();
            blackjack.ShowDialog();
            await uitloggencheck();

        }

        private void Poker_click(object sender, RoutedEventArgs e)
        {
            Pokergame();
        }

        private void ImgPoker_Click(object sender, RoutedEventArgs e)
        {
            Pokergame();
        }

        private async void Pokergame()
        {
            Account();
            PokerWindow roulette = new PokerWindow();
            this.Hide();
            roulette.ShowDialog();
            await uitloggencheck();
            await ApiWrapper.GetUserInfo();
            OnPropertyChanged("Tokens");
        }

        private void SlotMachine_click(object sender, RoutedEventArgs e)
        {
            SlotMachinegame();
        }

        private void ImgSlotMachine_Click(object sender, RoutedEventArgs e)
        {
            SlotMachinegame();
        }

        private async void SlotMachinegame()
        {
            Account();
            SlotmachineWindow roulette = new SlotmachineWindow();
            this.Hide();
            roulette.ShowDialog();
            await uitloggencheck();
        }


        private void HorseRace_click(object sender, RoutedEventArgs e)
        {
            HorseRacegame();
        }

        private void ImgHorseRace_Click(object sender, RoutedEventArgs e)
        {
            HorseRacegame();
        }

        private async void HorseRacegame()
        {
            Account();
            horseracewindow horserace = new horseracewindow();
            this.Hide();
            horserace.ShowDialog();
            await uitloggencheck();
        }


        private async void Uitloggen_Click(object sender, RoutedEventArgs e)
        {
            bool logout = await User.LogoutAsync();
            if (logout)
            {
                MainWindow login = new MainWindow();
                this.Close();
                login.Show();
            }
        }

        private void btnBibliotheek_Click(object sender, RoutedEventArgs e)
        {

        }
        public async Task uitloggencheck()
        {
            if (User.Logoutclick)
            {
                bool logout = await User.LogoutAsync();
                if (logout)
                {
                    MainWindow login = new MainWindow();
                    this.Close();
                    login.Show();
                }
            }
            else
            {
                this.Show();
            }
        }

        private async void Window_Closing(object sender, CancelEventArgs e)
        {
            if (User.Logoutclick == false)
            {
                if (User.shutdown)
                {
                    MessageBoxResult leaving = MessageBox.Show("Weet u zeker dat u de applicatie wil afsluiten", "Afsluiten", MessageBoxButton.YesNo);
                    if (leaving == MessageBoxResult.No)
                    {
                        e.Cancel = true;
                    }
                    else if (leaving == MessageBoxResult.Yes)
                    {

                        bool uitloggen =  await ApiWrapper.Logout();
                        if (uitloggen)
                        {
                            Application.Current.Shutdown();
                        }
                        


                    }
                }

            }
        }

        private async void Window_Closed(object sender, EventArgs e)
        {
            await ApiWrapper.Logout();
        }

        private void BtnGeldStorten_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://stonkscasino.nl/public/account-info");
        }
    }
}
