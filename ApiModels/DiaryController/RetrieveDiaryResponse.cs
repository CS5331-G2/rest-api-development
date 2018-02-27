
using System.Collections.Generic;
using diary.Models;
using Newtonsoft.Json;

namespace diary.ApiModels.DiaryController 
{
    public class RetrieveDiaryResponse 
    {
        [JsonProperty("status")]
        public bool Status;

        [JsonProperty("result")]
        public List<Diary> Result;
    }
}