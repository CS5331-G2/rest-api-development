
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.DiaryController 
{
    public class DeleteDiaryRequest 
    {
        [JsonProperty("token")]
        public string Token;

        [JsonProperty("id")]
        public string Id;
    }
}