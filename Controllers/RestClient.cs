using diary.ApiModels.DiaryController;
using diary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace diary.Controllers
{
    public class RestClient
    {
        private string Base_URL = "http://localhost/api/";

        public async Task<IEnumerable<Diary>> FindAllAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("diary").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RetrieveDiaryResponse>(data).Result;
                }
                return new List<Diary>();
            }
            catch
            {
                return new List<Diary>();
            }
        }

        public async Task<IEnumerable<Diary>> findAllAsync(String token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                RetrieveDiaryRequest diaryRequest = new RetrieveDiaryRequest
                {
                    Token = token
                };

                HttpResponseMessage response = client.PostAsync("diary/", new StringContent(JsonConvert.SerializeObject(diaryRequest), Encoding.UTF8, "application/json")).Result;
                
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RetrieveDiaryResponse>(data).Result;
                }
                return new List<Diary>();
            }
            catch
            {
                return new List<Diary>();
            }
        }

        public bool Create(String token, DiaryPost post)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                CreateDiaryRequest diaryRequest = new CreateDiaryRequest
                {
                    Token = token,
                    Title = post.Title,
                    IsPublic = post.IsPublic,
                    Text = post.Text
                };

                HttpResponseMessage response = client.PostAsync("diary/create", new StringContent(JsonConvert.SerializeObject(diaryRequest), Encoding.UTF8, "application/json")).Result;

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(String token, DiaryPost post)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                AdjustDiaryPermissionRequest diaryRequest = new AdjustDiaryPermissionRequest
                {
                    Token = token,
                    Id = post.Id.ToString(),
                    IsPublic = post.IsPublic
                };

                HttpResponseMessage response = client.PostAsync("diary/permission", new StringContent(JsonConvert.SerializeObject(diaryRequest), Encoding.UTF8, "application/json")).Result;

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(String token, int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                DeleteDiaryRequest diaryRequest = new DeleteDiaryRequest
                {
                    Token = token,
                    Id = id.ToString()
                };

                HttpResponseMessage response = client.PostAsync("diary/delete", new StringContent(JsonConvert.SerializeObject(diaryRequest), Encoding.UTF8, "application/json")).Result;

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
