
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace diary.ApiModels.UsersController 
{
    public class AuthenticateUserRequest 
    {
        [JsonProperty("username")]
        [Required]
        public string Username;

        [JsonProperty("password")]
        [Required]
        public string Password;
    }
}