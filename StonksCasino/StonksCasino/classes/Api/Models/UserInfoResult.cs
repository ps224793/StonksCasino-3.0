using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.Api.Models
{
    class UserInfoResult
    {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }
        
        [JsonProperty("tokens")]
        public int Tokens { get; set; }

        [JsonProperty("selectedSkin")]
        public string SelectedCardSkin { get; set; }
    }
}
