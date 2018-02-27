
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace diary.ApiModels.UsersController 
{
    public class ExpireUserRequest 
    {
        [JsonProperty("token")]
        [Required]
        public string Token;
    }
}