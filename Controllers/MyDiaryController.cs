using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using diary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diary.Controllers
{
    public class MyDiaryController : Controller
    {
        public IActionResult Index()
        {
            RestClient rc = new RestClient();
            //change to token
            var posts = rc.findAllAsync(User.Identity.Name).Result.Select(p => new DiaryPost
            {
                Id = p.Id,
                Title = p.Title,
                Author = p.Author,
                PublishDate = p.PublishDate,
                IsPublic = p.IsPublic,
                Text = p.Text,
            });

            return View(posts);
        }

        //Change permission
        [HttpPost]
        public IActionResult UpdatePost(DiaryPost post)
        {
            if (string.IsNullOrEmpty(post.Id.ToString()))
            {
                return View(new DiaryPost());
            }

            RestClient rc = new RestClient();
            var success = rc.Edit(HttpContext.Session.GetString(SessionState.SessionKeyToken), post);
            
            if (post != null && success)
            {
                return View(post);
            }

            return NotFound();
        }

        //Create new post
        [HttpGet]
        public IActionResult Edit(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new DiaryPost());
        }

        //Create new post
        [HttpPost]
        public IActionResult Edit(DiaryPost post)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", post);
            }

            RestClient rc = new RestClient();
            var success = rc.Create(HttpContext.Session.GetString(SessionState.SessionKeyToken), post);

            return Redirect("/MyDiary");
        }

        [HttpPost]
        public IActionResult Delete(String token, int id)
        {
            RestClient rc = new RestClient();
            rc.Delete(token, id);
            return Redirect("/MyDiary");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}