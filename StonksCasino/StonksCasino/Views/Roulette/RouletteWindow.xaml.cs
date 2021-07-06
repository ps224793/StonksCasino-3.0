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
using StonksCasino.classes.Main;
using StonksCasino.classes.Roulette;
using StonksCasino.Views.main;

namespace StonksCasino.Views.Roulette
{
    /// <summary>
    /// Interaction logic for RouletteWindow.xaml
    /// </summary>
    public partial class RouletteWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool _play = true;

        public bool play
        {
            get { return _play; }
            set { _play = value; OnPropertyChanged(); }
        }


        private const string _sender = "Roulette";

        public string Username 
        {
            get { return User.Username; }
        }

        public int Tokens 
        { 
            get { return User.Tokens; }
        }

        private bool _toLibrary = false;

        private Bettingtable _bettingtable = new Bettingtable();

        int _betAmount;

        public Bettingtable MyBettingTable
        {
            get { return _bettingtable; }
            set { _bettingtable = value; OnPropertyChanged(); }
        }

        private Bet _myamount = new Bet();

        public Bet MyAmount
        {
            get { return _myamount; }
            set { _myamount = value; }
        }

        Random _random = new Random();

        bool _Spinning = false;

        

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
        int _value = 0;
        int _Finalnumber;
        
        int _valuedisplay;
        bool _canbet = true;
        bool _display = true;
        DispatcherTimer _timerbet = new DispatcherTimer();
        DispatcherTimer _timerdisplay = new DispatcherTimer();
        DispatcherTimer _timerbal = new DispatcherTimer();

        public RouletteWindow()
        {
            
            Account();
            DataContext = this;
            configTimer();

            InitializeComponent();
            

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
        private async Task<bool> Checkingelogd()
        {
            return true;
        }

        private void configTimer()
        {
            _timerbal.Interval = TimeSpan.FromMilliseconds(100);
            _timerbal.Tick += _timerbal_Tick;
            _timerbet.Interval = TimeSpan.FromSeconds(1);
            _timerbet.Tick += _timerbet_Tick;
            _timerdisplay.Interval = TimeSpan.FromSeconds(1);
            _timerdisplay.Tick += _timerdisplay_Tick;
        }

        private void _timerbal_Tick(object sender, EventArgs e)
        {
            if (imBal.Margin.Top < 0)
            {
                imBal.Margin = new Thickness(0, imBal.Margin.Top + 2, 0, 0);
                imBal.RenderTransformOrigin = new Point(0.5, imBal.RenderTransformOrigin.Y - 0.001);
            }
            if(imBal.Margin.Top == 0)
            {
                imBal.RenderTransformOrigin = new Point(0.5, 0.5);
                _timerbal.Stop();
                

            }
        }

        private void _timerdisplay_Tick(object sender, EventArgs e)
        {
            if (_valuedisplay == 3)
            {
                MyBettingTable.Resetbet();
                _valuedisplay = 0;
                _display = true;
                _timerdisplay.Stop();
            }
            _valuedisplay++;
        }

        private void _timerbet_Tick(object sender, EventArgs e)
        {
            if (_value == 3)
            {
                _canbet = false;
                _value = 0;
                _timerbet.Stop();
                
            }
            _value++;
        }

        private async void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            play = false;
            bool ingelogd = await Checkingelogd();
            if (ingelogd)
            {

                imBal.RenderTransformOrigin = new Point(0.5, 0.59);
                imBal.Margin = new Thickness(0, -100, 0, 0);
                _timerbet.Start();
                
                _Spinning = true;
                int[] _score = new int[] { 0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 20 };
                Angle = 0;
                Storyboard storyboard = new Storyboard();
                storyboard.Duration = new Duration(TimeSpan.FromSeconds(8.0));
                double angle = _random.Next(1800, 3600);
                DoubleAnimation rotateAnimation = new DoubleAnimation()
                {
                    From = Angle,
                    To = angle,
                    Duration = storyboard.Duration,
                    AccelerationRatio = 0.1,
                    DecelerationRatio = 0.5
                };
                Angle += angle;
                Angle = Angle % 360;
                Storyboard.SetTarget(rotateAnimation, imWheel);
                Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));


