using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StonksCasino.classes.Api.Models
{
    class ApiAccessToken
    {
        [JsonProperty("accessToken")]
        public Int64 AccessToken
        {
            get { return GlobalApiToken.AccessToken; }
        }

        [JsonProperty("userId")]
        public int UserId
        {
            get { return GlobalApiToken.UserId; }
        }
    }
}
