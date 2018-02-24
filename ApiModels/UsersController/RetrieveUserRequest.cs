
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.UsersController 
{
    public class RetrieveUserRequest 
    {
        [JsonProperty("token")]
        public string Token;
    }
}