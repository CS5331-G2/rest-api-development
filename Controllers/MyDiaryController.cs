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

        public IActionResult Delete(int id)
        {
            RestClient rc = new RestClient();
            rc.Delete(id);
            return Redirect("/MyDiary");
        }

        public IActionResult UpdatePost(String id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(new DiaryPost());
            }

            RestClient rc = new RestClient();
            DiaryPost post = new DiaryPost();
            post = rc.findAsync(Int32.Parse(id)).Result;

            if (post != null)
            {
                return View(post);
            }

            return NotFound();
        }

        public IActionResult Edit(DiaryPost post)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", post);
            }

            RestClient rc = new RestClient();
            DiaryPost old = rc.findAsync(post.Id).Result;

            old.Title = post.Title.Trim();
            old.Title = post.Text.Trim();
            old.IsPublic = post.IsPublic;

            rc.Edit(old);

            return Redirect(post.GetLink());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}