using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.Api
{
    public static class GlobalApiToken
    {
        private static Int64 _accessToken = 0;

        public static Int64 AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }

        private static int _userId = 0;

        public static int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

    }
}
