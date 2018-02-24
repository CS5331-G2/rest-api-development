
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diary.ApiModels.UsersController 
{
    public class ExpireUserResponse 
    {
        [JsonProperty("status")]
        public bool Status;
    }
}