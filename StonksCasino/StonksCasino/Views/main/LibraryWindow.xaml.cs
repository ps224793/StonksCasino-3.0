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

        private void Roulettegame()
        {
            Account();
            RouletteWindow roulette = new RouletteWindow();
            this.Hide();
            roulette.ShowDialog();
            this.Show();
        }

        private void Blackjack_click(object sender, RoutedEventArgs e)
        {
            Blackjackgame();
        }

        private void ImgBlackjack_Click(object sender, RoutedEventArgs e)
        {
            Blackjackgame();
        }

        private void Blackjackgame()
        {
            Account();
            BlackjackWindow blackjack = new BlackjackWindow();
            this.Hide();
            blackjack.ShowDialog();
            this.Show();
           
        }

        private void Poker_click(object sender, RoutedEventArgs e)
        {
            Pokergame();
        }

        private void ImgPoker_Click(object sender, RoutedEventArgs e)
        {
            Pokergame();
        }

        private void Pokergame()
        {
            Account();
            PokerWindow roulette = new PokerWindow();
            this.Hide();
            roulette.ShowDialog();
            this.Show();

        }

        private void SlotMachine_click(object sender, RoutedEventArgs e)
        {
            SlotMachinegame();
        }

        private void ImgSlotMachine_Click(object sender, RoutedEventArgs e)
        {
            SlotMachinegame();
        }

        private void SlotMachinegame()
        {
            Account();
            SlotmachineWindow roulette = new SlotmachineWindow();
            this.Hide();
            roulette.ShowDialog();
            this.Show();
        }


        private void HorseRace_click(object sender, RoutedEventArgs e)
        {
            HorseRacegame();
        }

        private void ImgHorseRace_Click(object sender, RoutedEventArgs e)
        {
            HorseRacegame();
        }

        private void HorseRacegame()
        {
            Account();
            horseracewindow horserace = new horseracewindow();
            this.Hide();
            horserace.ShowDialog();
            this.Show();
        }



        private async void Window_Closed(object sender, EventArgs e)
        {

            await ApiWrapper.Logout();
        }

        private async void Uitloggen_Click(object sender, RoutedEventArgs e)
        {
            StonksCasino.Properties.Settings.Default.Username = "";
            StonksCasino.Properties.Settings.Default.Password = "";
            StonksCasino.Properties.Settings.Default.Save();
            await ApiWrapper.Logout();
            User.Username = "";
            User.Tokens = 0;

            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }

        private void btnBibliotheek_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