                storyboard.Children.Add(rotateAnimation);
                storyboard.Begin();



                //---------------------
                Angle2 = 0;
                int random1 = _random.Next(0, 36);
                Storyboard storyboard2 = new Storyboard();
                storyboard2.Completed += Storyboard2_Completed;
                storyboard2.Duration = new Duration(TimeSpan.FromSeconds(8.0));
                double angle2 = 9.72972973 * random1 + -3600 + Angle;
                DoubleAnimation rotateAnimation2 = new DoubleAnimation()
                {
                    From = Angle2,
                    To = angle2,
                    Duration = storyboard2.Duration,
                    AccelerationRatio = 0.4,
                    DecelerationRatio = 0.2
                };
                Angle2 += angle2;
                Angle2 = Angle2 % 360;
                Storyboard.SetTarget(rotateAnimation2, imBal);
                Storyboard.SetTargetProperty(rotateAnimation2, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));


                storyboard2.Children.Add(rotateAnimation2);
                storyboard2.Begin(this);
                _timerbal.Start();

                _Finalnumber = _score[random1];
               
                


            }
        }

        private async void Storyboard2_Completed(object sender, EventArgs e)
        {
            int totelwin = MyBettingTable.Checkwin(_Finalnumber);
            MyAmount.MyTotalinzet = 0;
            if (totelwin > 0)
            {
                MessageBox.Show("Gefeliciteerd u hebt € " + totelwin.ToString() + " Gewonnen");
                await ApiWrapper.UpdateTokens(totelwin, _sender);

            }
            _display = false;
            _timerdisplay.Start();
            _canbet = true;
            Account();
            _Spinning = false;
            play = true;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            bool ingelogd = await Checkingelogd();
            if (ingelogd)
            {
                if (_display == false)
                {
                    MyBettingTable.Resetbet();
                    _valuedisplay = 0;
                    _display = true;
                    _timerdisplay.Stop();
                }
                if (_canbet)
                {


                    if (_betAmount > 0)
                    {

                        if (User.Tokens >= _betAmount)
                        {
                            MyAmount.Addtotal(_betAmount);
                            Button bt = sender as Button;
                            ((Bet)bt.Tag).SetBet(_betAmount);
                            await ApiWrapper.UpdateTokens(-_betAmount, _sender);
                            Account();
                        }
                        else
                        {
                            MessageBox.Show("U heeft niet genoeg tokens om in te zetten");
                        }
                    }
                    else
                    {
                        MessageBox.Show("U kunt geen fiche van 0 inzetten");
                    }
                }
                else
                {
                    MessageBox.Show("U kunt nu niet meer inzetten. Wacht tot er een nummer is gevallen");
                }
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
        
                if (_canbet)
                {
                    Button bt = sender as Button;
                    ((Bet)bt.Tag).PreviewBet();
                    bool Chip = ((Bet)bt.Tag).Set;
                    if (Chip == true)
                    {
                        bt.ToolTip = ((Bet)bt.Tag).AmountLabel;
                    }
                    else
                    {
                        bt.ToolTip = null;
                    }
                }
            

        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            

                Button bt = sender as Button;
                ((Bet)bt.Tag).dePreviewBet();
            
        }



        private async void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool ingelogd = await Checkingelogd();
            if (ingelogd) {
                if (_canbet)
                {
                    Button bt = sender as Button;
                    double opacity = ((Bet)bt.Tag).Opacity;
                    if (opacity == 1)
                    {
                        int amount = ((Bet)bt.Tag).Amount;
                        ((Bet)bt.Tag).DeleteBet();
                        await ApiWrapper.UpdateTokens(amount, _sender);
                        MyAmount.RemoveTotal(amount);
                        Account();
                    }

                }
            }
            else
            {
                MessageBox.Show("u kunt uw ingezetten fiches niet meer weg halen. Wacht tot er een nummer is gevallen");
            }
        }

        private async void Fiche_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool ingelogd = await Checkingelogd();
            if (ingelogd)
            {
                TextBox text = sender as TextBox;
                int inttext = 0;


                try
                {
                    inttext = int.Parse(text.Text);
                }
                catch (Exception)
                {


                }
                _betAmount = inttext;

                if (text.Text.Length < 4)
                {
                    text.FontSize = 20;
                }
                if (text.Text.Length >= 4)
                {
                    text.FontSize = 15;
                }
                if (text.Text.Length >= 5)
                {
                    text.FontSize = 12;
                }
                if (text.Text.Length >= 6)
                {
                    text.FontSize = 10;
                }


            }

        }



        private void MaskNumericInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextIsNumeric(e.Text);
        }

        private void MaskNumericPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string input = (string)e.DataObject.GetData(typeof(string));
                if (!TextIsNumeric(input)) e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }

        private bool TextIsNumeric(string input)
        {
            return input.All(c => Char.IsDigit(c) || Char.IsControl(c));
        }

        private async void plus_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool ingelogd = await Checkingelogd();
            if (ingelogd)
            {
                MyAmount.Plusinzet();
            }
        }

        private async void min_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool ingelogd = await Checkingelogd();
            if (ingelogd)
            {
                if (_betAmount > 0)
                {
                    MyAmount.Mininzet();
                }
            }
        }

        private async void Window_Closing(object sender, CancelEventArgs e)
        {
            if (User.Logoutclick == false)
            {


                if (_Spinning)
                {

                    if (MyAmount.MyTotalinzet > 0)
                    {

                        MessageBoxResult spinning = MessageBox.Show("De roulette tafel is aan het draaien. Als u nu de applicatie afsluit dan bent u uw ingezetten fiches kwijt", "Weet u zeker dat u wil weggaan?", MessageBoxButton.OKCancel);
                        if (spinning == MessageBoxResult.Cancel)
                        {
                            e.Cancel = true;
                        }
                        else if (spinning == MessageBoxResult.OK)
                        {
                            if (!_toLibrary)
                            {
                                User.shutdown = false;
                                await ApiWrapper.Logout();
                                Application.Current.Shutdown();
                            }
                        }
                    }
                }

                {
                    if (MyAmount.MyTotalinzet > 0)
                    {

                        MessageBoxResult Leave = MessageBox.Show("U heeft geld ingezet. Als u nu de applicatie afsluit worden uw fiches wel teruggegeven", "Weet u zeker dat u wil weggaan?", MessageBoxButton.OKCancel);
                        if (Leave == MessageBoxResult.OK)
                        {
                            await ApiWrapper.UpdateTokens(MyAmount.MyTotalinzet, _sender);

                            Account();

                            if (!_toLibrary)
                            {
                                User.shutdown = false;
                                await ApiWrapper.Logout();
                                Application.Current.Shutdown();
                            }

                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        if (this.IsActive == true && !_toLibrary)
                        {
                            MessageBoxResult leaving = MessageBox.Show("Weet u zeker dat u de applicatie wil afsluiten", "Afsluiten", MessageBoxButton.YesNo);
                            if (leaving == MessageBoxResult.No)
                            {
                                e.Cancel = true;
                            }
                            else if (leaving == MessageBoxResult.Yes)
                            {
                                if (!_toLibrary)
                                {
                                    User.shutdown = false;
                                    await ApiWrapper.Logout();
                                    Application.Current.Shutdown();
                                }

                            }

                        }

                    }
                }
            }
            }

        private void btnBibliotheek_Click(object sender, RoutedEventArgs e)
        {
            _toLibrary = true;
            this.Close();
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
    }
    }

