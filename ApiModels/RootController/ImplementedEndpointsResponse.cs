
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.RootController 
{
    public class ImplementedEndpointsResponse
    {
        [JsonProperty("status")]
        public bool Status;

        [JsonProperty("result")]
        public List<string> Result;
    }
}