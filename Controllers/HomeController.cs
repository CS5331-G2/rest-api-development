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
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
