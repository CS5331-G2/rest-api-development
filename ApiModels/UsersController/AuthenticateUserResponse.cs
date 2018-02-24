
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.UsersController 
{
    public class AuthenticateUserResponse 
    {
        [JsonProperty("status")]
        public bool Status;

        [JsonProperty("token")]
        public string Token;
    }
}