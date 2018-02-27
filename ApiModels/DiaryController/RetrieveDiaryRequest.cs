
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.DiaryController 
{
    public class RetrieveDiaryRequest 
    {
        [JsonProperty("token")]
        public string Token;
    }
}