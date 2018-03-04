using System;
using System.Diagnostics;
using System.Linq;
using diary.Models;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult UpdatePost(String token, DiaryPost post)
        {
            if (string.IsNullOrEmpty(post.Id.ToString()))
            {
                return View(new DiaryPost());
            }

            RestClient rc = new RestClient();
            var success = rc.Edit(token, post);
            
            if (post != null && success)
            {
                return View(post);
            }

            return NotFound();
        }

        //Create new post
        public IActionResult Edit(String token, DiaryPost post)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", post);
            }

            RestClient rc = new RestClient();
            var success = rc.Create(token, post);

            return Redirect(post.GetLink());
        }

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