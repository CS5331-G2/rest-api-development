using System;
using System.Collections.Generic;
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
            if (HttpContext.Session.GetString(SessionState.SessionKeyToken) == null || HttpContext.Session.GetString(SessionState.SessionKeyToken).Length == 0)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            
            RestClient rc = new RestClient();

            var posts = rc.findAllAsync(HttpContext.Session.GetString(SessionState.SessionKeyToken)).Result.Select(p => new DiaryPost
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

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RestClient rc = new RestClient();

            var posts = rc.findAllAsync(HttpContext.Session.GetString(SessionState.SessionKeyToken)).Result.Select(p => new DiaryPost
            {
                Id = p.Id,
                Title = p.Title,
                Author = p.Author,
                PublishDate = p.PublishDate,
                IsPublic = p.IsPublic,
                Text = p.Text,
            });

            List<DiaryPost> model = new List<DiaryPost>(posts.Where(p => p.Id == id));


            if (model.Count() == 0)
            {
                return RedirectToAction(nameof(MyDiaryController.Index), "MyDiary");
            }
            else
            {
                return View(model.First());
            }
        }

        //Change permission
        [HttpPost, ActionName("Update")]
        public IActionResult Update(int id)
        {
            RestClient rc = new RestClient();

            var posts = rc.findAllAsync(HttpContext.Session.GetString(SessionState.SessionKeyToken)).Result.Select(p => new DiaryPost
            {
                Id = p.Id,
                Title = p.Title,
                Author = p.Author,
                PublishDate = p.PublishDate,
                IsPublic = p.IsPublic,
                Text = p.Text,
            });

            List<DiaryPost> model = new List<DiaryPost>(posts.Where(p => p.Id == id));


            if (model.Count() == 0)
            {
                return RedirectToAction(nameof(MyDiaryController.Index), "MyDiary");
            }

            var post = model.First();
            
            var success = rc.Edit(HttpContext.Session.GetString(SessionState.SessionKeyToken), post);
            return RedirectToAction(nameof(MyDiaryController.Index), "MyDiary");
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

            return RedirectToAction(nameof(MyDiaryController.Index), "MyDiary");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RestClient rc = new RestClient();

            var posts = rc.findAllAsync(HttpContext.Session.GetString(SessionState.SessionKeyToken)).Result.Select(p => new DiaryPost
            {
                Id = p.Id,
                Title = p.Title,
                Author = p.Author,
                PublishDate = p.PublishDate,
                IsPublic = p.IsPublic,
                Text = p.Text,
            });

            List<DiaryPost> model = new List<DiaryPost>(posts.Where(p => p.Id == id));


            if (model.Count() == 0)
            {
                return RedirectToAction(nameof(MyDiaryController.Index), "MyDiary");
            }
            else
            {
                return View(model.First());
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            RestClient rc = new RestClient();
            var success = rc.Delete(HttpContext.Session.GetString(SessionState.SessionKeyToken), id);
            return RedirectToAction(nameof(MyDiaryController.Index), "MyDiary");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}