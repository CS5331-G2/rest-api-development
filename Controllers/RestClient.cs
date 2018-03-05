using diary.ApiModels.DiaryController;
using diary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using diary.ApiModels.UsersController;
using Microsoft.AspNetCore.Mvc;
using diary.ApiModels;
using diary.ApiModels.MetaController;

namespace diary.Controllers
{
    public class RestClient
    {
        private static string BASE_URL = "http://localhost:" + diary.Program.WEBAPI_PORT + "/api";

        public async Task<bool> HeartbeatAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BASE_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("meta/heartbeat").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ApiResponseModel>(data).Status;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<String>> MembersAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BASE_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("meta/members").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<MembersResponse>(data).Result;
                }

                return new List<String>();
            }
            catch
            {
                return new List<String>();
            }
        }

        public async Task<IEnumerable<Diary>> FindAllAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BASE_URL);
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
                client.BaseAddress = new Uri(BASE_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                RetrieveDiaryRequest diaryRequest = new RetrieveDiaryRequest
                {
                    Token = token
                };

                HttpResponseMessage response = client.PostAsync("diary", new StringContent(JsonConvert.SerializeObject(diaryRequest), Encoding.UTF8, "application/json")).Result;
                
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
                client.BaseAddress = new Uri(BASE_URL);
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
                client.BaseAddress = new Uri(BASE_URL);
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
                client.BaseAddress = new Uri(BASE_URL);
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
        public bool Register(RegisterUserRequest user)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BASE_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsync("users/register", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Login(AuthenticateUserRequest user)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BASE_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsync("users/authenticate", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    string body = await response.Content.ReadAsStringAsync();
                    ApiResponseModel responseModel = JsonConvert.DeserializeObject<ApiResponseModel>(body);
                    return responseModel.Status;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }
    }
}
