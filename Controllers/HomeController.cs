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

            //APIClient apiClient = new APIClient();
            //var test = apiClient.GetDiary("http://localhost:8080/api/diary");
            //Console.Write(test.IsPublic);

            APIClient newClient = new APIClient();
            var test2 = newClient.PostGetDiary("http://localhost:8080/api/diary", "6bf00d02-dffc-4849-a635-a21b08500d61");
            //Console.Write(test2.Title);

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
