
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.UsersController 
{
    public class RegisterUserRequest 
    {
        [JsonProperty("username")]
        public string Username;

        [JsonProperty("password")]
        public string Password;

        [JsonProperty("fullname")]
        public string FullName;

        [JsonProperty("age")]
        public int Age;
    }
}