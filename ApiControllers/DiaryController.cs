using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diary.ApiModels.DiaryController;
using diary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace diary.Controllers
{
    [Route("api/[controller]")]
    public class DiaryController : Controller
    {

        // GET /api/diary OR :8080/diary
        [HttpGet]
        public RetrieveDiaryResponse Get()
        {
            return new RetrieveDiaryResponse()
            {
                Status = true,
                Result = Diary.Generate(true)
            };
        }

        // POST /api/diary OR :8080/diary
        [HttpPost]
        public RetrieveDiaryResponse Post(RetrieveDiaryRequest retrieveRequest)
        {
            if (ModelState.IsValid)
            {
                return new RetrieveDiaryResponse()
                {
                    Status = true,
                    Result = Diary.Generate()
                };
            }

            return new RetrieveDiaryResponse()
            {
                Status = false
            };
        }

        // POST /api/diary/create OR :8080/diary/create
        [HttpPost]
        [Route("create")]
        public CreateDiaryResponse Create(CreateDiaryRequest createRequest)
        {
            if (ModelState.IsValid)
            {
                return new CreateDiaryResponse()
                {
                    Status = true,
                    Result = new Random().Next(1,1000).ToString()
                };
            }

            return new CreateDiaryResponse()
            {
                Status = false,
                Error = "Invalid Model"
            };
        }

        // POST /api/diary/delete OR :8080/diary/delete
        [HttpPost]
        [Route("delete")]
        public DeleteDiaryResponse Delete(DeleteDiaryRequest deleteRequest)
        {
            if (ModelState.IsValid)
            {
                return new DeleteDiaryResponse()
                {
                    Status = true
                };
            }

            return new DeleteDiaryResponse()
            {
                Status = false,
                Error = "Invalid Model"
            };
        }

        // POST /api/diary/permission OR :8080/diary/permission
        [HttpPost]
        [Route("permission")]
        public AdjustDiaryPermissionResponse Permission(AdjustDiaryPermissionRequest adjustRequest)
        {
            if (ModelState.IsValid)
            {
                return new AdjustDiaryPermissionResponse()
                {
                    Status = true
                };
            }

            return new AdjustDiaryPermissionResponse()
            {
                Status = false,
                Error = "Invalid Model"
            };
        }
    }
}
