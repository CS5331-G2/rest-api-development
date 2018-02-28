
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.UsersController 
{
    public class RetrieveUserResultModel : ApiResultModel 
    {
        [JsonProperty("username")]
        public string Username;

        [JsonProperty("fullname")]
        public string Fullname;

        [JsonProperty("age")]
        public int Age;
    }
}