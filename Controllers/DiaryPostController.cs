using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diary.Data;
using diary.Models;
using Microsoft.AspNetCore.Mvc;

namespace diary.Controllers
{
    public class DiaryPostController : Controller
    {
        public ActionResult List(int userID)
        {
            RestClient rc = new RestClient();
            ViewBag.allPostByUser = rc.findAsync(userID);

            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult New(Diary pvm)
        {
            RestClient rc = new RestClient();
            rc.Create(pvm);
            return RedirectToAction("");
        }

        public ActionResult Delete(int id)
        {
            RestClient rc = new RestClient();
            rc.Delete(id);
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            RestClient rc = new RestClient();
            Diary pM = new Diary();
            pM = rc.findAsync(id).Result;
            return View("Edit", pM);
        }
        [HttpPost]
        public ActionResult Edit(Diary pM)
        {
            RestClient rc = new RestClient();
            rc.Edit(pM);
            return RedirectToAction("Index");
        }
    }
}