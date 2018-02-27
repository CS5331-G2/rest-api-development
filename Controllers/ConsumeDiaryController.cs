using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using diary.Models;
using System.Net.Http.Headers;

namespace diary.Controllers
{
    public class ConsumeDiaryController : Controller
    {
        public async IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("diary");

            return View();

        }
    }
}
