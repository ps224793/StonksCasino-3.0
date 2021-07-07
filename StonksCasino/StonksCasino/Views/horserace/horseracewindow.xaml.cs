using StonksCasino.classes.Api;
using StonksCasino.classes.HorseRace;
using StonksCasino.classes.Main;
using StonksCasino.Views.main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using XamlAnimatedGif;

namespace StonksCasino.Views.horserace
{
    /// <summary>
    /// Interaction logic for horseracewindow.xaml
    /// </summary>
    public partial class horseracewindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private const string _sender = "Horserace";

        private HorseGame _game;

        public HorseGame Game
        {
            get { return _game; }
            set { _game = value; OnPropertyChanged(); }
        }

        private int _line;

        public int Line
        {
            get { return _line; }
            set { _line = value; OnPropertyChanged(); }
        }

        private int _horseracer;

        public int Horseracer
        {
            get { return _horseracer; }
            set { _horseracer = value; OnPropertyChanged(); }
        }

        private int _horseracer2;

        public int Horseracer2
        {
            get { return _horseracer2; }
            set { _horseracer2 = value; OnPropertyChanged(); }
        }

        private int _horseracer3;

        public int Horseracer3
        {
            get { return _horseracer3; }
            set { _horseracer3 = value; OnPropertyChanged(); }
        }

        private int _horseracer4;

        public int Horseracer4
        {
            get { return _horseracer4; }
            set { _horseracer4 = value; OnPropertyChanged(); }
        }

        private string _actiontime;

