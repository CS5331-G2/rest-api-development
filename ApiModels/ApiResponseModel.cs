
using Newtonsoft.Json;

namespace diary.ApiModels 
{
    public class ApiResponseModel
    {
        [JsonProperty("status")]
        public bool Status;

        [JsonProperty("result", NullValueHandling=NullValueHandling.Ignore)]
        public ApiResultModel Result;

        [JsonProperty("error", NullValueHandling=NullValueHandling.Ignore)]
        public string Error;
    }
}