using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public  class IFSC
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("STATE")]
        public string State { get; set; }

        [JsonProperty("BANK")]
        public string Bank { get; set; }

        [JsonProperty("IFSC")]
        public string Ifsc { get; set; }

        [JsonProperty("BRANCH")]
        public string Branch { get; set; }

        [JsonProperty("ADDRESS")]
        public string Address { get; set; }

        [JsonProperty("CONTACT")]
        public string Contact { get; set; }

        [JsonProperty("CITY")]
        public string City { get; set; }

        [JsonProperty("DISTRICT")]
        public string District { get; set; }

        [JsonProperty("MICRCODE")]
       
        public long Micrcode { get; set; }
    }
}
