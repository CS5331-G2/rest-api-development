
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.DiaryController 
{
    public class CreateDiaryRequest 
    {
        [JsonProperty("token")]
        public string Token;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("public")]
        public bool IsPublic;

        [JsonProperty("text")]
        public string Text;
    }
}