
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace diary.ApiModels.DiaryController 
{
    public class AdjustDiaryPermissionRequest 
    {
        [JsonProperty("token")]
        [Required]
        public string Token;

        [JsonProperty("id")]
        [Required]
        public string Id;

        [JsonProperty("public")]
        [Required]
        public bool IsPublic;
    }
}