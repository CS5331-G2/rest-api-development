using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using diary.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace diary.Controllers
{
    public class APIClient
    {
        static readonly HttpClient client = new HttpClient();

        public class AuthToken
        {
            [JsonProperty("token")]
            public string Token { get; set; }
        }

        public Diary GetDiary(string path)
        {
            Diary diary = null;
            var response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                diary = JsonConvert.DeserializeObject<Diary>(response.Content.ReadAsStringAsync().Result);
                Console.Write(JsonConvert.DeserializeObject<Diary>(response.Content.ReadAsStringAsync().Result));
            }
            return diary;
        }

        public Diary PostGetDiary(string path, string token)
        {
            Diary diary = null;
            AuthToken authtoken = new AuthToken()
            {
                Token = token
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(authtoken));
            var response = client.PostAsync(path, content);
            Console.Write(content);
            Console.Write(response);
            return diary;
        }

    }
}
