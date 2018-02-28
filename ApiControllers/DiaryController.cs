using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diary.ApiModels;
using diary.ApiModels.DiaryController;
using diary.Data;
using diary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace diary.Controllers
{
    [Route("api/[controller]")]
    public class DiaryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public DiaryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET /api/diary OR :8080/diary
        [HttpGet]
        public RetrieveDiaryResponse Get()
        {
            return new RetrieveDiaryResponse()
            {
                Status = true,
                Result = _dbContext.Diaries.Where(p => p.IsPublic == true).ToList()
            };
        }

        // POST /api/diary OR :8080/diary
        [HttpPost]
        public RetrieveDiaryResponse Post([FromBody]RetrieveDiaryRequest retrieveRequest)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _dbContext.GetUserWithToken(retrieveRequest.Token);
                if (user != null)
                {
                    return new RetrieveDiaryResponse()
                    {
                        Status = true,
                        Result = _dbContext.Diaries.Where(p => p.Author == user.UserName).ToList()
                    };
                }
            }

            return new RetrieveDiaryResponse()
            {
                Status = false,
                Error = "Invalid authentication token."
            };
        }

        // POST /api/diary/create OR :8080/diary/create
        [HttpPost]
        [Route("create")]
        public ApiResponseModel Create([FromBody]CreateDiaryRequest createRequest)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _dbContext.GetUserWithToken(createRequest.Token);
                if (user != null)
                {

                    Diary newDiary = new Diary()
                    {
                        Title = createRequest.Title,
                        Author = user.UserName,
                        PublishDate = DateTime.Now,
                        IsPublic = createRequest.IsPublic,
                        Text = createRequest.Text
                    };
                    _dbContext.Add(newDiary);
                    if (_dbContext.SaveChanges() > 0)
                    {
                        Response.StatusCode = 201;
                        return new ApiResponseModel()
                        {
                            Status = true,
                            Result = new CreateDiaryResultModel()
                            {
                                Id = newDiary.Id.ToString()
                            } 
                        };
                    }
                }
            }

            return new ApiResponseModel()
            {
                Status = false,
                Error = "Invalid authentication token."
            };
        }

        // POST /api/diary/delete OR :8080/diary/delete
        [HttpPost]
        [Route("delete")]
        public DeleteDiaryResponse Delete([FromBody]DeleteDiaryRequest deleteRequest)
        {
            int deleteId = 0;
            if (ModelState.IsValid && int.TryParse(deleteRequest.Id, out deleteId))
            {
                ApplicationUser user = _dbContext.GetUserWithToken(deleteRequest.Token);
                if (user != null)
                {
                    IQueryable<Diary> result = _dbContext.Diaries.Where(
                                                        p => p.Author == user.UserName &&
                                                             p.Id == deleteId);
                    if (result.Count() == 1)
                    {
                        Diary toDelete = result.First();
                        _dbContext.Diaries.Remove(toDelete);
                        if (_dbContext.SaveChanges() > 0)
                        {
                            return new DeleteDiaryResponse()
                            {
                                Status = true
                            };
                        }
                    }
                }
            }

            return new DeleteDiaryResponse()
            {
                Status = false,
                Error = "Invalid authentication token."
            };
        }

        // POST /api/diary/permission OR :8080/diary/permission
        [HttpPost]
        [Route("permission")]
        public AdjustDiaryPermissionResponse Permission([FromBody]AdjustDiaryPermissionRequest adjustRequest)
        {
            int adjustId = 0;
            if (ModelState.IsValid && int.TryParse(adjustRequest.Id, out adjustId))
            {
                ApplicationUser user = _dbContext.GetUserWithToken(adjustRequest.Token);
                if (user != null)
                {
                    IQueryable<Diary> result = _dbContext.Diaries.Where(
                                                    p => p.Author == user.UserName &&
                                                         p.Id == adjustId);

                    if (result.Count() == 1)
                    {
                        Diary toAdjust = result.First();
                        toAdjust.IsPublic = adjustRequest.IsPublic;
                        _dbContext.Diaries.Update(toAdjust);
                        if (_dbContext.SaveChanges() > 0)
                        {
                            return new AdjustDiaryPermissionResponse()
                            {
                                Status = true
                            };
                        }
                    }
                }
            }

            return new AdjustDiaryPermissionResponse()
            {
                Status = false,
                Error = "Invalid authentication token."
            };
        }
    }
}
