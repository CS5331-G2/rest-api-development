
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.MetaController 
{
    public class MembersResponse 
    {
        [JsonProperty("status")]
        public bool Status;

        [JsonProperty("result")]
        public List<string> Result;
    }
}