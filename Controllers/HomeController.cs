using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using diary.Models;

namespace diary.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Posts()
        {
            ViewData["Message"] = "This is diary post page.";

            APIClient getpost = new APIClient();
            getpost.GetDiary("http://localhost:8080/api/diary");

            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:8080/api/diary");

            //client.DefaultRequestHeaders.Accept.Add(
            //    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            //HttpResponseMessage response = client.GetAsync("http://localhost:8080/api/diary").Result;

            //if (response.IsSuccessStatusCode)
            //{
            //    var dataObjects = response.Content.ReadAsStringAsync().Result;
            //    foreach (var d in dataObjects)
            //    {
            //        Console.WriteLine("{0}", d);
            //    }
            //}

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
