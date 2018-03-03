using System;
using System.Net.Http;
using System.Threading.Tasks;
using diary.Models;

namespace diary.Controllers
{
    public class APIClient
    {
        static HttpClient client = new HttpClient();

        public Diary GetDiary(string path)
        {
            Diary diary = null;
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.Write(response);
            }

            return diary;
        }

    }
}
