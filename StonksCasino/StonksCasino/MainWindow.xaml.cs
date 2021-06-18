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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StonksCasino.Views.main;
using StonksCasino.classes.Main;
using StonksCasino.classes.Api;
using StonksCasino.classes.Api.Models;
using System.Diagnostics;

namespace StonksCasino
{
    public partial class MainWindow : Window
    {
        private string _email;

        public string MyEmail
        {
            get { return _email; }
            set { _email = value; }
        }
        private bool _remember = false;

        public bool Remember
        {
            get { return _remember; }
            set { _remember = value; }
        }

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private async void Login()
        {
            LoginCredentials credentials = new LoginCredentials() { Email = MyEmail, Password = tbPassword.Password, Overwride = false };
            string result = await ApiWrapper.Login(credentials);
            User.Logoutclick = false;

            if (result == "succes")
            {
                if (Remember)
                {
                    RememberMe();
                }
                LibraryWindow libraryWindow = new LibraryWindow();
                this.Close();
                libraryWindow.Show();
            }
            else if (result == "active")
            {
                MessageBoxResult mes = MessageBox.Show("Er is al iemand anders ingelogd op dit account! Als u toch wilt inloggen wordt de ander van uw account afgezet. Let op! Dit kan nadelige gevolgen hebben voor uw account als de persoon die ingelogd is momenteel bezig is met een spel heb je het risico om je inzit kwijt te raken. Wilt u toch inloggen?", "Inloggen", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (mes == MessageBoxResult.Yes)
                {
                    credentials = new LoginCredentials() { Email = MyEmail, Password = tbPassword.Password, Overwride = true };
                    result = await ApiWrapper.Login(credentials);
                    if (Remember)
                    {
                        RememberMe();
                    }
                    LibraryWindow libraryWindow = new LibraryWindow();
                    this.Close();
                    libraryWindow.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Gebruikersnaam of wachtwoord is incorrect.");
            }
        }

        private void RememberMe()
        {
            Properties.Settings.Default.Username = MyEmail;
            Properties.Settings.Default.Password = tbPassword.Password;
            Properties.Settings.Default.Save();
        }

        private async void LoginRemember()
        {
            try
            {
                LoginCredentials credentials = new LoginCredentials() { Email = Properties.Settings.Default.Username, Password = Properties.Settings.Default.Password, Overwride = false };
                string result = await ApiWrapper.Login(credentials);
                
                if (result == "succes")
                {
                    LibraryWindow libraryWindow = new LibraryWindow();
                    this.Close();
                    libraryWindow.ShowDialog();
                }
            }
            catch
            {

            }
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginRemember();
        }

        private void Register_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://stonkscasino.nl/public/register");
        }

        private void Pass_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://stonkscasino.nl/public/password/reset");
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }
    }
}
