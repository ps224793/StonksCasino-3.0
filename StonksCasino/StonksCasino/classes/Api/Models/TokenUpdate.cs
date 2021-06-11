using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.Api.Models
{
    class TokenUpdate
    {
        [JsonProperty("credentials")]
        public ApiAccessToken ApiAccessToken
        {
            get { return new ApiAccessToken(); }
        }

        [JsonProperty("tokens")]
        public int Tokens { get; set; }

        [JsonProperty("sender")]
        public string Sender { get; set; }

        public TokenUpdate(int tokens, string sender)
        {
            Tokens = tokens;
            Sender = sender;
        }


    }
}
