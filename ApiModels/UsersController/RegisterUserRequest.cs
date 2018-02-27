
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace diary.ApiModels.UsersController 
{
    public class RegisterUserRequest 
    {
        [JsonProperty("username")]
        [Required]
        public string Username;

        [JsonProperty("password")]
        [Required]
        public string Password;

        [JsonProperty("fullname")]
        [Required]
        public string FullName;

        [JsonProperty("age")]
        [Required]
        public int Age;
    }
}