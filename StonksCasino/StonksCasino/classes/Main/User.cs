﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StonksCasino.classes.Api;

namespace StonksCasino.classes.Main
{
    public static class User
    {
        private static string _userName;

        public static string Username
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private static int _tokens;

        public static int Tokens
        {
            get { return _tokens; }
            set { _tokens = value; }
        }
        private static bool _logoutclick = false;

        public static bool Logoutclick
        {
            get { return _logoutclick; }
            set { _logoutclick = value; }
        }
        public static async Task<bool> LogoutAsync()
        {
            StonksCasino.Properties.Settings.Default.Username = "";
            StonksCasino.Properties.Settings.Default.Password = "";
            StonksCasino.Properties.Settings.Default.Save();
            User.Username = "";
            User.Tokens = 0;
          
            await ApiWrapper.Logout();
            return true;
        }

    }
}
