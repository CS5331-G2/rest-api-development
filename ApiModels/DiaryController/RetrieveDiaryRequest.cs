
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace diary.ApiModels.DiaryController 
{
    public class RetrieveDiaryRequest 
    {
        [JsonProperty("token")]
        [Required]
        public string Token;
    }
}