        public string ActionTime
        {
            get { return _actiontime; }
            set { _actiontime = value; OnPropertyChanged(); }
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

        DispatcherTimer HorseTimer = new DispatcherTimer();

        Random rndhorsenumber = new Random();

        int HorseNumber1Total = 0;
        int HorseNumber2Total = 0;
        int HorseNumber3Total = 0;
        int HorseNumber4Total = 0;

        int stamina1 = 100;
        int stamina2 = 100;
        int stamina3 = 100;
        int stamina4 = 100;

        int HorseChosen = 0;


        public horseracewindow()
        {
            InitializeComponent();
            Game = new HorseGame();
            DataContext = this;
            Account();

            Game.ButtonsHorse();
            HorseTimers();
        }

        private void Bibliotheek_Click(object sender, EventArgs e)
        {
            back2Library = true;
            this.Close();

        }

        public async void Horse1_Click(object sender, RoutedEventArgs e)
        {
            int MyAantal = Game.MyHorseBet;
            if (MyAantal <= User.Tokens && MyAantal > 0)
            {
                bool result = await ApiWrapper.UpdateTokens(MyAantal * -1, _sender);
                Game.HorseBet();
                HorseChosen = 1;
                HorseTimer.Start();
                ControllerhorsesPlay();
                ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                MessageBox.Show("U heeft niet genoeg tokens om te kunnen spelen!");
            }
            Account();
        }

        private async void Horse2_Click(object sender, RoutedEventArgs e)
        {
            int MyAantal = Game.MyHorseBet;
            if (MyAantal <= User.Tokens && MyAantal > 0)
            {
                bool result = await ApiWrapper.UpdateTokens(MyAantal * -1, _sender);
                Game.HorseBet();
                HorseChosen = 2;
                HorseTimer.Start();
                ControllerhorsesPlay();
                ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                MessageBox.Show("U heeft niet genoeg tokens om te kunnen spelen!");
            }
            Account();
        }

        private async void Horse3_Click(object sender, RoutedEventArgs e)
        {
            int MyAantal = Game.MyHorseBet;
            if (MyAantal <= User.Tokens && MyAantal > 0)
            {
                bool result = await ApiWrapper.UpdateTokens(MyAantal * -1, _sender);
                Game.HorseBet();
                HorseChosen = 3;
                HorseTimer.Start();
                ControllerhorsesPlay();
                ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                MessageBox.Show("U heeft niet genoeg tokens om te kunnen spelen!");
            }
            Account();
        }

        private async void Horse4_Click(object sender, RoutedEventArgs e)
        {
            int MyAantal = Game.MyHorseBet;
            if (MyAantal <= User.Tokens && MyAantal > 0)
            {
                bool result = await ApiWrapper.UpdateTokens(MyAantal * -1, _sender);
                Game.HorseBet();
                HorseChosen = 4;
                HorseTimer.Start();
                ControllerhorsesPlay();
                ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                MessageBox.Show("U heeft niet genoeg tokens om te kunnen spelen!");
            }
            Account();
        }

        private void HorseTimer_Tick(object sender, EventArgs e)
        {
            Line = (int)Linewidth.ActualWidth;

            Storyboard board = (Storyboard)this.FindResource("sonicRaces");
            board.Begin();

            Storyboard board2 = (Storyboard)this.FindResource("horseRaces2");
            board2.Begin();

            Storyboard board3 = (Storyboard)this.FindResource("horseRaces3");
            board3.Begin();

            Storyboard board4 = (Storyboard)this.FindResource("horseRaces4");
            board4.Begin();

            if (HorseNumber1Total < Line && HorseNumber2Total < Line && HorseNumber3Total < Line && HorseNumber4Total < Line)
            {
                HorsePlus();
            }
            else
            {
                HorseTimer.Stop();
                HorseActionTime();
                HorseChecker();
            }
        }

        public async void HorseChecker()
        {
            if (HorseNumber1Total > Line && HorseNumber2Total < Line && HorseNumber3Total < Line && HorseNumber4Total < Line)
            {
                MessageBox.Show("Horse nummer 1 heeft gewonnen!");

                if (HorseChosen == 1)
                {
                    MessageBox.Show("Je hebt gewonnen");
                    await ApiWrapper.UpdateTokens(Game.MyHorseBet * 2, _sender);
                }
            }
            else if (HorseNumber1Total > HorseNumber2Total && HorseNumber1Total > HorseNumber3Total && HorseNumber1Total > HorseNumber4Total)
            {
                MessageBox.Show("Horse nummer 1 heeft gewonnen!");

                if (HorseChosen == 1)
                {
                    MessageBox.Show("Je hebt gewonnen");
                    await ApiWrapper.UpdateTokens(Game.MyHorseBet * 2, _sender);
                }
            }

            if (HorseNumber2Total > Line && HorseNumber1Total < Line && HorseNumber3Total < Line && HorseNumber4Total < Line)
            {
                MessageBox.Show("Horse nummer 2 heeft gewonnen!");

                if (HorseChosen == 2)
                {
                    MessageBox.Show("Je hebt gewonnen");
                    await ApiWrapper.UpdateTokens(Game.MyHorseBet * 2, _sender);
                }
            }
            else if (HorseNumber2Total > HorseNumber1Total && HorseNumber2Total > HorseNumber3Total && HorseNumber2Total > HorseNumber4Total)
            {
                MessageBox.Show("Horse nummer 2 heeft gewonnen!");

                if (HorseChosen == 2)
                {
                    MessageBox.Show("Je hebt gewonnen");
                    await ApiWrapper.UpdateTokens(Game.MyHorseBet * 2, _sender);
                }
            }

            if (HorseNumber3Total > Line && HorseNumber1Total < Line && HorseNumber2Total < Line && HorseNumber4Total < Line)
            {
                MessageBox.Show("Horse nummer 3 heeft gewonnen!");

                if (HorseChosen == 3)
                {
                    MessageBox.Show("Je hebt gewonnen");
                    await ApiWrapper.UpdateTokens(Game.MyHorseBet * 2, _sender);
                }
            }
            else if (HorseNumber3Total > HorseNumber1Total && HorseNumber3Total > HorseNumber2Total && HorseNumber3Total > HorseNumber4Total)
            {
                MessageBox.Show("Horse nummer 3 heeft gewonnen!");

                if (HorseChosen == 3)
                {
                    MessageBox.Show("Je hebt gewonnen");
                    await ApiWrapper.UpdateTokens(Game.MyHorseBet * 2, _sender);
                }
            }

            if (HorseNumber4Total > Line && HorseNumber1Total < Line && HorseNumber2Total < Line && HorseNumber3Total < Line)
            {
                MessageBox.Show("Horse nummer 4 heeft gewonnen!");

                if (HorseChosen == 4)
                {
                    MessageBox.Show("Je hebt gewonnen");
                    await ApiWrapper.UpdateTokens(Game.MyHorseBet * 2, _sender);
                }
            }
            else if (HorseNumber4Total > HorseNumber1Total && HorseNumber4Total > HorseNumber2Total && HorseNumber4Total > HorseNumber3Total)
            {
                MessageBox.Show("Horse nummer 4 heeft gewonnen!");

                if (HorseChosen == 4)
                {
                    MessageBox.Show("Je hebt gewonnen");
                    await ApiWrapper.UpdateTokens(Game.MyHorseBet * 2, _sender);
                }
            }
            Account();
            Game.ButtonsHorse();
            Restart();
        }

        public void HorsesClear()
        {
            HorseNumber1Total = 0; HorseNumber2Total = 0; HorseNumber3Total = 0; HorseNumber4Total = 0; Line = 0;
            Horseracer = 0; Horseracer2 = 0; Horseracer3 = 0; Horseracer4 = 0;
        }

        public void HorseTimers()
        {
            HorseTimer.Interval = TimeSpan.FromSeconds(1);
            HorseTimer.Tick += HorseTimer_Tick;
        }

        public void HorseActionTime()
        {
            if (HorseNumber1Total > HorseNumber2Total && HorseNumber1Total > HorseNumber3Total && HorseNumber1Total > HorseNumber4Total)
            {
                ActionTime = "Nummer 1 heeft de leiding!";
            }
            else if (HorseNumber2Total > HorseNumber1Total && HorseNumber2Total > HorseNumber3Total && HorseNumber2Total > HorseNumber4Total)
            {
                ActionTime = "Nummer 2 heeft de leiding!";
            }
            else if (HorseNumber3Total > HorseNumber1Total && HorseNumber3Total > HorseNumber2Total && HorseNumber3Total > HorseNumber4Total)
            {
                ActionTime = "Nummer 3 heeft de leiding!";
            }
            else if (HorseNumber4Total > HorseNumber1Total && HorseNumber4Total > HorseNumber2Total && HorseNumber4Total > HorseNumber3Total)
            {
                ActionTime = "Nummer 4 heeft de leiding!";
            }
        }

        public void HorsePlus()
        {
            HorseActionTime();

            Horsetimer1();
            Horsetimer2();
            Horsetimer3();
            Horsetimer4();

            //HorseTimes.Horsetimer1();
        }

        public void Horsetimer1()
        {
            int HorseNumber1 = 0;

            if (stamina1 > 80)
            {
                HorseNumber1 = rndhorsenumber.Next(20, 150);
                stamina1 -= 10;
            }
            else if (stamina1 > 50)
            {
                HorseNumber1 = rndhorsenumber.Next(20, 60);
                stamina1 -= 10;
            }
            else if (HorseNumber1Total == Line)
            {
                stamina1 -= 10;
            }
            else
            {
                HorseNumber1 = rndhorsenumber.Next(20, 50);
                stamina1 += 60;
            }
            HorseNumber1Total += HorseNumber1;
            Horseracer = HorseNumber1Total;
        }

        public void Horsetimer2()
        {
            int HorseNumber2 = 0;
            if (stamina2 > 80)
            {
                HorseNumber2 = rndhorsenumber.Next(20, 100);
                stamina2 -= 10;
            }
            else if (stamina2 > 50)
            {
                HorseNumber2 = rndhorsenumber.Next(20, 80);
                stamina2 -= 10;
            }
            else if (HorseNumber2Total == Line)
            {
                stamina1 -= 10;
            }
            else
            {
                HorseNumber2 = rndhorsenumber.Next(20, 40);
                stamina2 += 60;
            }
            HorseNumber2Total += HorseNumber2;
            Horseracer2 = HorseNumber2Total;
        }

        public void Horsetimer3()
        {
            int HorseNumber3 = 0;

            if (stamina3 > 80)
            {
                HorseNumber3 = rndhorsenumber.Next(20, 150);
                stamina3 -= 10;
            }
            else if (stamina3 > 50)
            {
                HorseNumber3 = rndhorsenumber.Next(20, 60);
                stamina3 -= 10;
            }
            else if (HorseNumber3Total == Line)
            {
                stamina1 -= 10;
            }
            else
            {
                HorseNumber3 = rndhorsenumber.Next(20, 50);
                stamina3 += 60;
            }
            HorseNumber3Total += HorseNumber3;
            Horseracer3 = HorseNumber3Total;
        }

        public void Horsetimer4()
        {
            int HorseNumber4 = 0;

            if (stamina4 > 80)
            {
                HorseNumber4 = rndhorsenumber.Next(20, 100);
                stamina4 -= 10;
            }
            else if (stamina4 > 50)
            {
                HorseNumber4 = rndhorsenumber.Next(20, 80);
                stamina4 -= 10;
            }
            else if (HorseNumber4Total == Line)
            {
                stamina1 -= 10;
            }
            else
            {
                HorseNumber4 = rndhorsenumber.Next(20, 40);
                stamina4 += 60;
            }
            HorseNumber4Total += HorseNumber4;
            Horseracer4 = HorseNumber4Total;
        }

        public void Restart()
        {
            Line = 0;
            Line = Line;

            HorsesClear();

            Storyboard board = (Storyboard)this.FindResource("sonicRaces");
            board.Stop();

            Storyboard board2 = (Storyboard)this.FindResource("horseRaces2");
            board2.Stop();

            Storyboard board3 = (Storyboard)this.FindResource("horseRaces3");
            board3.Stop();

            Storyboard board4 = (Storyboard)this.FindResource("horseRaces4");
            board4.Stop();

            ActionTime = "";

            ResizeMode = ResizeMode.CanResize;
        }

        public void ControllerhorsesPlay()
        {
            Animator controller = AnimationBehavior.GetAnimator(sonicGif);
            controller.Play();

            Animator controller2 = AnimationBehavior.GetAnimator(horseGif2);
            controller2.Play();

            Animator controller3 = AnimationBehavior.GetAnimator(horseGif3);
            controller3.Play();

            Animator controller4 = AnimationBehavior.GetAnimator(horseGif4);
            controller4.Play();
        }

        private void Uitloggen_Click(object sender, RoutedEventArgs e)
        {
            User.Logoutclick = true;
            this.Close();
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
        private async void Window_Closing(object sender, CancelEventArgs e)
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
                        User.shutdown = false;
                        await ApiWrapper.Logout();
                        Application.Current.Shutdown();

                    }
                }
            }
        }

        private void BtnGeldStorten_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://stonkscasino.nl/public/account-info");

        }
    }
}
