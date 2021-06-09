using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
