
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.UsersController 
{
    public class AuthenticateUserResultModel : ApiResultModel
    {
        [JsonProperty("token")]
        public string Token;
    }
}