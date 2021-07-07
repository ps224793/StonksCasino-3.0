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
using StonksCasino.classes.Api;
using StonksCasino.classes.blackjack;
using StonksCasino.classes.Main;
using StonksCasino.classes.Roulette;
using StonksCasino.classes.Slotmachine;
using StonksCasino.Views.main;

namespace StonksCasino.Views.slotmachine
{
    /// <summary>
    /// Interaction logic for SlotmachineWindow.xaml
    /// </summary>
    public partial class SlotmachineWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private const string _sender = "Slotmachine";

        public string Username
        {
            get { return User.Username; }
        }

        public int Tokens
        {
            get { return User.Tokens; }
        }

        private bool back2Library = false;

        DispatcherTimer computertimer = new DispatcherTimer();

        private int _beurt = 0;

        public int Beurt
        {
            get { return _beurt; }
            set { _beurt = value; OnPropertyChanged(); }
        }

        private bool _allowedToPull = true;

        public bool AllowedToPull
        {
            get { return _allowedToPull; }
            set { _allowedToPull = value; OnPropertyChanged(); }
        }

        private bool _allowedtoclickVH;

        public bool AllowedToClickVH
        {
            get { return _allowedtoclickVH; }
            set { _allowedtoclickVH = value; OnPropertyChanged(); }
        }

        private bool _allowedtoclickVL;

        public bool AllowedToClickVL
        {
            get { return _allowedtoclickVL; }
            set { _allowedtoclickVL = value; OnPropertyChanged(); }
        }


        public SlotmachineWindow()
        {
            DataContext = this;
            InitializeComponent();
            computertimer.Interval = TimeSpan.FromMilliseconds(1);
            computertimer.Tick += computertimer_Tick;

            Check();
            Account();
        }

        private void computertimer_Tick(object sender, EventArgs e)
        {
            computertimer.Stop();
        }

        private void Bibliotheek_Click(object sender, EventArgs e)
        {
            back2Library = true;
            this.Close();
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

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpTab();
        }
        private void HelpTab()
        {
            HelpWindow roulette = new HelpWindow();
            roulette.Show();
        }

        private Bet _myamount = new Bet();

        public Bet MyAmount
        {
            get { return _myamount; }
            set { _myamount = value; }
        }

        public void Check()
        {
            if (User.Tokens < 100 || Beurt >= 10)
            {
                AllowedToClickVH = false;
            }
            else
            {
                AllowedToClickVH = true;
            }
            if (Beurt <= 0)
            {
                AllowedToClickVL = false;
                AllowedToPull = false;
            }
            else
            {
                AllowedToClickVL = true;
                AllowedToPull = true;
            }
        }

        public async void Verhogen_Click(object sender, RoutedEventArgs e)
        {
            await ApiWrapper.UpdateTokens(-50, _sender);
            Account();
            Beurt++;
            Check();
        }
        private async void Verlagen_Click(object sender, RoutedEventArgs e)
        {
            await ApiWrapper.UpdateTokens(50, _sender);
            Account();
            Beurt--;
            Check();
        }

        private Slotmachine _slotmachine = new Slotmachine();

        public Slotmachine Slotmachine
        {
            get { return _slotmachine; }
            set { _slotmachine = value; OnPropertyChanged(); }
        }







        private double _angle = 0;

        public double Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        private double _angle2 = 0;

        public double Angle2
        {
            get { return _angle2; }
            set { _angle2 = value; }
        }

        DispatcherTimer _timerbet = new DispatcherTimer();

        private void DisableButtons()
        {
            AllowedToPull = false;
            AllowedToClickVH = false;
            AllowedToClickVL = false;
        }


        private async void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            _timerbet.Start();
            Beurt--;

           

            Angle = 0;
            Storyboard storyboard = new Storyboard();
            storyboard.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            storyboard.Completed += Storyboard_Completed;
            double angle = 70;
            DoubleAnimation rotateAnimation = new DoubleAnimation()
            {
                From = Angle,
                To = angle,
                Duration = storyboard.Duration,
                AccelerationRatio = 0.5,
                DecelerationRatio = 0.5
            };
            Angle += angle;
            Angle = Angle % 360;
            Storyboard.SetTarget(rotateAnimation, imgLever);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));

            storyboard.Children.Add(rotateAnimation);
            storyboard.Begin();
            await Slotmachine.Activate();

            int winnings = Slotmachine.CheckWin();
            if (winnings>0)
            {
                MessageBox.Show($"u heeft {winnings} gewonnen");
                await ApiWrapper.UpdateTokens(winnings, _sender);
            }
            Check();
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {

            _timerbet.Start();
            Angle2 = 70;
            Storyboard storyboard2 = new Storyboard();
            storyboard2.Completed += Storyboard2_Completed;
            storyboard2.Duration = new Duration(TimeSpan.FromSeconds(1.0));

            double angle2 = 0;
            DoubleAnimation rotateAnimation = new DoubleAnimation()
            {
                From = Angle2,
                To = angle2,
                Duration = storyboard2.Duration,
                AccelerationRatio = 0.5,
                DecelerationRatio = 0.5
            };
            Angle += angle2;
            Angle = Angle % 360;
            Storyboard.SetTarget(rotateAnimation, imgLever);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));


            storyboard2.Children.Add(rotateAnimation);
            storyboard2.Begin();

            //--------------------------------------
        }
        private void Storyboard2_Completed(object sender, EventArgs e)
        {

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

        private void BtnGeldStorten_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://stonkscasino.nl/public/account-info");

        }
    }
}
