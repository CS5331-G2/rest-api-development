
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace diary.ApiModels.DiaryController 
{
    public class CreateDiaryRequest 
    {
        [JsonProperty("token")]
        [Required]
        public string Token;

        [JsonProperty("title")]
        [Required]
        public string Title;

        [JsonProperty("public")]
        [Required]
        public bool IsPublic;

        [JsonProperty("text")]
        [Required]
        public string Text;
    }
}