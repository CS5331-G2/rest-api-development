
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.DiaryController 
{
    public class RetrieveDiaryRequest 
    {
        [JsonProperty("username")]
        public string Username;

        [JsonProperty("password")]
        public string Password;
    }
}