using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using diary.Models;

namespace diary.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            PostSummaryViewModel postSModel = new PostSummaryViewModel();

            Func<Diary, bool> postFilter = p => p.IsPublic;

            RestClient rc = new RestClient();

            IEnumerable<Diary> postModels = rc.FindAllAsync().Result.Where(postFilter);

            postSModel.PostSummaries = postModels.Select(p => new PostSummaryViewModel.PostSummaryModel
            {
                Id = p.Id,
                Title = p.Title,
                Author = p.Author,
                PublishDate = p.PublishDate,
                IsPublic = p.IsPublic,
                Text = p.Text,
            });
    
            return View(postSModel);
        }

        public IActionResult About()
        {
            RestClient rc = new RestClient();
            var isUP = rc.HeartbeatAsync();

            if(isUP.Result)
            {
                ViewData["Message"] = "Web API Service is up.";
            }
            else
            {
                ViewData["Message"] = "Web API Service is down.";
            }
            return View();
        }

        public IActionResult Contact()
        {
            RestClient rc = new RestClient();
            var isUP = rc.MembersAsync();

            if (isUP.Result.Count != 0)
            {
                ViewData["Message"] = "This is done by a team of " + isUP.Result.Count+ "  NUS students.";
                ViewData["List"] = isUP.Result;
            }
            else
            {
                ViewData["Message"] = "Web API Service is down.";
            }

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
