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
using System.Windows.Threading;

namespace StonksCasino.Views.slotmachine
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        DispatcherTimer Timer = new DispatcherTimer();

        BitmapImage[] _Image = new BitmapImage[5];
        Random _Random = new Random();
        public HelpWindow()
        {
            InitializeComponent();
            ArrayImage();
            ConfigTimer();
        }

        private void ArrayImage()
        {
            _Image[0] = new BitmapImage(new Uri("/Img/Slotmachine/Cherry.png", UriKind.Relative));
            _Image[1] = new BitmapImage(new Uri("/Img/Slotmachine/Grape.png", UriKind.Relative));
            _Image[2] = new BitmapImage(new Uri("/Img/Slotmachine/Melon.png", UriKind.Relative));
            _Image[3] = new BitmapImage(new Uri("/Img/Slotmachine/Orange.png", UriKind.Relative));
        }
        private void ConfigTimer()
        {
            Timer.Interval = TimeSpan.FromSeconds(2);
            Timer.Tick += Tick;
            Timer.Start();
        }
        private void Tick(object sender, System.EventArgs e)
        {
            int i = _Random.Next(0, 4);
            if (Fruit.Source != _Image[i] && Fruit1.Source != _Image[i] && Fruit2.Source != _Image[i])
            {
                Fruit.Source = _Image[i];
                Fruit1.Source = _Image[i];
                Fruit2.Source = _Image[i];
            }
            else
            {
                i = _Random.Next(0, 4);
                if (Fruit.Source != _Image[i] && Fruit1.Source != _Image[i] && Fruit2.Source != _Image[i])
                {
                    Fruit.Source = _Image[i];
                    Fruit1.Source = _Image[i];
                    Fruit2.Source = _Image[i];
                }
                else
                {
                    i = _Random.Next(0, 4);
                    if (Fruit.Source != _Image[i] && Fruit1.Source != _Image[i] && Fruit2.Source != _Image[i])
                    {
                        Fruit.Source = _Image[i];
                        Fruit1.Source = _Image[i];
                        Fruit2.Source = _Image[i];
                    }
                }
            }
        }
    }
}
