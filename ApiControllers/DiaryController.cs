using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public RetrieveDiaryResponse Post(RetrieveDiaryRequest retrieveRequest)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _dbContext.GetUserWithToken(retrieveRequest.Token);
                if (user == null)
                { // invalid token
                    return new RetrieveDiaryResponse()
                    {
                        Status = false,
                        Error = "Invalid authentication token."
                    };
                }
                return new RetrieveDiaryResponse()
                {
                    Status = true,
                    Result = _dbContext.Diaries.Where(p => p.Author == user.UserName).ToList()
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
        public CreateDiaryResponse Create([FromBody]CreateDiaryRequest createRequest)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _dbContext.GetUserWithToken(createRequest.Token);
                if (user == null)
                { // invalid token
                    return new CreateDiaryResponse()
                    {
                        Status = false,
                        Error = "Invalid authentication token."
                    };
                }

                Diary d = new Diary()
                {
                    Title = createRequest.Title,
                    Author = user.UserName,
                    PublishDate = DateTime.Now,
                    IsPublic = createRequest.IsPublic,
                    Text = createRequest.Text
                };
                _dbContext.Add(d);
                if (_dbContext.SaveChanges() > 0)
                {
                    Response.StatusCode = 201;
                    return new CreateDiaryResponse()
                    {
                        Status = true,
                        Result = d.Id.ToString()
                    };
                }
                else 
                {
                    return new CreateDiaryResponse()
                    {
                        Status = false,
                        Error = "Failed to create diary."
                    };
                }
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
            int deleteId = 0;
            if (ModelState.IsValid && int.TryParse(deleteRequest.Id, out deleteId))
            {
                ApplicationUser user = _dbContext.GetUserWithToken(deleteRequest.Token);
                if (user == null)
                { // invalid token
                    return new DeleteDiaryResponse()
                    {
                        Status = false,
                        Error = "Invalid authentication token."
                    };
                }
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
                
                return new DeleteDiaryResponse()
                {
                    Status = true,
                    Error = "Failed to delete diary"
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
            int adjustId = 0;
            if (ModelState.IsValid && int.TryParse(adjustRequest.Id, out adjustId))
            {
                ApplicationUser user = _dbContext.GetUserWithToken(adjustRequest.Token);
                if (user == null)
                { // invalid token
                    return new AdjustDiaryPermissionResponse()
                    {
                        Status = false,
                        Error = "Invalid authentication token."
                    };
                }
                IQueryable<Diary> result = _dbContext.Diaries.Where(
                                                    p => p.Author == user.UserName &&
                                                         p.Id == adjustId);

                if (result.Count() == 1)
                {
                    Diary toAdjust = result.First();
                    toAdjust.IsPublic = !adjustRequest.IsPrivate;
                    _dbContext.Diaries.Update(toAdjust);
                    if (_dbContext.SaveChanges() > 0)
                    {
                        return new AdjustDiaryPermissionResponse()
                        {
                            Status = true
                        };
                    }
                }

                return new AdjustDiaryPermissionResponse()
                {
                    Status = false,
                    Error = "Failed to update diary"
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
