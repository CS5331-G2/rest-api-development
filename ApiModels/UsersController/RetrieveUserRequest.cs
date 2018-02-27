
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace diary.ApiModels.UsersController 
{
    public class RetrieveUserRequest 
    {
        [JsonProperty("token")]
        [Required]
        public string Token;
    }
}