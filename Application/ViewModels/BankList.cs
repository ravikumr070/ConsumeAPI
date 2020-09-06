using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
   public class BankList
    {

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public string[] Data { get; set; }

        [JsonProperty("message")]
        public object Message { get; set; }

    }
}
