
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.UsersController 
{
    public class ExpireUserRequest 
    {
        [JsonProperty("token")]
        public string Token;
    }
}