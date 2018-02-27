
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.DiaryController 
{
    public class AdjustDiaryPermissionResponse 
    {
        [JsonProperty("status")]
        public bool Status;

        [JsonProperty("error", NullValueHandling=NullValueHandling.Ignore)]
        public string Error;
    }
}