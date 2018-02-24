
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.UsersController 
{
    public class AuthenticateUserRequest 
    {
        [JsonProperty("username")]
        public string Username;

        [JsonProperty("password")]
        public string Password;
    }
}