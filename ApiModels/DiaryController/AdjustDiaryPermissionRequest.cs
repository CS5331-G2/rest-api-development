
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.DiaryController 
{
    public class AdjustDiaryPermissionRequest 
    {
        [JsonProperty("token")]
        public string Token;

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("private")]
        public bool IsPrivate;
    }
}