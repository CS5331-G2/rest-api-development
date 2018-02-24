
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.MetaController 
{
    public class HeartbeatResponse 
    {
        [JsonProperty("status")]
        public bool Status;
    }
}