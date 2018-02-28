
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.DiaryController 
{
    public class CreateDiaryResultModel : ApiResultModel 
    {
        [JsonProperty("id")]
        public string Id;
    }
}