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
        private string Base_URL = "http://localhost/api";

        public async Task<IEnumerable<PostSummaryViewModel.PostSummaryModel>> findAllAsync()
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
                    return JsonConvert.DeserializeObject<List<PostSummaryViewModel.PostSummaryModel>>(data);
                }
                return new List<PostSummaryViewModel.PostSummaryModel>();
            }
            catch
            {
                return new List<PostSummaryViewModel.PostSummaryModel>();
            }
        }
        public async Task<PostSummaryViewModel.PostSummaryModel> findAsync(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("diary/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PostSummaryViewModel.PostSummaryModel>(data);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool Create(PostSummaryViewModel.PostSummaryModel post)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsync("diary", new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json")).Result;

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(PostSummaryViewModel.PostSummaryModel post)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsync("diary/" + post.Author, new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json")).Result;


                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.DeleteAsync("diary/" + id).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
