
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.UsersController 
{
    public class RetrieveUserResponse 
    {
        [JsonProperty("status")]
        public bool Status;

        [JsonProperty("username")]
        public string Username;

        [JsonProperty("fullname")]
        public string Fullname;

        [JsonProperty("age")]
        public int Age;

        [JsonProperty("error", NullValueHandling=NullValueHandling.Ignore)]
        public string Error;
    }
}