using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public  class IFSCList
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public IFSC[] Data { get; set; }

        [JsonProperty("message")]
        public object Message { get; set; }
    }
}
