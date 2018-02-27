
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.DiaryController 
{
    public class CreateDiaryResponse 
    {
        [JsonProperty("status")]
        public bool Status;

        [JsonProperty("result", NullValueHandling=NullValueHandling.Ignore)]
        public string Result;

        [JsonProperty("error", NullValueHandling=NullValueHandling.Ignore)]
        public string Error;
    }
}