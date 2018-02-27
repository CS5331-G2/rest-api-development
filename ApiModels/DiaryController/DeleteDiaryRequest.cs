
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace diary.ApiModels.DiaryController 
{
    public class DeleteDiaryRequest 
    {
        [JsonProperty("token")]
        [Required]
        public string Token;

        [JsonProperty("id")]
        [Required]
        public string Id;
    }
}