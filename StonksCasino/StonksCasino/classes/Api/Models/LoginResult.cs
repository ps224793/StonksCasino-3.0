using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.Api.Models
{
    public class LoginResult
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("accessToken")]
        public Int64 AccessToken { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }
    }
}
